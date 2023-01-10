using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.UI
{
    [RequireComponent(typeof(Button))]
    public class ExitGameButton : MonoBehaviour
    {
        void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(HandleExitGameButton);
        }

        async void HandleExitGameButton()
        {
            bool proceed = await Mediator.Instance.SendAsync(new BinaryChoiceRequest("Exit Game", "Are you sure you want to close the game?"));
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