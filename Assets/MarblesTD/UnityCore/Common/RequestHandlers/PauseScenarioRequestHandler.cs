using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Systems.GameSystems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.UnityCore.Common.RequestHandlers
{
    public class PauseScenarioRequestHandler : MonoRequestHandler<PauseScenarioRequest, bool>
    {
        [SerializeField] CanvasGroup windowBox;
        [SerializeField] Button settingsButton;
        [SerializeField] Button continueButton;
        [SerializeField] Button exitButton;
        
        [Inject] SignalBus Bus { get; set; }
        [Inject] Mediator Mediator { get; set; }
        [Inject] GameSettings GameSettings { get; set; }
        [Inject] TimeController TimeController { get; set; }
        
        bool _receivedConfirmation;
        bool _continue;
        bool _processing;
        
        bool PlayerInteraction() => _receivedConfirmation;

        void Awake()
        {
            settingsButton.onClick.AddListener(SettingsClicked);
            continueButton.onClick.AddListener(ContinueClicked);
            exitButton.onClick.AddListener(ExitClicked);
            gameObject.SetActive(false);
        }

        void SettingsClicked()
        {
            Mediator.SendAsync(new ChangeSettingsRequest());
        }
        
        void ContinueClicked()
        {
            _continue = true;
            _receivedConfirmation = true;
        }
        
        async void ExitClicked()
        {
            bool proceed = await Mediator.SendAsync(new BinaryChoiceRequest("Ale by na pewno?", "Wyjście teraz ze scenariusza sprawi że nie otrzymasz żadnych odznak."));
            _continue = false;
            _receivedConfirmation = proceed;
        }

        protected override async UniTask<bool> Execute(PauseScenarioRequest request)
        {
            if (_processing)
            {
                _continue = true;
                _receivedConfirmation = true;
                return false;
            }
            _processing = true;
            
            var scenario = request.Scenario;

            TimeController.Pause();
            Show();
            _receivedConfirmation = false;
            await UniTask.WaitUntil(PlayerInteraction);
            Hide();
            TimeController.Unpause();

            if (!_continue)
            {
                Bus.Fire(new MapStartedSignal());
            }

            _processing = false;
            return _continue;
        }
        
        void Show()
        {
            windowBox.interactable = true;
            windowBox.transform.localScale = Vector3.one * .01f;
            gameObject.SetActive(true);
            windowBox.transform.DOKill();
            windowBox.transform.DOScale(Vector3.one, .4f);
        }

        async void Hide()
        {
            windowBox.interactable = false;
            windowBox.transform.DOKill();
            windowBox.transform.DOScale(Vector3.one * .01f, .2f);
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            gameObject.SetActive(false);
        }
    }
}