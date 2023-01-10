﻿using System;
using System.Collections.Generic;
using System.Linq;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.MapSystems;
using MarblesTD.UnityCore.Common.Extensions;
using MarblesTD.UnityCore.Systems.GameSystems.Saving;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MarblesTD.UnityCore.Systems.MapSystems
{
    public class ScenarioSpawner : MonoBehaviour, IState, ISaveable
    {
        [SerializeField] List<ScenarioButton> scenarioButtons;
        [SerializeField] TMP_Text questsCompletedText;
        [SerializeField] RawImage background;
        [Space] 
        [SerializeField] GameObject mapCanvas;
        
        [Inject] Mediator Mediator { get; set; }

        List<Scenario> _scenarios;

        void Start()
        {
            ExitState();
        }

        public void EnterState()
        {
            scenarioButtons.ForEach(button => button.UpdateButton());

            int allQuests = _scenarios.Count * 3;
            int completedQuests = _scenarios.Sum(scenario => scenario.GetCompletedQuests());
            questsCompletedText.text = $"Ukończone misje: {completedQuests}/{allQuests}";
            background.color = ProgressionHelper.GetProgressionColor(completedQuests, allQuests);
            LayoutRebuilder.ForceRebuildLayoutImmediate(background.rectTransform);

            mapCanvas.SetActive(true);
            gameObject.SetActive(true);
        }

        public void ExitState()
        {
            mapCanvas.SetActive(false);
            gameObject.SetActive(false);
        }

        public void Save(SaveData saveData, bool freshSave)
        {
            if (freshSave)
            {
                var ids = Enum.GetValues(typeof(ScenarioID)).Cast<ScenarioID>().ToList();
                ids.Remove(ScenarioID.NULL);
                int length = ids.Count;
                _scenarios = new List<Scenario>();
                for (var i = 0; i < length; i++)
                {
                    var scenario = new Scenario(ids[i], false, false, false);
                    _scenarios.Add(scenario);
                    scenarioButtons.First(x => x.ID == ids[i]).Init(scenario, Mediator);
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
            _scenarios = new List<Scenario>();
            for (var i = 0; i < length; i++)
            {
                (var id, bool questA, bool questB, bool questC) = saveData.ScenarioQuests[i];
                var scenario = new Scenario(id, questA, questB, questC);
                _scenarios.Add(scenario);
                scenarioButtons.First(x => x.ID == id).Init(scenario, Mediator);
            }
        }
    }
}