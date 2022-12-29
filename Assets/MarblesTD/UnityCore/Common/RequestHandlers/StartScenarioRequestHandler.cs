using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MarblesTD.Core.Common.Requests.List;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.RequestHandlers
{
    public class StartScenarioRequestHandler : MonoRequestHandler<StartScenarioRequest, bool>
    {
        [SerializeField] GameObject windowBox;
        [SerializeField] Button enterScenario;
        [SerializeField] Button winScenario;

        void Awake()
        {
            Hide();
        }

        protected async override UniTask<bool> Execute(StartScenarioRequest request)
        {
            Show();

            return true;
        }

        void Show()
        {
            windowBox.transform.localScale = Vector3.one * .01f;
            gameObject.SetActive(true);
            windowBox.transform.DOKill();
            windowBox.transform.DOScale(Vector3.one, .5f);
        }

        void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}