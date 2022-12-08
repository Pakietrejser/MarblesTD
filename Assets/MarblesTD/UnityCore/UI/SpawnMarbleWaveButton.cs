using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.UI
{
    [RequireComponent(typeof(Button))]
    public class SpawnMarbleWaveButton : MonoBehaviour
    {
        [SerializeField] TMP_Text roundText;
        [SerializeField] GameObject marblePrefab;

        Button _spawnButton;

        int _wave = 0;

        void Awake()
        {
            roundText.text = "0";
            _spawnButton = GetComponent<Button>();
            _spawnButton.onClick.AddListener(OnSpawnButtonClicked);
        }

        void OnSpawnButtonClicked()
        {
            roundText.text = $"{++_wave}";
            SignalBus.FireStatic(new MarbleWaveSpawnedSignal(marblePrefab));
        }
    }
}