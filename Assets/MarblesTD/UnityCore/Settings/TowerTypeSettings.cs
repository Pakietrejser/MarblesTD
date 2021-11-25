using MarblesTD.Core.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Settings
{
    [CreateAssetMenu(menuName = "Scriptables/Settings/new Tower Type Settings", fileName = "Tower Type Settings")]
    public class TowerTypeSettings : ScriptableObject
    {
        public string DisplayName = "Animals";
        public TowerType TowerType = TowerType.None;
        [Space]
        public Color Color = Color.white;
    }
    
}