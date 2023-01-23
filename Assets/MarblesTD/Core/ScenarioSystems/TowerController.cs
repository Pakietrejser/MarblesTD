using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Projectiles;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.Core.ScenarioSystems
{
    public class TowerController : IUpdateState
    {
        [Inject] MarbleController MarbleController { get; }
        [Inject] TimeController TimeController { get; }
        [Inject] ScenarioManager ScenarioManager { get; set; }
        [Inject] SignalBus SignalBus { get; set; }
        
        public HashSet<AnimalType> UsedAnimalTypes { get; } = new HashSet<AnimalType>();
        
        readonly List<Tower> _activeTowers = new List<Tower>();
        public static readonly List<Projectile> ActiveProjectiles = new List<Projectile>();
        readonly IView _view;
        static TowerController Instance;
        
        public TowerController(IView view)
        {
            Instance = this;
            _view = view;
            _view.TowerCreated += OnTowerCreated;
            Marble.Cracked += OnMarbleCracked;
        }

        void OnTowerCreated(Tower tower)
        {
            _activeTowers.Add(tower);
            UsedAnimalTypes.Add(tower.AnimalType);
            RefreshTowerCount();
        }

        public static void RefreshTowerCountStatic() => Instance.RefreshTowerCount();
        void RefreshTowerCount()
        {
            _activeTowers.ForEach(x => x.StagBuff = StagBuff.None);
            SignalBus.FireId(StagBuff.Tier3, new TowerCountChangedSignal(_activeTowers));
            SignalBus.FireId(StagBuff.Tier2, new TowerCountChangedSignal(_activeTowers));
            SignalBus.FireId(StagBuff.Tier1, new TowerCountChangedSignal(_activeTowers));
        }

        public void EnterState()
        {
            _view.Init();
        }

        public void ExitState()
        {
            _view.Clear();
            
            for (int index = _activeTowers.Count - 1; index >= 0; index--)
            {
                _activeTowers[index].Destroy();
            }
            for (int index = ActiveProjectiles.Count - 1; index >= 0; index--)
            {
                ActiveProjectiles[index].Destroy();
            }

            _activeTowers.Clear();
            ActiveProjectiles.Clear();
        }

        public void UpdateState(float timeDelta)
        {
            for (int i = _activeTowers.Count - 1; i >= 0; i--)
            {
                if (_activeTowers[i].IsDestroyed)
                {
                    _activeTowers.Remove(_activeTowers[i]);
                    
                    RefreshTowerCount();
                    continue;
                }
                
                _activeTowers[i].UpdateTower(MarbleController.Marbles, timeDelta, TimeController.TimeScale);
            }

            for (int i = ActiveProjectiles.Count - 1; i >= 0; i--)
            {
                ActiveProjectiles[i].Update(timeDelta);
            }
        }
        
        void OnMarbleCracked(Marble marble, int crackedAmount)
        {
            SignalBus.Fire(new HoneyGeneratedSignal(crackedAmount));
        }
        
        public interface IView
        {
            event Action<Tower> TowerCreated;
            void Init();
            void Clear();
        }
    }
}