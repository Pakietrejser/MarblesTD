using FMODUnity;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;

namespace MarblesTD.UnityCore.Common.Audio
{
    public class AudioPlayer
    {
        readonly AudioDatabase _audioDatabase;
        
        public AudioPlayer(AudioDatabase audioDatabase, SignalBus signalBus)
        {
            _audioDatabase = audioDatabase;
            Play(AudioName.MainMusic);

            signalBus.Subscribe<ButtonClickSignal>(() => Play(AudioName.ButtonClick));
            signalBus.Subscribe<ButtonHoverSignal>(() => Play(AudioName.ButtonHover));
            signalBus.Subscribe<MarbleDamagedSignal>(() => Play(AudioName.MarbleDamaged));
        }

        void Play(AudioName audioName)
        {
            var eventReference = _audioDatabase.GetAudio(audioName);
            RuntimeManager.PlayOneShot(eventReference);
        }
    }
}