using MarblesTD.Core.MapSystems;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.Map
{
    public class ScenarioSpawnerView : MonoBehaviour, ScenarioSpawner.IView
    {
        [SerializeField] ScenarioButton main;

        void Start()
        {
            main.SetAsActive();
        }
    }
}