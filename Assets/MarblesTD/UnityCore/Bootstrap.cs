using System.Collections;
using System.Collections.Generic;
using MarblesTD.Towers;
using MarblesTD.UnityCore.Settings;
using UnityEngine;

namespace MarblesTD.UnityCore
{
    public class Bootstrap : MonoBehaviour
    {
        public Transform StartingPosition;
        public Transform EndPosition;
        public PlayerView PlayerView;
        public Transform PlaceTowerButtonsParent;
        public GameObject PlaceTowerButtonPrefab;
        public GlobalTowerSettings GlobalTowerSettings;

        public List<Tower> Towers = new List<Tower>();
        public List<Marble> Marbles = new List<Marble>();
        public List<Projectile> Projectiles = new List<Projectile>();

        public Player Player { get; private set; }

        public static Bootstrap Instance;
        private void Awake()
        {
            Instance = this;
        
            //do player
            Player = new Player(PlayerView);
            Player.AddLives(100);
            StartCoroutine(AddLivesAfterDelay());
        
            //do towers
            var go = Instantiate(PlaceTowerButtonPrefab, PlaceTowerButtonsParent);
            var placeTowerButton = go.GetComponent<PlaceTowerButton>();
            placeTowerButton.Init(GlobalTowerSettings);
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
                Towers[i].Update(GetMarblePlacements(), Time.deltaTime);
            }

            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                Projectiles[i].Update(Time.deltaTime);
            }
        
            for (int i = Marbles.Count - 1; i >= 0; i--)
            {
                Marbles[i].Update(EndPosition, Time.deltaTime);
            }
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
