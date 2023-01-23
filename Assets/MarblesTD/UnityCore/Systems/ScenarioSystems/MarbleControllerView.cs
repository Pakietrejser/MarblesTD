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
        public float[] PathDistributions { get; set; }
        
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

        public Vector2 GetStartPosition(int pathIndex)
        {
            return PathCreators[pathIndex].path.GetPointAtTime(0, EndOfPathInstruction.Stop);
        }

        public Vector2 GetEndPosition(int pathIndex)
        {
            return PathCreators[pathIndex].path.GetPointAtTime(1, EndOfPathInstruction.Stop);
        }

        public Vector2 GetPositionAtDistance(int pathIndex, float distance)
        {
            return PathCreators[pathIndex].path.GetPointAtDistance(distance, EndOfPathInstruction.Stop);
        }

        public Quaternion GetRotationAtDistance(int pathIndex, float distance)
        {
            return PathCreators[pathIndex].path.GetRotationAtDistance(distance, EndOfPathInstruction.Stop);
        }
    }
}