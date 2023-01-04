using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Marbles;
using UnityEngine;

namespace MarblesTD.Core.ScenarioSystems
{
    public class MarbleController : IUpdateState
    {
        public IEnumerable<Marble> Marbles => _marbleWaves.SelectMany(wave => wave.Marbles);
        
        readonly Marble.Pool _marblePool;
        readonly TimeController _timeController;
        readonly ScenarioManager _scenarioManager;
        readonly IView _view;
        readonly WaveProvider _waveProvider = new WaveProvider();
        readonly List<MarbleWave> _marbleWaves = new List<MarbleWave>();
        
        public MarbleController(Marble.Pool marblePool, SignalBus signalBus, TimeController timeController, ScenarioManager scenarioManager, IView view)
        {
            _marblePool = marblePool;
            _timeController = timeController;
            _scenarioManager = scenarioManager;
            _view = view;
            signalBus.Subscribe<MarbleWaveSpawnedSignal>(SpawnMarbleWave);
        }
        
        async void SpawnMarbleWave(MarbleWaveSpawnedSignal spawnedSignal)
        {
            var wave = _waveProvider.Next();
            var marbleWave = new MarbleWave(wave.HoneyReward);
            _marbleWaves.Add(marbleWave);

            foreach (var waveGroup in wave.GetGroups())
            {
                for (var i = 0; i < waveGroup.MarbleCount; i++)
                {
                    var go = UnityEngine.Object.Instantiate(spawnedSignal.MarblePrefab);
                    var view = go.GetComponent<IMarbleView>();
                    var marble = _marblePool.Spawn();
                    marble.Init(view, _view.GetStartPosition(), waveGroup.MarbleHealth, waveGroup.MarbleSpeed);
                    marbleWave.Add(marble);

                    await UniTask.WaitUntil(() => _timeController.TimeScale != 0f);
                    await UniTask.Delay(TimeSpan.FromSeconds(waveGroup.MarbleDelay / _timeController.TimeScale));
                }
            }

            marbleWave.FinishedSpawning = true;
        }

        public void Enter() {}

        public void UpdateState(float timeDelta)
        {
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
                            _scenarioManager.Honey += marbleWave.HoneyReward;
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

        public void Exit()
        {
            _waveProvider.Reset();
        }
        
        public interface IView
        {
            Vector2 GetStartPosition();
            Vector2 GetEndPosition();
            Vector2 GetPositionAtDistance(float distance);
            Quaternion GetRotationAtDistance(float distance);
        }
    }
}