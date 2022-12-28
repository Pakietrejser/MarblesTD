using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Systems.MapSystems
{
    public class ArrowButton : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] CanvasGroup canvasGroup;

        public event Action<ScenarioButton> ArrowClicked;

        ScenarioButton _scenarioButton;

        void Awake()
        {
            button.onClick.AddListener(() => ArrowClicked?.Invoke(_scenarioButton));
            button.interactable = false;
            canvasGroup.alpha = 0;  
        }

        public void Init(ScenarioButton scenarioButton)
        {
            _scenarioButton = scenarioButton;
        }

        public async void Show()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            button.interactable = true;
            canvasGroup.DOKill();
            canvasGroup.DOFade(1, .4f);
        }

        public void Hide()
        {
            button.interactable = false;
            canvasGroup.DOKill();
            canvasGroup.DOFade(0, .2f);
        }
    }
}