﻿using System;
using MarblesTD.Core.ScenarioSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Systems.ScenarioSystems
{
    public class TimeControllerView : MonoBehaviour, TimeController.IView
    {
        [SerializeField] TMP_Text text;
        [SerializeField] Button changeSpeedButton;

        public event Action RequestChangeSpeed;
        
        void Awake()
        {
            changeSpeedButton.onClick.AddListener(() => RequestChangeSpeed?.Invoke());
        }
        
        public void UpdateTimeScale(float timeScale)
        {
            text.text = $"{timeScale}x";
        }
    }
}