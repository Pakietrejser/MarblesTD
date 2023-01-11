using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Extensions;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.Towers;
using MarblesTD.Towers.CannonBoarTower;
using MarblesTD.Towers.QuickFoxTower;
using MarblesTD.Towers.StarStagTower;
using MarblesTD.UnityCore.Common.RequestHandlers;
using MarblesTD.UnityCore.Common.UI;
using MarblesTD.UnityCore.Systems.GameSystems.Saving;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.ScenarioSystems
{
    public class TowerControllerView : MonoRequestHandler<CreateTowerRequest, Tower>, TowerController.IView, ISaveable
    {
        [SerializeField] TowerPanel towerPanel;
        [SerializeField] List<PlaceTowerButton> placeTowerButtons;

        Dictionary<Type, bool> _towerUnlocks;
        readonly Dictionary<Type, Func<Tower>> _towerCreate = new Dictionary<Type, Func<Tower>>()
        {
            {typeof(QuickFox), () => new QuickFox()},
            {typeof(CannonBoar), () => new CannonBoar()},
            {typeof(StarStag), () => new StarStag()},
            
            {typeof(Mastiffteer), () => new Mastiffteer()},
            {typeof(HalberdBear), () => new HalberdBear()},
            {typeof(Beehive), () => new Beehive()},
            
            {typeof(ShadowPaw), () => new ShadowPaw()},
            {typeof(MagicPot), () => new MagicPot()},
            {typeof(WebWeaver), () => new WebWeaver()},
        };

        public void Init()
        {
            var index = 0;
            foreach (var pair in _towerUnlocks)
            {
                placeTowerButtons[index++].Init(_towerCreate[pair.Key].Invoke(), pair.Value);
            }
        }

        public void Clear()
        {
            towerPanel.Hide();
        }
        
        protected async override UniTask<Tower> Execute(CreateTowerRequest request)
        {
            if (!_towerCreate.TryGetValue(request.Type, out var createTower)) throw new NullReferenceException();
            var tower = createTower.Invoke();
            tower.Init(request.View, request.Position);
            tower.Selected += OnTowerSelected;
            TowerController.ActiveTowers.Add(tower);
            return tower;
        }

        void OnTowerSelected(Tower tower)
        {
            towerPanel.Show(tower);
        }

        public void Save(SaveData saveData, bool freshSave)
        {
            if (freshSave)
            {
                _towerUnlocks = new Dictionary<Type, bool>
                {
                    {typeof(QuickFox), true},
                    {typeof(CannonBoar), false},
                    {typeof(StarStag), false},
                    
                    {typeof(Mastiffteer), false},
                    {typeof(HalberdBear), false},
                    {typeof(Beehive), false},
                    
                    {typeof(ShadowPaw), false},
                    {typeof(MagicPot), false},
                    {typeof(WebWeaver), false},
                };
            }
            saveData.TowerUnlocks = _towerUnlocks.Values.ToArray();
        }

        public void Load(SaveData saveData)
        {
            _towerUnlocks = new Dictionary<Type, bool>();
            if (saveData.TowerUnlocks.Length != 9) throw new NullReferenceException();
            _towerUnlocks.Add(typeof(QuickFox), saveData.TowerUnlocks[0]);
            _towerUnlocks.Add(typeof(CannonBoar), saveData.TowerUnlocks[1]);
            _towerUnlocks.Add(typeof(StarStag), saveData.TowerUnlocks[2]);
                
            _towerUnlocks.Add(typeof(Mastiffteer), saveData.TowerUnlocks[3]);
            _towerUnlocks.Add(typeof(HalberdBear), saveData.TowerUnlocks[4]);
            _towerUnlocks.Add(typeof(Beehive), saveData.TowerUnlocks[5]);
                
            _towerUnlocks.Add(typeof(ShadowPaw), saveData.TowerUnlocks[6]);
            _towerUnlocks.Add(typeof(MagicPot), saveData.TowerUnlocks[7]);
            _towerUnlocks.Add(typeof(WebWeaver), saveData.TowerUnlocks[8]);
        }
    }
}
