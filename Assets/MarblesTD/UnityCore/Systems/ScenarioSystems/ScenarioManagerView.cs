using System;
using DG.Tweening;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.ScenarioSystems
{
    public class ScenarioManagerView : MonoBehaviour, ScenarioManager.IView
    {
        [SerializeField] CanvasGroup scenarioCanvasGroup;
        [SerializeField] MarbleControllerView marbleControllerView;
        [SerializeField] TMP_Text livesText; 
        [SerializeField] TMP_Text honeyText;

        void Awake()
        {
            scenarioCanvasGroup.alpha = 0;
        }

        public void ShowUI()
        {
            scenarioCanvasGroup.alpha = 0;
            scenarioCanvasGroup.DOKill();
            scenarioCanvasGroup.DOFade(1, 1f)
                .OnComplete(() => scenarioCanvasGroup.interactable = true);
        }

        public void HideUI()
        {
            scenarioCanvasGroup.alpha = 1;
            scenarioCanvasGroup.interactable = false;
            scenarioCanvasGroup.DOKill();
            scenarioCanvasGroup.DOFade(0, 0.5f);
        }
        
        public void SpawnScenario(ScenarioID id)
        {
            var scenarioView = Instantiate(id.GetPrefab(), transform).GetComponent<ScenarioView>();
            marbleControllerView.PathCreators = scenarioView.Paths;
        }

        public void UpdateLivesText(int lives) =>  livesText.text = lives.ToString();
        public void UpdateHoneyText(int honey) => honeyText.text = honey.ToString();
    }
}