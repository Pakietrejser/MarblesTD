using UnityEngine;

namespace MarblesTD.Core.Settings
{
    [CreateAssetMenu(menuName = "Scriptables/Global Settings/new Tower Settings", fileName = "Global Tower Settings")]
    public class GlobalTowerSettings : ScriptableObject
    {
        [SerializeField] TowerSetSettings[] towerSets;
    }

    public enum TowerSet
    {
        NULL = 0,
        WildAnimals = 1,
        NobleAnimals = 2,
        EvilAnimals = 3,
        HeroAnimals = 4,
    }
}