using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Settings
{
    [CreateAssetMenu(menuName = "Scriptables/Settings/Quick Fox", fileName = "Quick Fox Settings")]
    public class QuickFoxSettings : ScriptableObject
    {
        public TowerSet towerSet;
        public Sprite towerIcon;
        public int towerCost;
        [Space]
        public QuickFox.Settings quickFoxSettings;
    }
}