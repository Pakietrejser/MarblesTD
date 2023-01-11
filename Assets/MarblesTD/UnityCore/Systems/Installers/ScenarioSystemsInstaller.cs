using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Common.UI;
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
        [SerializeField] TowerControllerView towerControllerView;
        [SerializeField] TowerPanel towerPanel;
        
        public override void InstallBindings()
        {
            Container.Bind<ScenarioManager.IView>().FromInstance(scenarioManagerView);
            Container.Bind<MarbleController.IView>().FromInstance(marbleControllerView);
            Container.Bind<TimeController.IView>().FromInstance(timeControllerView);
            Container.Bind<TowerController.IView>().FromInstance(towerControllerView);
            Container.Bind<TowerControllerView>().FromInstance(towerControllerView);

            Container.Bind<ScenarioManager>().AsSingle().NonLazy();
            Container.Bind<MarbleController>().AsSingle().NonLazy();
            Container.Bind<TimeController>().AsSingle().NonLazy();
            Container.Bind<TowerController>().AsSingle().NonLazy();

            Container.Bind<TowerPanel>().FromInstance(towerPanel);
        }
    }
}