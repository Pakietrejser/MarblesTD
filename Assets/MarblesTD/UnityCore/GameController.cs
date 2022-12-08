using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Systems;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore
{
    public class GameController : MonoBehaviour
    {
        [Inject] TimeController _timeController;
        [Inject] MarbleController _marbleController;
        
        GroupState _gameStates;
        GroupState _mapStates;
        GroupState _scenarioStates;

        void Awake()
        {
            _scenarioStates = new GroupState(new List<IState>()
            {
                _timeController,
                _marbleController,
            });
            
            _scenarioStates.Enter();
        }

        void Update()
        {
            if (_scenarioStates.IsActive)
            {
                _scenarioStates.Update(Time.deltaTime * _timeController.TimeScale);
            }
        }
    }
}