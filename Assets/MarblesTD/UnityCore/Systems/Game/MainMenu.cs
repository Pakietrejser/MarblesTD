using System;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Game
{
    public class MainMenu : MonoBehaviour, IState
    {
        [Inject] Mediator Mediator { get; set; }
        
        [SerializeField] Button playButton;
        [SerializeField] Button settingsButton;
        [SerializeField] Button exitButton;

        void Awake()
        {
            playButton.onClick.AddListener(HandlePlayClicked);
            settingsButton.onClick.AddListener(HandleSettingsClicked);
            exitButton.onClick.AddListener(HandleExitGameButton);
        }

        void HandlePlayClicked()
        {
            throw new NotImplementedException();
        }
        
        void HandleSettingsClicked()
        {
            throw new NotImplementedException();
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

        public void Enter() {}

        public void Exit() {}
    }
}