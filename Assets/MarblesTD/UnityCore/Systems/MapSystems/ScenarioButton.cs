using System.Collections.Generic;
using Cinemachine;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Extensions;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.MapSystems;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Systems.MapSystems
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
        [SerializeField] Sprite starLocked;
        [SerializeField] Sprite starUnlocked;
        [SerializeField] Button scenarioButton;
        [Space] 
        [SerializeField] ScenarioID id;
        [SerializeField] List<ScenarioButton> targets;

        public ScenarioID ID => id;
        
        static ScenarioButton ActiveScenarioButton;
        
        Scenario _scenario;
        Mediator _mediator;
        readonly List<ArrowButton> _arrowButtons = new List<ArrowButton>();

        void Awake()
        {
            scenarioButton.onClick.AddListener(ScenarioStartedClicked);
            
            lockedBox.SetActive(true);
            
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

        async void ScenarioStartedClicked()
        {
            bool started = await _mediator.SendAsync(new StartScenarioRequest(_scenario));
        }

        public void Init(Scenario scenario, Mediator mediator)
        {
            _scenario = scenario;
            _mediator = mediator;
            pathImage.sprite = scenario.ID.GetPathSprite();
        }

        public void UpdateButton()
        {
            scenarioText.text = id.GetName();

            for (var i = 0; i < stars.Length; i++)
            {
                bool unlocked = _scenario.GetQuestCompletion(i);
                stars[i].sprite = unlocked ? starUnlocked : starLocked;
                stars[i].ChangeAlpha(unlocked ? 1f : 0.8f);
            }

            if (true)
            // if (_scenario.Completed)
            {
                targets.ForEach(button => button.lockedBox.SetActive(false));
            }
            
            if (id == ScenarioID.HelloWorld)
            {
                lockedBox.SetActive(false);
                SetAsActive();
            }
        }

        void SetAsActive()
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
        
        void OnArrowClicked(ScenarioButton scenarioButton)
        {
            scenarioButton.SetAsActive();
        }
    }
}