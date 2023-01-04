using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Systems.ScenarioSystems;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Installers
{
    public class ScenarioSystemsInstaller : MonoInstaller
    {
        [SerializeField] ScenarioManagerView scenarioManagerView;
        [SerializeField] MarbleControllerView marbleControllerView;
        [SerializeField] TimeControllerView timeControllerView;
        
        public override void InstallBindings()
        {
            Container.Bind<ScenarioManager.IView>().FromInstance(scenarioManagerView);
            Container.Bind<MarbleController.IView>().FromInstance(marbleControllerView);
            Container.Bind<TimeController.IView>().FromInstance(timeControllerView);

            Container.Bind<ScenarioManager>().AsSingle().NonLazy();
            Container.Bind<MarbleController>().AsSingle().NonLazy();
            Container.Bind<TimeController>().AsSingle().NonLazy();
        }
    }
}