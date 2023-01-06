using System;
using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Requests.List;

namespace MarblesTD.UnityCore.Common.RequestHandlers
{
    public class ExitScenarioRequestHandler : MonoRequestHandler<ExitScenarioRequest, bool>
    {
        void Awake()
        {
            gameObject.SetActive(false);
        }

        protected override UniTask<bool> Execute(ExitScenarioRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}