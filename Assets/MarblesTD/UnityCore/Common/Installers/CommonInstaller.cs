using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.UnityCore.Common.Audio;
using MarblesTD.UnityCore.Common.RequestHandlers;
using MarblesTD.UnityCore.Systems.Game.Saving;
using UnityEngine;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.UnityCore.Common.Installers
{
    public class CommonInstaller : MonoInstaller
    {
        [SerializeField] AudioDatabase audioDatabase;
        
        [SerializeField] BinaryChoiceRequestHandler binaryChoiceRequestHandler;
        [SerializeField] SaveWindow saveWindow;

        public override void InstallBindings()
        {
            Container.BindInstance(audioDatabase);

            Container.Bind<SignalBus>().AsSingle();
            Container.Bind<AudioPlayer>().AsSingle().NonLazy();
            
            CreateAndBindMediator();
        }

        void CreateAndBindMediator()
        {
            var mediator = new Mediator();
            Container.BindInstance(mediator);
            
            mediator.AddHandler<BinaryChoiceRequest, bool>(binaryChoiceRequestHandler);
            mediator.AddHandler<SaveGameRequest, bool>(saveWindow);
        }
    }
}