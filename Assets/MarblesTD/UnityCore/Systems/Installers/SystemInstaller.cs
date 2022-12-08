using MarblesTD.Core.Systems;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Installers
{
    public class SystemInstaller : MonoInstaller
    {
        [SerializeField] TimeControllerView timeControllerView;
        
        public override void InstallBindings()
        {
            Container.Bind<TimeController.IView>().FromInstance(timeControllerView);
            
            Container.Bind<MarbleController>().AsSingle().NonLazy();
            Container.Bind<TimeController>().AsSingle().NonLazy();
        }
    }
}