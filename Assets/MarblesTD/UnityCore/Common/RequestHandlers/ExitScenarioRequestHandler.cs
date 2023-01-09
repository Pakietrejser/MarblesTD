using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Requests;
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
    public class ExitScenarioRequestHandler : MonoRequestHandler<ExitScenarioRequest, bool>
    {
        [SerializeField] GameObject windowBox;
        [SerializeField] TMP_Text titleText;
        [SerializeField] Button closeButton;
        [Header("Quests")] 
        [SerializeField] GameObject towerUnlock;
        [SerializeField] Image[] stars;
        [SerializeField] TMP_Text[] questsTexts;
        [SerializeField] Sprite starLocked;
        [SerializeField] Sprite starUnlocked;
        
        [Inject] SignalBus Bus { get; set; }
        [Inject] Mediator Mediator { get; set; }
        
        bool _receivedConfirmation;
        bool PlayerInteraction() => _receivedConfirmation;
        
        void Awake()
        {
            closeButton.onClick.AddListener(Hide);
            gameObject.SetActive(false);
        }

        protected override async UniTask<bool> Execute(ExitScenarioRequest request)
        {
            var scenario = request.Scenario;
            bool playerWon = request.PlayerWon;
            int wavesCompleted = request.WavesCompleted;

            if (wavesCompleted >= 2)
            {
                scenario.TryCompleteQuest(QuestID.Wave20);
            }
            if (wavesCompleted >= 3)
            {
                scenario.TryCompleteQuest(QuestID.Wave40);
            }

            Debug.Log(playerWon ? "Player Won!" : "Player Lost!");
            closeButton.interactable = false;
            titleText.text = playerWon ? "Wygrałeś!" : "Porażka";
            towerUnlock.SetActive(false);
            for (var i = 0; i < stars.Length; i++)
            {
                stars[i].transform.parent.gameObject.SetActive(false);
            }
            
            bool successful = await Mediator.SendAsync(new SaveGameRequest());
            
            Show();
            for (var i = 0; i < stars.Length; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(.8f));
                bool unlocked = scenario.GetQuestCompletion(i);
                stars[i].sprite = unlocked ? starUnlocked : starLocked;
                stars[i].ChangeAlpha(unlocked ? 1f : 0.8f);
                questsTexts[i].text = scenario.GetQuest(i).GetQuestDescription();
                stars[i].transform.parent.gameObject.SetActive(true);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.8f));
            closeButton.interactable = true;
            _receivedConfirmation = false;
            await UniTask.WaitUntil(PlayerInteraction);
            Hide();

            Bus.Fire(new MapStartedSignal());
            return true;
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
            _receivedConfirmation = true;
            windowBox.transform.DOKill();
            windowBox.transform.DOScale(Vector3.one * .01f, .2f);
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            gameObject.SetActive(false);
        }
    }
}