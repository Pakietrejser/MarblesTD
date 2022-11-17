﻿using System.Collections;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Systems.Waves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore
{
    [RequireComponent(typeof(Button))]
    public class SpawnMarbleWaveButton : MonoBehaviour
    {
        [SerializeField] TMP_Text roundText;
        [SerializeField] GameObject marblePrefab;

        readonly WaveManager _waveManager = new WaveManager();
        Button _spawnButton;

        void Awake()
        {
            roundText.text = "0";
            _spawnButton = GetComponent<Button>();
            _spawnButton.onClick.AddListener(() => StartCoroutine(SpawnWave()));
        }

        IEnumerator SpawnWave()
        {
            var wave = _waveManager.GetNextWave();
            var marbleWave = new MarbleWave(wave.WaveIndex);
            
            roundText.text = $"{wave.WaveIndex}";
            Bootstrap.Instance.MarbleWaves.Add(marbleWave);

            foreach (var waveGroup in wave.GetGroups())
            {
                for (var i = 0; i < waveGroup.MarbleCount; i++)
                {
                    var spawnPosition = Bootstrap.Instance.StartingPosition;
                    
                    var go = Instantiate(marblePrefab);
                    var view = go.GetComponent<IMarbleView>();
                    var marble = MarbleController.GetFreshMarbleTest();
                    marble.Init(view, new Vector2(spawnPosition.x, spawnPosition.z), waveGroup.MarbleHealth, waveGroup.MarbleSpeed);
                    marbleWave.Add(marble);
                    
                    yield return new WaitForSeconds(waveGroup.MarbleDelay);
                }
            }

            marbleWave.FinishedSpawning = true;
        }
    }
}