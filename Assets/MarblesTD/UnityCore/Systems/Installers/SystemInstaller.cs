using MarblesTD.Core.Systems;
using MarblesTD.UnityCore.Systems.Scenario;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Installers
{
    public class SystemInstaller : MonoInstaller
    {
        [SerializeField] MarbleControllerView marbleControllerView;
        [SerializeField] TimeControllerView timeControllerView;
        
        public override void InstallBindings()
        {
            Container.Bind<MarbleController.IView>().FromInstance(marbleControllerView);
            Container.Bind<TimeController.IView>().FromInstance(timeControllerView);

            Container.Bind<MarbleController>().AsSingle().NonLazy();
            Container.Bind<TimeController>().AsSingle().NonLazy();
        }
    }
}