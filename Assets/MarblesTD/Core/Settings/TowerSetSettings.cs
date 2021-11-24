using UnityEngine;

namespace MarblesTD.Core.Settings
{
    [CreateAssetMenu(menuName = "Scriptables/Settings/new Tower Set Settings", fileName = "Tower Set Settings")]
    public class TowerSetSettings : ScriptableObject
    {
        public string displayName = "Animals";
        public TowerSet towerSet = TowerSet.NULL;
        [Space]
        public Color color = Color.white;
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