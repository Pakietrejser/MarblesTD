using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.UnityCore.Common.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.UnityCore.Systems.GameSystems
{
    public class MainMenu : MonoBehaviour, IState
    {
        [Inject] Mediator Mediator { get; set; }
        [Inject] SignalBus Bus { get; set; }

        [SerializeField] RawImage background;
        [SerializeField] CanvasGroup windowBox;
        [SerializeField] Button playButton;
        [SerializeField] Button settingsButton;
        [SerializeField] Button exitButton;

        void Awake()
        {
            playButton.onClick.AddListener(HandlePlayClicked);
            settingsButton.onClick.AddListener(HandleSettingsClicked);
            exitButton.onClick.AddListener(HandleExitGameButton);
            gameObject.SetActive(false);
            background.ChangeAlpha(0);
        }

        public void EnterState()
        {
            background.raycastTarget = true;
            background.ChangeAlpha(0);
            background.DOKill();
            background.DOFade(1f, 2f);
        }

        public void ExitState()
        {
            background.DOKill();
            background.DOFade(0f, 0.2f);
            background.raycastTarget = false;
        }

        // public void Show() => gameObject.SetActive(true);
        // public void Hide() => gameObject.SetActive(false);
        
        public void Show()
        {
            windowBox.interactable = true;
            windowBox.transform.localScale = Vector3.one * .01f;
            gameObject.SetActive(true);
            windowBox.transform.DOKill();
            windowBox.transform.DOScale(Vector3.one, .4f);

            // windowBox.transform.localPosition = Vector3.up * 1000;
            // gameObject.SetActive(true);
            // windowBox.transform.DOKill();
            // windowBox.transform.DOLocalMoveY(50, 1.5f).SetEase(Ease.OutBounce);
        }

        async void Hide()
        {
            windowBox.interactable = false;
            windowBox.transform.DOKill();
            windowBox.transform.DOScale(Vector3.one * .01f, .2f);
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            gameObject.SetActive(false);
        }

        async void HandlePlayClicked()
        {
            Bus.Fire(new MapStartedSignal());
            Hide();
        }
        
        void HandleSettingsClicked()
        {
            Mediator.SendAsync(new ChangeSettingsRequest());
        }

        async void HandleExitGameButton()
        {
            bool proceed = await Mediator.SendAsync(new BinaryChoiceRequest("Exit Game", "Are you sure you want to close the game?"));
            if (!proceed) return;
            
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            return;
        }
    }
}