using MarblesTD.Core.Common.Systems;
using MarblesTD.Core.Entities.Marbles;
using Zenject;

namespace MarblesTD.UnityCore.Installers
{
    public class SystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MarbleController>().AsSingle().NonLazy();
        }
    }
}