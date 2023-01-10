using System;
using MarblesTD.Core.ScenarioSystems;
using PathCreation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Systems.ScenarioSystems
{
    public class MarbleControllerView : MonoBehaviour, MarbleController.IView
    {
        [SerializeField] TMP_Text roundText;
        [SerializeField] GameObject marblePrefab;
        [SerializeField] Button nextWaveButton;
        [SerializeField] TMP_Text nextWaveButtonText;
        
        [Header("Runtime")]
        public PathCreator[] PathCreators;
        
        public event Action NextWaveRequested;

        void Awake()
        {
            roundText.text = "0";
            nextWaveButton.onClick.AddListener(() => NextWaveRequested?.Invoke());
        }

        public void ToggleWaveRequest(bool enable)
        {
            nextWaveButton.interactable = enable;
            nextWaveButtonText.text = enable ? "Rozpocznij rundę" : "Wysyłanie marbli...";
        }

        public GameObject GetMarblePrefab() => marblePrefab;
        
        public void SetWaveString(int currentWave, int lastWave)
        {
            roundText.text = $"{currentWave}/{lastWave}";
        }

        public Vector2 GetStartPosition()
        {
            return PathCreators[0].path.GetPointAtTime(0, EndOfPathInstruction.Stop);
        }

        public Vector2 GetEndPosition()
        {
            return PathCreators[0].path.GetPointAtTime(1, EndOfPathInstruction.Stop);
        }

        public Vector2 GetPositionAtDistance(float distance)
        {
            return PathCreators[0].path.GetPointAtDistance(distance, EndOfPathInstruction.Stop);
        }

        public Quaternion GetRotationAtDistance(float distance)
        {
            return PathCreators[0].path.GetRotationAtDistance(distance, EndOfPathInstruction.Stop);
        }
    }
}