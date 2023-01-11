using DG.Tweening;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Systems.ScenarioSystems
{
    public class ScenarioManagerView : MonoBehaviour, ScenarioManager.IView
    {
        [SerializeField] CanvasGroup scenarioCanvasGroup;
        [SerializeField] MarbleControllerView marbleControllerView;
        [SerializeField] TMP_Text livesText; 
        [SerializeField] TMP_Text honeyText;
        [SerializeField] Button settingsButton;

        ScenarioView _currentScenarioView;
        
        void Awake()
        {
            scenarioCanvasGroup.alpha = 0;
            settingsButton.onClick.AddListener(() => Mediator.Instance.SendAsync(new PauseScenarioRequest()));
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
            _currentScenarioView = Instantiate(id.GetPrefab(), transform).GetComponent<ScenarioView>();
            marbleControllerView.PathCreators = _currentScenarioView.Paths;
        }

        public void DestroyScenario()
        {
            Destroy(_currentScenarioView.gameObject);
        }

        public void UpdateLivesText(int lives) =>  livesText.text = lives.ToString();
        public void UpdateHoneyText(int honey) => honeyText.text = honey.ToString();
    }
}