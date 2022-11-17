using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.UnityCore.Installers
{
    public class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SignalBus>().AsSingle();
        }
    }
}