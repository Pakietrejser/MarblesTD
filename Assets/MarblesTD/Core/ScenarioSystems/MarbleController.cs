using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Marbles;
using UnityEngine;

namespace MarblesTD.Core.ScenarioSystems
{
    public class MarbleController : IUpdateState
    {
        public IEnumerable<Marble> Marbles => _marbleWaves.SelectMany(wave => wave.Marbles);

        const int LastWave = 20;
        public static int CurrentWave;

        readonly Marble.Pool _marblePool;
        readonly TimeController _timeController;
        readonly ScenarioManager _scenarioManager;
        readonly IView _view;
        readonly Mediator _mediator;
        readonly SignalBus _signalBus;
        WaveProvider _waveProvider;
        readonly List<MarbleWave> _marbleWaves = new List<MarbleWave>();

        public static bool ProcessingRound { get; private set; }
        CancellationTokenSource _cts;
        
        public MarbleController(Marble.Pool marblePool, TimeController timeController, ScenarioManager scenarioManager, IView view, Mediator mediator, SignalBus signalBus)
        {
            _marblePool = marblePool;
            _timeController = timeController;
            _scenarioManager = scenarioManager;
            _view = view;
            _mediator = mediator;
            _signalBus = signalBus;
            _view.NextWaveRequested += OnNextWaveRequested;
        }

        async void OnNextWaveRequested()
        {
            if (ProcessingRound) return;
            ProcessingRound = true;
            _view.ToggleWaveRequest(false);
            try
            {
                _signalBus.Fire(new RoundStartedSignal());
                var marbleWave = await SpawnMarbleWave();
                CurrentWave = marbleWave.WaveIndex;
                Debug.Log($"Finished spawning wave {CurrentWave}");
                if (CurrentWave == LastWave) return;
                _view.ToggleWaveRequest(true);
                ProcessingRound = false;
            }
            catch (OperationCanceledException ex)
            {
                Debug.Log("Stop spawning waves...");
            }
        }

        async UniTask<MarbleWave> SpawnMarbleWave()
        {
            var wave = _waveProvider.Next();
            var marbleWave = new MarbleWave(wave.HoneyReward, _waveProvider.CurrentWave);
            var prefab = _view.GetMarblePrefab();
            _marbleWaves.Add(marbleWave);
            _view.SetWaveString(_waveProvider.CurrentWave, LastWave);

            foreach (var waveGroup in wave.GetGroups())
            {
                for (var i = 0; i < waveGroup.MarbleCount; i++)
                {
                    var go = UnityEngine.Object.Instantiate(prefab);
                    var view = go.GetComponent<IMarbleView>();
                    var marble = _marblePool.Spawn();
                    marble.Init(view, _view.GetStartPosition(), waveGroup.MarbleHealth, waveGroup.MarbleSpeed);
                    marbleWave.Add(marble);

                    await UniTask.WaitUntil(() => _timeController.TimeScale != 0f, default, _cts.Token);
                    await UniTask.Delay(TimeSpan.FromSeconds(waveGroup.MarbleDelay / _timeController.TimeScale), false, PlayerLoopTiming.Update, _cts.Token);
                }
            }

            marbleWave.FinishedSpawning = true;
            return marbleWave;
        }

        public void EnterState()
        {
            _cts = new CancellationTokenSource();
            
            _waveProvider = new WaveProvider();
            ProcessingRound = false;
            _waveProvider.Reset();
            _view.ToggleWaveRequest(true);
            _view.SetWaveString(_waveProvider.CurrentWave, LastWave);
        }

        public async void UpdateState(float timeDelta)
        {
            if (_scenarioManager.RunEnded) return;
            
            foreach (var marbleWave in _marbleWaves)
            {
                var marbles = marbleWave.Marbles;

                for (int i = marbles.Count - 1; i >= 0; i--)
                {
                    var marble = marbles[i];

                    if (marble.IsDestroyed)
                    {
                        _marblePool.Despawn(marble);
                        marbles.Remove(marble);

                        if (marbles.Count == 0 && marbleWave.FinishedSpawning)
                        {
                            _signalBus.Fire(new HoneyGeneratedSignal(marbleWave.HoneyReward));
                            _signalBus.Fire(new RoundEndedSignal(marbleWave.HoneyReward));
                            
                            if (marbleWave.WaveIndex >= LastWave)
                            {
                                _scenarioManager.RunEnded = true;
                                await _mediator.SendAsync(new ExitScenarioRequest(_scenarioManager.CurrentScenario, true, CurrentWave));
                                return;
                            }
                        }
                    }
                    else
                    {
                        float distanceTravelled = marble.DistanceTravelled + marble.Speed * timeDelta;
                        var position = _view.GetPositionAtDistance(distanceTravelled);
                        var rotation = _view.GetRotationAtDistance(distanceTravelled);
                        bool reachedDestination = position == _view.GetEndPosition();
                        marble.Update(distanceTravelled, position, rotation, reachedDestination, _timeController.TimeScale);
                        if (reachedDestination)
                        {
                            _scenarioManager.Lives -= marble.Health;
                        }
                    }
                }
            }
        }

        public void ExitState()
        {
            _cts.Cancel();
            foreach (var marbleWave in _marbleWaves)
            foreach (var marble in marbleWave.Marbles)
            {
                marble.Destroy();
                _marblePool.Despawn(marble);
            }
            _marbleWaves.Clear();
        }
        
        public interface IView
        {
            event Action NextWaveRequested;
            void SetWaveString(int currentWave, int lastWave);
            void ToggleWaveRequest(bool enable); 
            GameObject GetMarblePrefab();
            Vector2 GetStartPosition();
            Vector2 GetEndPosition();
            Vector2 GetPositionAtDistance(float distance);
            Quaternion GetRotationAtDistance(float distance);
        }
    }
}