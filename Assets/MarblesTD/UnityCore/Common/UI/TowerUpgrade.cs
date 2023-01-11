using System;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
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
        [SerializeField] ButtonAudio buttonAudio;

        public event Action TowerUpgraded;
        
        Tower _activeTower;
        Upgrade _activeUpgrade;

        void Awake()
        {
            upgradeButton.onClick.AddListener(UpgradeClicked);
        }
        
        void Start()
        {
            SignalBus.Instance.Subscribe<HoneyChangedSignal>(OnHoneyChanged);
        }

        void OnHoneyChanged(HoneyChangedSignal signal)
        {
            if (_activeUpgrade == null) return;
            price.color = signal.Honey >= _activeUpgrade.Cost && !statusLock.enabled ? Color.black : new Color(0.87f, 0.23f, 0.28f);
        }
        
        public void Init(int honey, Tower activeTower, Upgrade activeUpgrade, Upgrade requiredUpgrade = null)
        {
            _activeTower = activeTower;
            _activeUpgrade = activeUpgrade;

            price.text = $"${_activeUpgrade.Cost}";
            price.enabled = !_activeUpgrade.Applied && _activeTower.RemainingUpgrades > 0;
            description.text = _activeUpgrade.Description;
            statusCheck.enabled = _activeUpgrade.Applied;
            statusLock.enabled = requiredUpgrade is { Applied: false } || _activeTower.RemainingUpgrades == 0 && !_activeUpgrade.Applied;
            buttonAudio.IsActive = !statusLock.enabled && !activeUpgrade.Applied;
            price.color = honey >= _activeUpgrade.Cost && !statusLock.enabled ? Color.black : new Color(0.87f, 0.23f, 0.28f);
            statusCheck.ChangeAlpha(1f);
        }

        async void UpgradeClicked()
        {
            if (statusLock.enabled) return;
            if (_activeTower.RemainingUpgrades <= 0) return;
            bool purchaseCompleted = await Mediator.Instance.SendAsync(new PurchaseRequest(_activeUpgrade.Cost));
            if (!purchaseCompleted) return;
            _activeUpgrade.Apply(_activeTower);
            TowerUpgraded?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            highlight.enabled = true;
            if (statusLock.enabled || _activeUpgrade.Applied || statusCheck.enabled) return;
            statusCheck.enabled = true;
            statusCheck.ChangeAlpha(.3f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            highlight.enabled = false;
            if (statusLock.enabled || _activeUpgrade.Applied) return;
            statusCheck.enabled = false;
            statusCheck.ChangeAlpha(1f);
        }
    }
}