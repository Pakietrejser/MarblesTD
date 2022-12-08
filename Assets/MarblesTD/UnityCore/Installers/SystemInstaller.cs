using MarblesTD.Core.Systems;
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