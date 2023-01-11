using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
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
    public class TowerPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text towerName;
        [SerializeField] Image towerIcon;
        [Space] 
        [SerializeField] TMP_Text targetText;
        [SerializeField] TMP_Text killedText;
        [SerializeField] Button switchTargetButton;
        [Space]
        [SerializeField] TMP_Text remainingUpgrades;
        [SerializeField] TMP_Text sellForText;
        [SerializeField] Button sellButton;
        [Space]
        [SerializeField] int towerMask;

        [Header("Upgrades")] 
        [SerializeField] List<TowerUpgrade> topUpgradesLeftToRight;
        [SerializeField] List<TowerUpgrade> botUpgradesLeftToRight;

        Tower _activeTower;
        Tower ActiveTower
        {
            get => _activeTower;
            set
            {
                _activeTower?.Deselect();
                _activeTower = value;
                _activeTower?.Select();
            }
        }

        void Awake()
        {
            switchTargetButton.onClick.AddListener(SwitchTarget);
            sellButton.onClick.AddListener(SellTower);
            gameObject.SetActive(false);
            topUpgradesLeftToRight.ForEach(x => x.TowerUpgraded += RefreshTowerUpgrades);
            botUpgradesLeftToRight.ForEach(x => x.TowerUpgraded += RefreshTowerUpgrades);
        }

        public void Show(Tower tower)
        {
            if (tower.Upgrades == null) return;
            if (tower.Upgrades.Count == 0) return;
            if (tower == ActiveTower)
            {
                Hide();
                return;
            }
            ActiveTower = tower;
            
            towerName.text = ActiveTower.TranslatedName;
            towerIcon.sprite = ActiveTower.GetIcon();
            RefreshTowerUpgrades();

            gameObject.SetActive(true);
        }
        
        void RefreshTowerUpgrades()
        {
            if (ActiveTower == null) return;
            var upgrades = ActiveTower.Upgrades;
            botUpgradesLeftToRight[0].Init(ActiveTower, upgrades[UpgradePath.BotLeft]);
            botUpgradesLeftToRight[1].Init(ActiveTower, upgrades[UpgradePath.BotMid]);
            botUpgradesLeftToRight[2].Init(ActiveTower, upgrades[UpgradePath.BotRight]);
            topUpgradesLeftToRight[0].Init(ActiveTower, upgrades[UpgradePath.TopLeft], upgrades[UpgradePath.BotLeft]);
            topUpgradesLeftToRight[1].Init(ActiveTower, upgrades[UpgradePath.TopMid], upgrades[UpgradePath.BotMid]);
            topUpgradesLeftToRight[2].Init(ActiveTower, upgrades[UpgradePath.TopRight], upgrades[UpgradePath.BotRight]);
        }

        void Hide()
        {
            gameObject.SetActive(false);
            ActiveTower = null;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(position, Vector2.down, 100, towerMask);
                if (hit.collider == null)
                {
                    Hide();
                }
            }
            
            if (ActiveTower == null) return;

            killedText.text = $"{ActiveTower.MarblesKilled}";
            sellForText.text = $"Sprzedaj za za ${ActiveTower.SellValue}";
            remainingUpgrades.text = $"Pozostałe ulepszenia: {ActiveTower.RemainingUpgrades}";
        }
        
        void SwitchTarget()
        {
            throw new System.NotImplementedException();
        }

        void SellTower()
        {
            SignalBus.FireStatic(new TowerSoldSignal(ActiveTower.SellValue));
            ActiveTower.Destroy();
            Hide();
        }
    }
}