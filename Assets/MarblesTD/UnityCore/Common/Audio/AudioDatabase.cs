using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace MarblesTD.UnityCore.Common.Audio
{
    [CreateAssetMenu(menuName = "Scriptables/Audio/database", fileName = "AudioDatabase")]
    public class AudioDatabase : ScriptableObject
    {
        [Header("Music")] 
        [SerializeField] EventReference mainMusic;
        
        [Header("Marbles SFX")]
        [SerializeField] EventReference marbleDamaged;
        
        Dictionary<AudioName, EventReference> _audioDictionary;
        Dictionary<AudioName, EventReference> AudioDictionary => _audioDictionary ??= CreateAudioDictionary();
        public EventReference GetAudio(AudioName audioName) => AudioDictionary[audioName];
        
        Dictionary<AudioName, EventReference> CreateAudioDictionary()
        {
            if (mainMusic.IsNull) throw new NullReferenceException("EventReferences are null, probably using an instance of AudioDatabase instead of the original SO.");
            
            return new Dictionary<AudioName, EventReference>
            {
                {AudioName.MainMusic, mainMusic},
                
                {AudioName.MarbleDamaged, marbleDamaged},
            };
        }
    }
}