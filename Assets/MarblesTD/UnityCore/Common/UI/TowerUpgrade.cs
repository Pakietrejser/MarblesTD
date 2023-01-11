﻿using System;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.UI
{
    public class TowerUpgrade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TMP_Text price;
        [SerializeField] TMP_Text description;
        [SerializeField] Image statusLock;
        [SerializeField] Image statusCheck;
        [SerializeField] Image highlight;
        [SerializeField] Button upgradeButton;
        
        Tower _activeTower;
        Upgrade _activeUpgrade;

        void Awake()
        {
            upgradeButton.onClick.AddListener(UpgradeClicked);
        }
        
        public void Init(Tower activeTower, Upgrade activeUpgrade, Upgrade requiredUpgrade = null)
        {
            _activeTower = activeTower;
            _activeUpgrade = activeUpgrade;

            price.text = activeUpgrade.Cost.ToString();
            description.text = activeUpgrade.Description;
            statusCheck.enabled = activeUpgrade.Applied;
            statusLock.enabled = requiredUpgrade is { Applied: false };
            statusCheck.ChangeAlpha(1f);
        }

        async void UpgradeClicked()
        {
            bool purchaseCompleted = await Mediator.Instance.SendAsync(new PurchaseRequest(_activeUpgrade.Cost));
            if (!purchaseCompleted) return;
            
            _activeUpgrade.Apply(_activeTower);
            statusCheck.enabled = _activeUpgrade.Applied;
            statusCheck.ChangeAlpha(1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            highlight.enabled = true;
            if (statusLock.enabled) return;
            statusCheck.enabled = true;
            statusCheck.ChangeAlpha(.3f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            highlight.enabled = true;
            if (statusLock.enabled) return;
            statusCheck.enabled = false;
            statusCheck.ChangeAlpha(1f);
        }
    }
}