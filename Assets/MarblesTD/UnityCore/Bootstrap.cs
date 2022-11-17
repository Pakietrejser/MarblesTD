using System.Collections.Generic;
using MarblesTD.Core.Marbles;
using MarblesTD.Core.Players;
using MarblesTD.Core.Towers;
using MarblesTD.Core.Towers.Projectiles;
using MarblesTD.UnityCore.Settings;
using PathCreation;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MarblesTD.UnityCore
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject] MarbleController MarbleController;
        
        [SerializeField] PathCreator pathCreator;

        public Vector3 StartingPosition => pathCreator.path.GetPointAtTime(0, EndOfPathInstruction.Stop);
        public Vector3 EndPosition => pathCreator.path.GetPointAtTime(1, EndOfPathInstruction.Stop);
        
        public PlayerView PlayerView;
        public Transform PlaceTowerButtonsParent;
        public GameObject PlaceTowerButtonPrefab;
        public GlobalTowerSettings GlobalTowerSettings;
        public TowerPanelView TowerPanelView;
        public LayerMask TowersMask;

        public readonly List<Tower> Towers = new List<Tower>();
        public readonly List<Projectile> Projectiles = new List<Projectile>();

        public Player Player { get; private set; }
        
        public void SelectTower(Tower tower)
        {
            TowerPanelView.ShowPanel(tower);
        }

        public static Bootstrap Instance;

        void Awake()
        {
            Instance = this;
            TowerPanelView.HidePanel();
        
            //do player
            Player = new Player(PlayerView);
            Player.AddLives(20);
            Player.AddMoney(100);
            
            GlobalTowerSettings.Init();
            TowerPanelView.Init(Player);
            
            GlobalTowerSettings.SettingsChanged += GlobalTowerSettingsOnSettingsChanged;
            
            //do towers
            for (int i = 0; i < 9; i++)
            {
                var go = Instantiate(PlaceTowerButtonPrefab, PlaceTowerButtonsParent);
                placeTowerButton = go.GetComponent<PlaceTowerButton>();
                placeTowerButton.Init(GlobalTowerSettings.QuickFoxSettings, GlobalTowerSettings);
            }
            
            //do marbles
            Marble.Cracked += OnMarbleCracked;
        }

        void OnMarbleCracked(Marble marble, int crackedAmount)
        {
            Player.AddMoney(crackedAmount);
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
                
                Towers[i].Update(MarbleController.Marbles, Time.deltaTime);
            }

            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                Projectiles[i].Update(Time.deltaTime);
            }

            foreach (var marble in MarbleController.Marbles)
            {
                if (marble.IsDestroyed) continue;
                
                float distanceTravelled = marble.DistanceTravelled + marble.Speed * Time.deltaTime;
                var position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                var rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

                bool reachedDestination = position == EndPosition;
                marble.Update(distanceTravelled, position, rotation, reachedDestination);
                if (reachedDestination)
                {
                    Player.RemoveLives(marble.Health);
                }
            }
            
            MarbleController.OnUpdate(Time.deltaTime);

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, TowersMask))
                {
                    TowerPanelView.HidePanel();
                }
            }

            TowerPanelView.UpdatePanel();
        }
    }
}
