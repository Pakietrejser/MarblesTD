using MarblesTD.Core.Common.Systems;
using MarblesTD.Core.Marbles;
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