using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MarblesTD.Core.Common.Extensions;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.UnityCore.Common.RequestHandlers
{
    public class StartScenarioRequestHandler : MonoRequestHandler<StartScenarioRequest, bool>
    {
        [SerializeField] GameObject windowBox;
        [SerializeField] Button enterScenario;
        [SerializeField] Button winScenario;
        [SerializeField] Button closeButton;
        [SerializeField] TMP_Text titleText;
        [SerializeField] TMP_Text unlockText;
        [SerializeField] Image scenarioPath;
        [Header("Quests")]
        [SerializeField] Image[] stars;
        [SerializeField] TMP_Text[] questsTexts;
        [SerializeField] Sprite starLocked;
        [SerializeField] Sprite starUnlocked;

        [Inject] SignalBus Bus { get; set; }

        bool _receivedConfirmation;
        bool _confirmed;

        bool PlayerInteraction() => _receivedConfirmation;
        
        void Awake()
        {
            enterScenario.onClick.AddListener(EnterScenarioClicked);
            winScenario.onClick.AddListener(WinScenarioClicked);
            closeButton.onClick.AddListener(Hide);
            gameObject.SetActive(false);
        }

        protected override async UniTask<bool> Execute(StartScenarioRequest request)
        {
            var scenario = request.Scenario;
            var scenarioPathSprite = request.Scenario.ID.GetPathSprite();
            
            titleText.text = $"Scenariusz: {request.Scenario.ID.GetName()}";
            scenarioPath.sprite = scenarioPathSprite;
            unlockText.enabled = scenario.Completed;
            
            for (var i = 0; i < stars.Length; i++)
            {
                bool unlocked = scenario.GetQuestCompletion(i);
                stars[i].sprite = unlocked ? starUnlocked : starLocked;
                stars[i].ChangeAlpha(unlocked ? 1f : 0.8f);
                questsTexts[i].text = scenario.GetQuest(i).GetQuestDescription();
            }
            
            Show();
            _receivedConfirmation = false;
            await UniTask.WaitUntil(PlayerInteraction);
            bool result = _confirmed;
            if (result) Bus.Fire(new ScenarioStartedSignal());
            Hide();
            return result;
        }

        void Show()
        {
            windowBox.transform.localScale = Vector3.one * .01f;
            gameObject.SetActive(true);
            windowBox.transform.DOKill();
            windowBox.transform.DOScale(Vector3.one, .4f);
        }

        async void Hide()
        {
            _confirmed = false;
            _receivedConfirmation = true;
            windowBox.transform.DOKill();
            windowBox.transform.DOScale(Vector3.one * .01f, .2f);
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            gameObject.SetActive(false);
        }

        void EnterScenarioClicked()
        {
            _confirmed = true;
            _receivedConfirmation = true;
        }
        
        void WinScenarioClicked()
        {
            _confirmed = true;
            _receivedConfirmation = true;
        }
    }
}