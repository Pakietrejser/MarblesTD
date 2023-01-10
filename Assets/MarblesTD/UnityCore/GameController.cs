using System.Collections.Generic;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Systems.GameSystems;
using MarblesTD.UnityCore.Systems.GameSystems.Saving;
using MarblesTD.UnityCore.Systems.MapSystems;
using UnityEngine;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.UnityCore
{
    public class GameController : MonoBehaviour
    {
        [Inject] SaveWindow _saveWindow;
        [Inject] MainMenu _mainMenu;

        [Inject] ScenarioSpawner _scenarioSpawner;
        
        [Inject] ScenarioManager ScenarioManager { get; set; }
        [Inject] TimeController _timeController;
        [Inject] MarbleController _marbleController;
        
        [Inject] SignalBus Bus { get; set; }
        [Inject] Mediator Mediator { get; set; }
        
        GroupState _gameStates;
        GroupState _mapStates;
        GroupState _scenarioStates;

        KeyCode escapeKey = KeyCode.Escape;

        void Awake()
        {
            Bus.Subscribe<MapStartedSignal>(EnterMap);
            Bus.Subscribe<ScenarioStartedSignal>(EnterScenario);
        }

        void EnterMap(MapStartedSignal signal)
        {
            _gameStates?.ExitState();
            _scenarioStates?.ExitState();
            _mapStates.EnterState();
        }
        
        void EnterScenario(ScenarioStartedSignal signal)
        {
            _gameStates?.ExitState();
            _mapStates?.ExitState();
            ScenarioManager.CurrentScenario = signal.Scenario;
            _scenarioStates.EnterState();
        }

        void Start()
        {
            _gameStates = new GroupState(new List<IState>()
            {
                _saveWindow,
                _mainMenu,
            });

            _mapStates = new GroupState(new List<IState>()
            {
                _scenarioSpawner
            });
            
            _scenarioStates = new GroupState(new List<IState>()
            {
                ScenarioManager,
                _timeController,
                _marbleController,
            });
         
            _gameStates.EnterState();
        }

        
        void Update()
        {
            if (_scenarioStates.IsActive)
            {
                _scenarioStates.UpdateState(Time.deltaTime * _timeController.TimeScale);
                
                if (Input.GetKeyDown(escapeKey))
                {
                    Mediator.SendAsync(new PauseScenarioRequest());
                }
            }
            else
            {
                if (Input.GetKeyDown(escapeKey) && !_saveWindow.gameObject.activeSelf)
                {
                    Mediator.SendAsync(new ChangeSettingsRequest());
                }
            }
        }
    }
}