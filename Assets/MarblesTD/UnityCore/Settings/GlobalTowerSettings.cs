using System;
using System.Linq;
using MarblesTD.Towers;
using MarblesTD.Towers.Upgrades;
using UnityEngine;
using UnityEngine.Serialization;

namespace MarblesTD.UnityCore.Settings
{
    [CreateAssetMenu(menuName = "Scriptables/Global Settings/new Tower Settings", fileName = "Global Tower Settings")]
    public class GlobalTowerSettings : ScriptableObject
    {
        [SerializeField] TowerSetSettings[] towerSets;
        [FormerlySerializedAs("QuickFoxSettings")]
        [Space] 
        [SerializeField] QuickFox.Settings quickFoxSettings;
        [SerializeField] QuickFoxSettings qfSettings;
        [Space]
        [SerializeField] RuleOfThree.Settings ruleOfThreeSettings;

        public QuickFoxSettings QfSettings => qfSettings;

        public TowerSetSettings Get(TowerSet towerSet)
        {
            var found = towerSets.FirstOrDefault(x => x.towerSet == towerSet);
            return found ? found : throw new ArgumentException();
        }
        
        public TTower Create<TTower>(ITowerView towerView, Vector3 spawnPosition) where TTower : Tower
        {
            return new QuickFox(quickFoxSettings, towerView, spawnPosition) as TTower;
        }
    }
}