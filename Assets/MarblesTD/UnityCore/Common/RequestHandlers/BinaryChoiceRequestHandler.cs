using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Requests.List;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.RequestHandlers
{
    public class BinaryChoiceRequestHandler : MonoRequestHandler<BinaryChoiceRequest, bool>
    {
        [SerializeField] TMP_Text titleText;
        [SerializeField] TMP_Text contentText;
        [Space]
        [SerializeField] Button yesButton;
        [SerializeField] Button noButton;
        
        bool _receivedConfirmation;
        bool _confirmed;
        
        void Start()
        {
            yesButton.onClick.AddListener(HandleYesRequest);
            noButton.onClick.AddListener(HandleNoRequest);
            Hide();
        }

        protected override async Task<bool> Execute(BinaryChoiceRequest request)
        {
            titleText.text = request.Title;
            contentText.text = request.Description;
            gameObject.SetActive(true);
            noButton.gameObject.SetActive(!request.AlwaysTrue);
            _receivedConfirmation = false;
            
            await UniTask.WaitUntil(PlayerInteraction);

            Hide();
            return _confirmed;
        }
        
        bool PlayerInteraction() => _receivedConfirmation;

        void HandleYesRequest()
        {
            _confirmed = true;
            _receivedConfirmation = true;
        }

        void HandleNoRequest()
        {
            _confirmed = false;
            _receivedConfirmation = true;
        }

        void Hide()
        {
            titleText.text = string.Empty;
            contentText.text = string.Empty;
            gameObject.SetActive(false);
        }
    }
}