using System.Collections.Generic;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Systems.Game;
using MarblesTD.UnityCore.Systems.Game.Saving;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore
{
    public class GameController : MonoBehaviour
    {
        [Inject] SaveWindow _saveWindow;
        [Inject] MainMenu _mainMenu;
        [Inject] GameSettings _gameSettings;
        
        [Inject] TimeController _timeController;
        [Inject] MarbleController _marbleController;
        
        GroupState _gameStates;
        GroupState _mapStates;
        GroupState _scenarioStates;

        void Start()
        {
            _gameStates = new GroupState(new List<IState>()
            {
                _saveWindow,
                _mainMenu,
                _gameSettings,
            });
            
            _scenarioStates = new GroupState(new List<IState>()
            {
                _timeController,
                _marbleController,
            });
         
            _gameStates.Enter();
            _scenarioStates.Enter();
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