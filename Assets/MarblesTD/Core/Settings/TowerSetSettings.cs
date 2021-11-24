using UnityEngine;

namespace MarblesTD.Core.Settings
{
    [CreateAssetMenu(menuName = "Scriptables/Settings/new Tower Set Settings", fileName = "Tower Set Settings")]
    public class TowerSetSettings : ScriptableObject
    {
        [SerializeField] string displayName = "Animals";
        [SerializeField] TowerSet towerSet = TowerSet.NULL;
        [Space]
        [SerializeField] Color color = Color.white;
    }
}