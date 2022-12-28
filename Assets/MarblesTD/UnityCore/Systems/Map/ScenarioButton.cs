using System.Collections.Generic;
using Cinemachine;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Systems.Map
{
    public class ScenarioButton : MonoBehaviour
    {
        [SerializeField] ArrowButton arrowPrefab;
        [SerializeField] Collider2D arrowBoundsCollider;
        [SerializeField] CinemachineVirtualCamera virtualCamera;
        [Space] 
        [SerializeField] TMP_Text scenarioText;
        [SerializeField] Image backgroundImage;
        [SerializeField] Image pathImage;
        [SerializeField] Image[] stars;
        [SerializeField] GameObject lockedBox;
        [Space]
        [SerializeField] List<ScenarioButton> targets;

        static ScenarioButton ActiveScenarioButton;
        
        readonly List<ArrowButton> _arrowButtons = new List<ArrowButton>();
        
        void Awake()
        {
            foreach (var target in targets)
            {
                if (!target.targets.Contains(this))
                {
                    target.targets.Add(this);
                    target.Awake();
                }
                
                var targetPosition = target.transform.position;
                var closestPoint = arrowBoundsCollider.ClosestPoint(targetPosition);
                var arrowButton = Instantiate(arrowPrefab, closestPoint, Quaternion.identity, transform);
                
                arrowButton.transform.Rotate2D(targetPosition);
                arrowButton.Init(target);
                arrowButton.ArrowClicked += OnArrowClicked;
                _arrowButtons.Add(arrowButton);
            }
            
            SetAsInactive();
        }

        void OnArrowClicked(ScenarioButton scenarioButton)
        {
            scenarioButton.SetAsActive();
        }

        public void SetAsActive()
        {
            ActiveScenarioButton?.SetAsInactive();
            ActiveScenarioButton = this;
            
            virtualCamera.Priority = 1;
            _arrowButtons.ForEach(button => button.Show());
        }

        void SetAsInactive()
        {
            virtualCamera.Priority = 0;
            _arrowButtons.ForEach(button => button.Hide());
        }
    }
}