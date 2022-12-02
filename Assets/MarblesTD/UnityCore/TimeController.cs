using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore
{
    public class TimeController : MonoBehaviour
    {
        
        [SerializeField] Bootstrap bootstrap;
        [SerializeField] TMP_Text text;
        [SerializeField] Button changeSpeedButton;

        int _currentTimeScaleIndex = 1;

        readonly Dictionary<int, float> _timeScales = new Dictionary<int, float>
        {
            {0, 0f},
            {1, 1f},
            {2, 2f},
            {3, 4f},
        };
        
        void Awake()
        {
            changeSpeedButton.onClick.AddListener(ChangeSpeed);
            
            float timeScale = _timeScales[_currentTimeScaleIndex];
            bootstrap.TimeScale = timeScale;
            text.text = $"{timeScale}x";
            
            SignalBus.Instance.Subscribe<MarbleWaveBeginSpawnSignal>(OnSpawnMarbleWave);
        }

        void ChangeSpeed()
        {
            if (_currentTimeScaleIndex == _timeScales.Count - 1)
                _currentTimeScaleIndex = 0;
            else
                _currentTimeScaleIndex++;

            float timeScale = _timeScales[_currentTimeScaleIndex];
            bootstrap.TimeScale = timeScale;
            text.text = $"{timeScale}x";
        }

        void OnSpawnMarbleWave()
        {
            if (_currentTimeScaleIndex != 0) return;
            ChangeSpeed();
        }
    }
}