using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Common.Systems;
using MarblesTD.Core.Players;
using MarblesTD.Core.Waves;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MarblesTD.Core.Marbles
{
    public class MarbleController : GameSystem
    {
        public IEnumerable<Marble> Marbles => _marbleWaves.SelectMany(wave => wave.Marbles);
        public Vector3 SpawnPosition;
        
        readonly Marble.Pool _marblePool;
        readonly WaveProvider _waveProvider = new WaveProvider();
        readonly List<MarbleWave> _marbleWaves = new List<MarbleWave>();
        
        
        public MarbleController(Marble.Pool marblePool, SignalBus signalBus)
        {
            _marblePool = marblePool;
            signalBus.Subscribe<MarbleWaveBeginSpawnSignal>(SpawnMarbleWave);
        }

        public override void OnEnter()
        {
            _waveProvider.Reset();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var marbleWave in _marbleWaves)
            {
                int wave = marbleWave.WaveIndex;
                var marbles = marbleWave.Marbles;

                for (int i = marbles.Count - 1; i >= 0; i--)
                {
                    if (marbles[i].IsDestroyed)
                    {
                        _marblePool.Despawn(marbles[i]);
                        marbles.Remove(marbles[i]);

                        if (marbles.Count == 0 && marbleWave.FinishedSpawning)
                        {
                            Player.Instance.AddMoney(50 + wave * 20);
                        }
                    }
                }
            }
        }

        async void SpawnMarbleWave(MarbleWaveBeginSpawnSignal signal)
        {
            var wave = _waveProvider.Next();
            var marbleWave = new MarbleWave(wave.WaveIndex);
            _marbleWaves.Add(marbleWave);

            foreach (var waveGroup in wave.GetGroups())
            {
                for (var i = 0; i < waveGroup.MarbleCount; i++)
                {
                    var go = Object.Instantiate(signal.MarblePrefab);
                    var view = go.GetComponent<IMarbleView>();
                    var marble = _marblePool.Spawn();
                    marble.Init(view, SpawnPosition, waveGroup.MarbleHealth, waveGroup.MarbleSpeed);
                    marbleWave.Add(marble);

                    await UniTask.Delay(TimeSpan.FromSeconds(waveGroup.MarbleDelay));
                }
            }

            marbleWave.FinishedSpawning = true;
        }
    }
}