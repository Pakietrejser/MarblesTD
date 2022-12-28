using System;
using System.Linq;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.MapSystems;
using MarblesTD.UnityCore.Systems.GameSystems.Saving;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.MapSystems
{
    public class ScenarioSpawner : MonoBehaviour, IState, ISaveable
    {
        [SerializeField] ScenarioButton main;

        Scenario[] _scenarios;

        void Start()
        {
            main.SetAsActive();
        }

        public void Enter()
        {
            
            
            
            
            main.SetAsActive();
        }

        public void Exit()
        {
            
        }

        public void Save(SaveData saveData, bool freshSave)
        {
            if (freshSave)
            {
                var ids = Enum.GetValues(typeof(ScenarioID)).Cast<ScenarioID>().ToArray();
                int length = ids.Length;
                _scenarios = new Scenario[length];
                for (var i = 0; i < length; i++)
                {
                    _scenarios[i] = new Scenario(ids[i], false, false, false);
                }
            }
            
            saveData.ScenarioQuests = _scenarios.Select(x => (
                x.ID, 
                x.GetQuestCompletion(0), 
                x.GetQuestCompletion(1), 
                x.GetQuestCompletion(2)
                )).ToArray();
        }

        public void Load(SaveData saveData)
        {
            int length = saveData.ScenarioQuests.Length;
            _scenarios = new Scenario[length];
            for (var i = 0; i < length; i++)
            {
                (var id, bool questA, bool questB, bool questC) = saveData.ScenarioQuests[i];
                _scenarios[i] = new Scenario(id, questA, questB, questC);
            }
        }
    }
}