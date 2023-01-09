using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.MapSystems;
using UnityEngine;

namespace MarblesTD.Core.ScenarioSystems
{
    public class ScenarioManager : RequestHandler<PurchaseRequest, bool>, IUpdateState
    {
        int _lives;
        int _honey;
        readonly IView _view;

        public Scenario CurrentScenario;

        public int Lives
        {
            get => _lives;
            set
            {
                _lives = value;
                _view.UpdateLivesText(_lives);

                if (Lives <= 0)
                {
                    Debug.Log("you lost");
                }
            }
        }

        public int Honey
        {
            get => _honey;
            set
            {
                _honey = value;
                _view.UpdateHoneyText(_honey);
            }
        }

        public ScenarioManager(IView view, Mediator mediator)
        {
            _view = view;
            mediator.AddHandler<PurchaseRequest, bool>(this);
        }

        public void Enter()
        {
            Lives = 20;
            Honey = 100;

            _view.SpawnScenario(CurrentScenario.ID);
            _view.ShowUI();
        }

        public void Exit()
        {
            _view.HideUI();
        }

        public void UpdateState(float timeDelta)
        {
            
        }
        
        protected async override UniTask<bool> Execute(PurchaseRequest request)
        {
            if (Honey < request.RequiredHoney) return false;
            Honey -= request.RequiredHoney;
            return true;
        }
        
        public interface IView
        {
            void ShowUI();
            void HideUI();
            void UpdateLivesText(int lives);
            void UpdateHoneyText(int honey);
            void SpawnScenario(ScenarioID id);
        }
    }
}