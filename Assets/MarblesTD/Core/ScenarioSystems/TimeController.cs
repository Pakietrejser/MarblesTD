using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Automatons;

namespace MarblesTD.Core.ScenarioSystems
{
    public class TimeController : IState
    {
        public float TimeScale => _paused ? 0 : _timeScales[_currentTimeScaleIndex];

        int _currentTimeScaleIndex;
        bool _paused;
        
        readonly IView _view;
        readonly Dictionary<int, float> _timeScales = new Dictionary<int, float>
        {
            {0, 1f},
            {1, 2f},
            {2, 4f},
        };
        
        public TimeController(IView view)
        {
            _view = view;
            _view.RequestChangeSpeed += ChangeSpeed;
        }
        
        public void Enter()
        {
            _currentTimeScaleIndex = 0;
            _view.UpdateTimeScale(_timeScales[_currentTimeScaleIndex]);
        }
        
        public void Exit()
        {
            _currentTimeScaleIndex = 0;
        }

        public void Pause() => _paused = true;
        public void Unpause() => _paused = false;
        
        void ChangeSpeed()
        {
            if (_currentTimeScaleIndex == _timeScales.Count - 1)
                _currentTimeScaleIndex = 0;
            else
                _currentTimeScaleIndex++;

            _view.UpdateTimeScale(_timeScales[_currentTimeScaleIndex]);
        }

        public interface IView
        {
            event Action RequestChangeSpeed;
            void UpdateTimeScale(float timeScale);
        }
    }
}