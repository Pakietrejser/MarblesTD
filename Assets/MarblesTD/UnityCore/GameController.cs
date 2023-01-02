using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Requests;
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
        [Inject] GameSettings _gameSettings;

        [Inject] ScenarioSpawner _scenarioSpawner;
        
        [Inject] TimeController _timeController;
        [Inject] MarbleController _marbleController;
        
        [Inject] SignalBus _bus;
        
        GroupState _gameStates;
        GroupState _mapStates;
        GroupState _scenarioStates;

        void Awake()
        {
            _bus.Subscribe<MapStartedSignal>(EnterMap);
            _bus.Subscribe<ScenarioStartedSignal>(EnterScenario);
        }

        void EnterMap(MapStartedSignal signal)
        {
            _scenarioStates?.Exit();
            _mapStates.Enter();
        }
        
        void EnterScenario(ScenarioStartedSignal signal)
        {
            _mapStates?.Exit();
            _scenarioStates.Enter();
        }

        void Start()
        {
            _gameStates = new GroupState(new List<IState>()
            {
                _saveWindow,
                _mainMenu,
                _gameSettings,
            });

            _mapStates = new GroupState(new List<IState>()
            {
                _scenarioSpawner
            });
            
            _scenarioStates = new GroupState(new List<IState>()
            {
                _timeController,
                _marbleController,
            });
         
            _gameStates.Enter();
        }

        void Update()
        {
            if (_scenarioStates.IsActive)
            {
                _scenarioStates.UpdateState(Time.deltaTime * _timeController.TimeScale);
            }
        }
    }
}