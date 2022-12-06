using System;
using System.Collections.Generic;
using System.Linq;
using MarblesTD.Core.Towers;
using MarblesTD.Towers;
using MarblesTD.Towers.HalberdBearImpl;
using MarblesTD.Towers.QuickFoxImpl;
using MarblesTD.Towers.QuickFoxImpl.LeftPathUpgrades;
using MarblesTD.Towers.ShadowPawImpl;
using MarblesTD.Towers.StarStagImpl;
using UnityEngine;
using UnityEngine.Serialization;

namespace MarblesTD.UnityCore.Settings
{
    [CreateAssetMenu(menuName = "Scriptables/Global Settings/new Tower Settings", fileName = "Global Tower Settings")]
    public class GlobalTowerSettings : ScriptableObject
    {
        [Header("Global Settings")]
        [SerializeField] TowerTypeSettings[] towerSets;

        [Header("Tower Settings")]
        public QuickFox.Settings QuickFoxSettings;
        public StarStag.Settings StarStagSettings;
        public ShadowPaw.Settings ShadowPawSettings;
        public HalberdBear.Settings HalberdBearSettings;

        public TowerTypeSettings GetTowerTypeSettings(TowerType towerType)
        {
            var found = towerSets.FirstOrDefault(x => x.TowerType == towerType);
            return found ? found : throw new ArgumentException();
        }

        //temp, to factory
        public Tower CreateTower(Tower.SettingsBase settings, ITowerView towerView, Vector3 spawnPosition)
        {
            Tower tower = settings switch
            {
                HalberdBear.Settings _ => new HalberdBear(HalberdBearSettings, towerView, spawnPosition),
                QuickFox.Settings _ => new QuickFox(QuickFoxSettings, towerView, spawnPosition),
                ShadowPaw.Settings _ => new ShadowPaw(ShadowPawSettings, towerView, spawnPosition),
                StarStag.Settings _ => new StarStag(StarStagSettings, towerView, spawnPosition),
                _ => throw new ArgumentOutOfRangeException(nameof(settings))
            };

            activeTowers.Add(tower);
            tower.Selected += () => Bootstrap.Instance.SelectTower(tower);
            return tower;
        }

        List<Tower> activeTowers = new List<Tower>();
        public void Init()
        {
            activeTowers = new List<Tower>();
            SettingsChanged = null;
        }
        
        public event Action SettingsChanged;
        void OnValidate()
        {
            foreach (var tower in activeTowers)
            {
                tower.UpdateSettings(QuickFoxSettings);
            }
            
            SettingsChanged?.Invoke();
        }
    }
}