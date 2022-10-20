using System.Collections;
using System.Collections.Generic;
using MarblesTD.Core.Marbles;
using MarblesTD.Core.Waves;
using MarblesTD.UnityCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD
{
    [RequireComponent(typeof(Button))]
    public class SpawnMarbleWaveButton : MonoBehaviour
    {
        [SerializeField] TMP_Text roundText;
        [SerializeField] GameObject marblePrefab;

        readonly WaveManager _waveManager = new WaveManager();
        Button spawnButton;

        void Awake()
        {
            roundText.text = "0";
            spawnButton = GetComponent<Button>();
            spawnButton.onClick.AddListener(() => StartCoroutine(SpawnWave()));
        }

        IEnumerator SpawnWave()
        {
            var wave = _waveManager.GetNextWave();
            roundText.text = $"{wave.WaveIndex}";
            Bootstrap.Instance.MarbleWaves.Add(wave.WaveIndex, new List<Marble>());

            foreach (var waveGroup in wave.GetGroups())
            {
                for (var i = 0; i < waveGroup.MarbleCount; i++)
                {
                    var spawnPosition = Bootstrap.Instance.StartingPosition;
                    var go = Instantiate(marblePrefab);
                    
                    var view = go.GetComponent<IMarbleView>();
                    var marble = new Marble(view, new Vector2(spawnPosition.x, spawnPosition.z), waveGroup.MarbleHealth, waveGroup.MarbleSpeed);
                    Bootstrap.Instance.MarbleWaves[wave.WaveIndex].Add(marble);
                    
                    yield return new WaitForSeconds(waveGroup.MarbleDelay);
                }
            }
        }
    }
}