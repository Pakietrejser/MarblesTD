using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals.List;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.UnityCore.Systems.GameSystems
{
    public class MainMenu : MonoBehaviour, IState
    {
        [Inject] Mediator Mediator { get; set; }
        [Inject] GameSettings GameSettings { get; set; }
        [Inject] SignalBus Bus { get; set; }
        
        [SerializeField] KeyCode escapeKey;
        [SerializeField] Button playButton;
        [SerializeField] Button settingsButton;
        [SerializeField] Button exitButton;

        public void Enter()
        {
            playButton.onClick.AddListener(HandlePlayClicked);
            settingsButton.onClick.AddListener(HandleSettingsClicked);
            exitButton.onClick.AddListener(HandleExitGameButton);
            Hide();
        }

        public void Exit()
        {
            playButton.onClick.RemoveListener(HandlePlayClicked);
            settingsButton.onClick.RemoveListener(HandleSettingsClicked);
            exitButton.onClick.RemoveListener(HandleExitGameButton);
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        
        void Update()
        {
            if (Input.GetKeyDown(escapeKey))
            {
                Debug.Log(gameObject.activeSelf);
                if (gameObject.activeSelf)
                {
                    Hide();
                }
                else
                {
                    Show();
                }
            }
        }

        async void HandlePlayClicked()
        {
            Bus.Fire(new MapStartedSignal());
            Hide();
        }
        
        void HandleSettingsClicked()
        {
            Hide();
            GameSettings.Show();
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