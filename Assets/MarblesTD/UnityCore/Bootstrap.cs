using System.Collections;
using System.Collections.Generic;
using MarblesTD.Core.Marbles;
using MarblesTD.Core.Player;
using MarblesTD.Core.Projectiles;
using MarblesTD.Core.Towers;
using MarblesTD.UnityCore.Settings;
using PathCreation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarblesTD.UnityCore
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] PathCreator pathCreator;

        public Vector3 StartingPosition => pathCreator.path.GetPointAtTime(0);
        public Vector3 EndPosition => pathCreator.path.GetPointAtTime(1);
        
        public PlayerView PlayerView;
        public Transform PlaceTowerButtonsParent;
        public GameObject PlaceTowerButtonPrefab;
        public GlobalTowerSettings GlobalTowerSettings;
        public TowerPanelView TowerPanelView;
        public LayerMask TowersMask;

        public List<Tower> Towers = new List<Tower>();
        public List<Marble> Marbles = new List<Marble>();
        public List<Projectile> Projectiles = new List<Projectile>();

        public Player Player { get; private set; }
        
        public void SelectTower(Tower tower)
        {
            TowerPanelView.ShowPanel(tower);
        }

        public static Bootstrap Instance;
        private void Awake()
        {
            Instance = this;
            TowerPanelView.HidePanel();
        
            //do player
            Player = new Player(PlayerView);
            Player.AddLives(100);
            StartCoroutine(AddLivesAfterDelay());
            
            GlobalTowerSettings.Init();
            TowerPanelView.Init(Player);
            
            GlobalTowerSettings.SettingsChanged += GlobalTowerSettingsOnSettingsChanged;
            
            //do towers
            var go = Instantiate(PlaceTowerButtonPrefab, PlaceTowerButtonsParent);
            placeTowerButton = go.GetComponent<PlaceTowerButton>();
            placeTowerButton.Init(GlobalTowerSettings.QuickFoxSettings, GlobalTowerSettings);
            
        }

        PlaceTowerButton placeTowerButton;

        void GlobalTowerSettingsOnSettingsChanged()
        {
            placeTowerButton.Init(GlobalTowerSettings.QuickFoxSettings, GlobalTowerSettings);
        }

        IEnumerator AddLivesAfterDelay()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.2f);
                Player.AddMoney(10);
            }
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
                
                Towers[i].Update(GetMarblePlacements(), Time.deltaTime);
            }

            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                Projectiles[i].Update(Time.deltaTime);
            }
        
            for (int i = Marbles.Count - 1; i >= 0; i--)
            {
                if (Marbles[i].IsDestroyed)
                {
                    Marbles.Remove(Marbles[i]);
                    continue;
                }

                float distanceTravelled = Marbles[i].DistanceTravelled + Marbles[i].Speed * 20 * Time.deltaTime;
                var position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                var rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                Marbles[i].Update(distanceTravelled, position, rotation, position == EndPosition);
            }

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

        private IEnumerable<MarblePlacement> GetMarblePlacements()
        {
            int marblesCount = Marbles.Count;
            for (int i = 0; i < marblesCount; i++)
            {
                var marble = Marbles[i];
                yield return new MarblePlacement(marble, marble.Position);
            }
        }
    }
}
