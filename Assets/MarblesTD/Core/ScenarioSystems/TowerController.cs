using System.Collections.Generic;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Projectiles;
using UnityEngine;
using Zenject;

namespace MarblesTD.Core.ScenarioSystems
{
    public class TowerController : IUpdateState
    {
        [Inject] MarbleController MarbleController { get; }
        [Inject] TimeController TimeController { get; }
        [Inject] ScenarioManager ScenarioManager { get; set; }
        
        public static readonly List<Tower> ActiveTowers = new List<Tower>();
        public static readonly List<Projectile> ActiveProjectiles = new List<Projectile>();
        readonly IView _view;
        
        public TowerController(IView view)
        {
            _view = view;
            Marble.Cracked += OnMarbleCracked;
        }
        
        public void EnterState()
        {
            _view.Init();
        }

        public void ExitState()
        {
        }

        public void UpdateState(float timeDelta)
        {
            Debug.Log(ActiveTowers.Count);
            for (int i = ActiveTowers.Count - 1; i >= 0; i--)
            {
                if (ActiveTowers[i].IsDestroyed)
                {
                    ActiveTowers.Remove(ActiveTowers[i]);
                    continue;
                }
                
                ActiveTowers[i].UpdateTower(MarbleController.Marbles, timeDelta);
            }

            for (int i = ActiveProjectiles.Count - 1; i >= 0; i--)
            {
                ActiveProjectiles[i].Update(timeDelta);
            }

            //TODO: wtf
            // if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            // {
            //     var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //     var hit = Physics2D.Raycast(position, Vector2.down, 100, TowersMask);
            //     if (hit.collider == null)
            //     {
            //         towerPanel.HidePanel();
            //     }
            // }
        }
        
        void OnMarbleCracked(Marble marble, int crackedAmount)
        {
            ScenarioManager.Honey += crackedAmount;
        }
        
        public interface IView
        {
            void Init();
        }
    }
}