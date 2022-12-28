using MarblesTD.UnityCore.Systems.MapSystems;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Installers
{
    public class MapSystemsInstaller : MonoInstaller
    {
        [SerializeField] ScenarioSpawner scenarioSpawner;
        
        public override void InstallBindings()
        {
            Container.BindInstance(scenarioSpawner);
        }
    }
}