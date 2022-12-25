using System;
using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;
using FMODUnity;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.UnityCore.Systems.Game.Saving;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Game
{
    public class GameSettings : MonoBehaviour, IState
    {
        [Inject] Mediator Mediator { get; set; }
        
        [SerializeField] Slider masterSlider;
        [SerializeField] Slider musicSlider;
        [SerializeField] Slider sfxSlider;
        [SerializeField] Toggle muteWhileInBackgroundToggle;
        [SerializeField] TMP_Dropdown windowModeDropdown;
        [SerializeField] TMP_Dropdown resolutionsDropdown;
        [SerializeField] TMP_Dropdown framerateDropdown;
        [SerializeField] Toggle vsyncToggle;

        Bus _masterBus;
        Bus _musicBus;
        Bus _sfxBus;
        float _masterVolume = .5f;
        float _musicVolume = 1f;
        float _sfxVolume = 1f;
        bool _muteWhileInBackground = true;
        FullScreenMode _windowMode = FullScreenMode.FullScreenWindow;
        Resolution? _resolution = null;
        Resolution Resolution
        {
            get
            {
                _resolution ??= Screen.currentResolution;
                return _resolution.Value;
            }
        }
        List<Resolution> _possibleResolutions;
        int _framerate = 60;
        bool _vsyncEnabled = false;
        
        readonly Dictionary<float, string> _aspectRatioStrings = new Dictionary<float, string>
        {
            {1, "1:1"},
            {4/3f, "4:3"},
            {5/4f, "5:4"},
            {3/2f, "3:2"},
            {16/10f, "16:10"},
            {16/9f, "16:9"},
            {21/9f, "21:9"},
            {32/9f, "32:9"},
        };

        readonly List<int> _framerateList = new List<int>
        {
            24, 30, 60, 120, 240
        };

        public async void RestoreDefault() //set inside Unity
        {
            bool proceed = await Mediator.SendAsync(new BinaryChoiceRequest("Restore Default", "Are you sure you want to restore default settings?"));
            if (!proceed) return;
            
            _masterVolume = .5f;
            _musicVolume = 1f;
            _sfxVolume = 1f;
            _muteWhileInBackground = true;
            _windowMode = FullScreenMode.FullScreenWindow;
            _resolution = _possibleResolutions[0];
            _framerate = 60;
            _vsyncEnabled = false;
            _masterBus.setVolume(_masterVolume);
            _musicBus.setVolume(_musicVolume);
            _sfxBus.setVolume(_sfxVolume);
            Screen.fullScreenMode = _windowMode;
            Screen.SetResolution(Resolution.width, Resolution.height, Screen.fullScreen);

            bool successful = await Mediator.SendAsync(new SaveGameRequest());
        }
        
        void Awake()
        {
            _masterBus = RuntimeManager.GetBus("bus:/Master");
            _musicBus = RuntimeManager.GetBus("bus:/Master/Music");
            _sfxBus = RuntimeManager.GetBus("bus:/Master/Sfx");
            
            int targetResolution = -1;
            _possibleResolutions = new List<Resolution>();
            foreach (var resolution in Screen.resolutions.Reverse())
            {
                if (_possibleResolutions.Any(t => t.width == resolution.width && t.height == resolution.height))
                    continue;

                _possibleResolutions.Add(resolution);

                if (ResolutionsEqual(resolution, Resolution))
                    targetResolution = _possibleResolutions.Count - 1;
            }

            resolutionsDropdown.options.Clear();
            foreach (var resolution in _possibleResolutions)
            {
                int x = resolution.width;
                int y = resolution.height;
                var resolutionString = $"{x} x {y} ({GetAspectRatioString(x, y)})";
                resolutionsDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionString));
            }

            // If we couldn't find a resolution that we already had selected, go ahead and find the one closest to our current resolution.
            if (targetResolution == -1)
            {
                const float LeastDistance = float.MaxValue;
                for (var i = 0; i < _possibleResolutions.Count; i++)
                {
                    float dist = ResolutionDistance(Screen.currentResolution, _possibleResolutions[i]);
                    if (dist < LeastDistance)
                    {
                        targetResolution = i;
                    }
                }
            }

            _resolution = _possibleResolutions[targetResolution];
            resolutionsDropdown.value = targetResolution;
            resolutionsDropdown.RefreshShownValue();
            resolutionsDropdown.onValueChanged.RemoveListener(SetResolution);
            resolutionsDropdown.onValueChanged.AddListener(SetResolution);

            // FRAMERATE
            framerateDropdown.options.Clear();
            foreach (int framerate in _framerateList)
            {
                framerateDropdown.options.Add(new TMP_Dropdown.OptionData(framerate.ToString()));
            }
            framerateDropdown.value = _framerateList.IndexOf(_framerate);
            framerateDropdown.RefreshShownValue();
            framerateDropdown.onValueChanged.RemoveListener(SetFramerate);
            framerateDropdown.onValueChanged.AddListener(SetFramerate);
            
            //VSYNC
            vsyncToggle.onValueChanged.RemoveListener(SetVsync);
            vsyncToggle.onValueChanged.AddListener(SetVsync);
            vsyncToggle.isOn = _vsyncEnabled;
        }
        
        void OnApplicationFocus(bool hasFocus)
        {
            if (_muteWhileInBackground)
                _masterBus.setMute(!hasFocus);
        }

        async void SetVsync(bool vsyncEnabled)
        {
            _vsyncEnabled = vsyncEnabled;
            if (vsyncEnabled)
            {
                Application.targetFrameRate = -1;
            }
            else
            {
                Application.targetFrameRate = _framerate;
            }
            bool successful = await Mediator.SendAsync(new SaveGameRequest());
        }

        public async void SetWindowMode(int value) //set inside Unity
        {
            _windowMode = value switch
            {
                0 => FullScreenMode.FullScreenWindow,
                1 => FullScreenMode.ExclusiveFullScreen,
                2 => FullScreenMode.Windowed,
                _ => throw new ArgumentOutOfRangeException()
            };
            Screen.fullScreenMode = _windowMode;
            bool successful = await Mediator.SendAsync(new SaveGameRequest());
        }
        
        async void SetResolution(int index)
        {
            _resolution = _possibleResolutions[index];
            Screen.SetResolution(Resolution.width, Resolution.height, Screen.fullScreen);
            bool successful = await Mediator.SendAsync(new SaveGameRequest());
        }

        async void SetFramerate(int index)
        {
            _framerate = _framerateList[index];
            if (_vsyncEnabled)
            {
                Application.targetFrameRate = -1;
            }
            else
            {
                Application.targetFrameRate = _framerate;
            }
            bool successful = await Mediator.SendAsync(new SaveGameRequest());
        }

        public async void SetMasterVolume(float volume)
        {
            _masterVolume = volume;
            _masterBus.setVolume(_masterVolume);
            bool successful = await Mediator.SendAsync(new SaveGameRequest());
        }
        
        public async void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
            _musicBus.setVolume(_musicVolume);
            bool successful = await Mediator.SendAsync(new SaveGameRequest());
        }
        
        public async void SetSfxVolume(float volume)
        {
            _sfxVolume = volume;
            _sfxBus.setVolume(_sfxVolume);
            bool successful = await Mediator.SendAsync(new SaveGameRequest());
        }

        public async void SetMuteWhileInBackground(bool muteWhileInBackground)
        {
            _muteWhileInBackground = muteWhileInBackground;
            bool successful = await Mediator.SendAsync(new SaveGameRequest());
        }

        public void Save(SaveData saveData)
        {
            saveData.MasterVolume = _masterVolume;
            saveData.MusicVolume = _musicVolume;
            saveData.SfxVolume = _sfxVolume;
            masterSlider.value = _masterVolume;
            musicSlider.value = _musicVolume;
            sfxSlider.value = _sfxVolume;
            
            saveData.MuteWhileInBackground = _muteWhileInBackground;
            muteWhileInBackgroundToggle.isOn = _muteWhileInBackground;

            saveData.WindowMode = _windowMode;
            windowModeDropdown.value = _windowMode switch
            {
                FullScreenMode.ExclusiveFullScreen => 1,
                FullScreenMode.FullScreenWindow => 0,
                FullScreenMode.Windowed => 2,
                _ => throw new ArgumentOutOfRangeException()
            };

            saveData.Resolution = Resolution;
            resolutionsDropdown.value = _possibleResolutions.IndexOf(Resolution);

            saveData.Framerate = _framerate;
            framerateDropdown.value = _framerateList.IndexOf(_framerate);

            saveData.VsyncEnabled = _vsyncEnabled;
            vsyncToggle.isOn = _vsyncEnabled;
        }

        public void Load(SaveData saveData)
        {
            _masterVolume = saveData.MasterVolume;
            _musicVolume = saveData.MusicVolume;
            _sfxVolume = saveData.SfxVolume;
            _masterBus.setVolume(_masterVolume);
            _musicBus.setVolume(_musicVolume);
            _sfxBus.setVolume(_sfxVolume);
            masterSlider.value = _masterVolume;
            musicSlider.value = _musicVolume;
            sfxSlider.value = _sfxVolume;

            _muteWhileInBackground = saveData.MuteWhileInBackground;
            muteWhileInBackgroundToggle.isOn = _muteWhileInBackground;

            _windowMode = saveData.WindowMode;
            windowModeDropdown.value = _windowMode switch
            {
                FullScreenMode.ExclusiveFullScreen => 1,
                FullScreenMode.FullScreenWindow => 0,
                FullScreenMode.Windowed => 2,
                _ => throw new ArgumentOutOfRangeException()
            };

            _resolution = saveData.Resolution;
            resolutionsDropdown.value = _possibleResolutions.IndexOf(Resolution);
            
            _framerate = saveData.Framerate;
            framerateDropdown.value = _framerateList.IndexOf(_framerate);

            _vsyncEnabled = saveData.VsyncEnabled;
            vsyncToggle.isOn = _vsyncEnabled;
        }

        static bool ResolutionsEqual(Resolution lhs, Resolution rhs)
        {
            return lhs.width == rhs.width && lhs.height == rhs.height;
        }

        static float ResolutionDistance(Resolution lhs, Resolution rhs)
        {
            return Mathf.Abs(lhs.width - rhs.width) + Mathf.Abs(lhs.height - rhs.height);
        }

        string GetAspectRatioString(float x, float y)
        {
            float ratio = Math.Max(x, y) / Math.Min(x, y);
            if (_aspectRatioStrings.TryGetValue(ratio, out string result))
                return result;

            if (TryFindClosestRatio(ratio, out string ratioString))
                return ratioString;

            return Math.Round(ratio * 100) / 100 + ":1";
        }

        bool TryFindClosestRatio(float oldRatio, out string ratioString)
        {
            foreach (float ratio in _aspectRatioStrings.Keys)
            {
                if (!(Math.Abs(ratio - oldRatio) <= .05f)) continue;
                
                ratioString = _aspectRatioStrings[ratio];
                return true;
            }

            ratioString = string.Empty;
            return false;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}