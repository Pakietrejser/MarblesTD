using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;

namespace MarblesTD.Core.Systems
{
    public class TimeController : IState
    {
        public float TimeScale => _timeScales[_currentTimeScaleIndex];
        
        int _currentTimeScaleIndex = 1;
        
        readonly IView _view;
        readonly Dictionary<int, float> _timeScales = new Dictionary<int, float>
        {
            {0, 0f},
            {1, 1f},
            {2, 2f},
            {3, 4f},
        };
        
        public TimeController(SignalBus signalBus, IView view)
        {
            _view = view;
            _view.RequestChangeSpeed += ChangeSpeed;
            signalBus.Subscribe<MarbleWaveSpawnedSignal>(OnMarbleWaveSpawned);
        }
        
        public void Enter()
        {
            _currentTimeScaleIndex = 1;
            _view.UpdateTimeScale(_timeScales[_currentTimeScaleIndex]);
        }

        public void Update(float timeDelta) {}

        public void Exit()
        {
            _currentTimeScaleIndex = 0;
        }
        
        void ChangeSpeed()
        {
            if (_currentTimeScaleIndex == _timeScales.Count - 1)
                _currentTimeScaleIndex = 0;
            else
                _currentTimeScaleIndex++;

            _view.UpdateTimeScale(_timeScales[_currentTimeScaleIndex]);
        }
        
        void OnMarbleWaveSpawned()
        {
            if (_currentTimeScaleIndex != 0) return;
            ChangeSpeed();
        }
        
        public interface IView
        {
            event Action RequestChangeSpeed;
            void UpdateTimeScale(float timeScale);
        }
    }
}