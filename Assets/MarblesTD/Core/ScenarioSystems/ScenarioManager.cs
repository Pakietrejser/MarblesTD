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
        public bool LostLifeThisScenario { get; private set; }

        int _lives;
        int _honey;
        public bool RunEnded { get; set; }
        readonly IView _view;
        readonly Mediator _mediator;
        readonly SignalBus _signalBus;
        readonly TimeController _timeController;

        public Scenario CurrentScenario;

        public int Lives
        {
            get => _lives;
            set
            {
                _lives = value;
                _view.UpdateLivesText(_lives);

                if (value < _lives)
                {
                    LostLifeThisScenario = true;
                }
                
                if (Lives <= 0 && !RunEnded)
                {
                    _mediator.SendAsync(new ExitScenarioRequest(CurrentScenario, false, MarbleController.CurrentWave));
                }
            }
        }

        public int Honey
        {
            get => _honey;
            private set
            {
                _honey = value;
                _signalBus.Fire(new HoneyChangedSignal(_honey));
                _view.UpdateHoneyText(_honey);
            }
        }

        public ScenarioManager(IView view, Mediator mediator, SignalBus signalBus, TimeController timeController)
        {
            _view = view;
            _mediator = mediator;
            _signalBus = signalBus;
            _timeController = timeController;
            mediator.AddHandler<PurchaseRequest, bool>(this);
            signalBus.Subscribe<TowerSoldSignal>(OnTowerSold);
            signalBus.Subscribe<HoneyGeneratedSignal>(OnHoneyGenerated);
            signalBus.Subscribe<LivesGeneratedSignal>(OnLivesGenerated);
        }

        public void EnterState()
        {
            LostLifeThisScenario = false;
            RunEnded = false;
            Lives = 20;
            Honey = 1000;

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
        
        void OnHoneyGenerated(HoneyGeneratedSignal signal)
        {
            // Debug.Log($"{Honey} -> {Honey + signal.Honey}");
            Honey += signal.Honey;
        }
        
        void OnLivesGenerated(LivesGeneratedSignal signal)
        {
            Lives += signal.Lives;
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