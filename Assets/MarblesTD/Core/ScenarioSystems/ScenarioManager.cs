using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.MapSystems;

namespace MarblesTD.Core.ScenarioSystems
{
    public class ScenarioManager : RequestHandler<PurchaseRequest, bool>, IUpdateState
    {
        int _lives;
        int _honey;
        public bool RunEnded { get; set; }
        readonly IView _view;
        readonly Mediator _mediator;

        public Scenario CurrentScenario;

        public int Lives
        {
            get => _lives;
            set
            {
                _lives = value;
                _view.UpdateLivesText(_lives);
                
                if (Lives <= 0 && !RunEnded)
                {
                    RunEnded = true;
                    _mediator.SendAsync(new ExitScenarioRequest(CurrentScenario, false, MarbleController.CurrentWave));
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

        public ScenarioManager(IView view, Mediator mediator, SignalBus signalBus)
        {
            _view = view;
            _mediator = mediator;
            mediator.AddHandler<PurchaseRequest, bool>(this);
            signalBus.Subscribe<TowerSoldSignal>(OnTowerSold);
        }

        public void EnterState()
        {
            RunEnded = false;
            Lives = 20;
            Honey = 100;

            _view.SpawnScenario(CurrentScenario.ID);
            _view.ShowUI();
        }

        public void ExitState()
        {
            _view.HideUI();
            _view.DestroyScenario();
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
        
        void OnTowerSold(TowerSoldSignal signal)
        {
            Honey += signal.Honey;
        }
        
        public interface IView
        {
            void ShowUI();
            void HideUI();
            void UpdateLivesText(int lives);
            void UpdateHoneyText(int honey);
            void SpawnScenario(ScenarioID id);
            void DestroyScenario();
        }
    }
}