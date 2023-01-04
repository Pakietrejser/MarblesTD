using System.Collections.Generic;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Projectiles;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Common.UI;
using MarblesTD.UnityCore.Entities.Settings;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MarblesTD.UnityCore.Systems.ScenarioSystems
{
    public class TowerControllerView : MonoBehaviour
    {
        [Inject] MarbleController MarbleController;
        [Inject] TimeController TimeController;
        [Inject] ScenarioManager ScenarioManager { get; set; }

        public Transform PlaceTowerButtonsParent;
        public GameObject PlaceTowerButtonPrefab;
        public GlobalTowerSettings GlobalTowerSettings;
        public TowerPanelView TowerPanelView;
        public LayerMask TowersMask;

        public readonly List<Tower> Towers = new List<Tower>();
        public readonly List<Projectile> Projectiles = new List<Projectile>();
        
        public void SelectTower(Tower tower)
        {
            TowerPanelView.ShowPanel(tower);
        }

        public static TowerControllerView Instance;

        void Awake()
        {
            Instance = this;
            TowerPanelView.HidePanel();
            GlobalTowerSettings.Init();
            GlobalTowerSettings.SettingsChanged += GlobalTowerSettingsOnSettingsChanged;
            
            //do towers
            placeTowerButton = Instantiate(PlaceTowerButtonPrefab, PlaceTowerButtonsParent).GetComponent<PlaceTowerButton>();
            placeTowerButton.Init(GlobalTowerSettings.QuickFoxSettings, GlobalTowerSettings);
            
            placeTowerButton = Instantiate(PlaceTowerButtonPrefab, PlaceTowerButtonsParent).GetComponent<PlaceTowerButton>();
            placeTowerButton.Init(GlobalTowerSettings.StarStagSettings, GlobalTowerSettings);
            
            placeTowerButton = Instantiate(PlaceTowerButtonPrefab, PlaceTowerButtonsParent).GetComponent<PlaceTowerButton>();
            placeTowerButton.Init(GlobalTowerSettings.HalberdBearSettings, GlobalTowerSettings);
            
            placeTowerButton = Instantiate(PlaceTowerButtonPrefab, PlaceTowerButtonsParent).GetComponent<PlaceTowerButton>();
            placeTowerButton.Init(GlobalTowerSettings.ShadowPawSettings, GlobalTowerSettings);

            //do marbles
            Marble.Cracked += OnMarbleCracked;
        }

        void OnMarbleCracked(Marble marble, int crackedAmount)
        {
            ScenarioManager.Honey += crackedAmount;
        }

        PlaceTowerButton placeTowerButton;

        void GlobalTowerSettingsOnSettingsChanged()
        {
            placeTowerButton.Init(GlobalTowerSettings.QuickFoxSettings, GlobalTowerSettings);
        }

        void Update()
        {
            for (int i = Towers.Count - 1; i >= 0; i--)
            {
                if (Towers[i].IsDestroyed)
                {
                    Towers.Remove(Towers[i]);
                    continue;
                }
                
                Towers[i].Update(MarbleController.Marbles, Time.deltaTime * TimeController.TimeScale);
            }

            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                Projectiles[i].Update(Time.deltaTime * TimeController.TimeScale);
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(position, Vector2.down, 100, TowersMask);
                if (hit.collider == null)
                {
                    TowerPanelView.HidePanel();
                }
            }

            TowerPanelView.UpdatePanel();
        }
    }
}
