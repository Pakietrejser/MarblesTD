using MarblesTD.UnityCore.Common.Audio;
using UnityEngine;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.UnityCore.Common.Installers
{
    public class CommonInstaller : MonoInstaller
    {
        [SerializeField] AudioDatabase audioDatabase;
        
        public override void InstallBindings()
        {
            Container.BindInstance(audioDatabase);

            Container.Bind<SignalBus>().AsSingle();
            Container.Bind<AudioPlayer>().AsSingle().NonLazy();
        }
    }
}