using System.Collections.Generic;
using DG.Tweening;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

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
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] Ease easeType = Ease.OutBounce;

        [Header("Upgrades")] 
        [SerializeField] List<TowerUpgrade> topUpgradesLeftToRight;
        [SerializeField] List<TowerUpgrade> botUpgradesLeftToRight;
        
        [Inject] ScenarioManager ScenarioManager { get; set; }

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
            
            towerName.text = ActiveTower.GetTranslatedName();
            towerIcon.sprite = ActiveTower.GetIcon();
            RefreshTowerUpgrades();

            transform.localPosition = new Vector3(transform.localPosition.x, -(540+258), 0);
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            transform.DOKill();
            transform.DOLocalMoveY(-540, .8f).SetEase(easeType);
            canvasGroup.DOFade(1f, 0.4f);
        }
        
        public void Hide()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, 0.2f).OnComplete(() => gameObject.SetActive(false));
            ActiveTower = null;
        }

        void RefreshTowerUpgrades()
        {
            if (ActiveTower == null) return;
            var upgrades = ActiveTower.Upgrades;
            botUpgradesLeftToRight[0].Init(ScenarioManager.Honey, ActiveTower, upgrades[UpgradePath.BotLeft]);
            botUpgradesLeftToRight[1].Init(ScenarioManager.Honey, ActiveTower, upgrades[UpgradePath.BotMid]);
            botUpgradesLeftToRight[2].Init(ScenarioManager.Honey, ActiveTower, upgrades[UpgradePath.BotRight]);
            topUpgradesLeftToRight[0].Init(ScenarioManager.Honey, ActiveTower, upgrades[UpgradePath.TopLeft], upgrades[UpgradePath.BotLeft]);
            topUpgradesLeftToRight[1].Init(ScenarioManager.Honey, ActiveTower, upgrades[UpgradePath.TopMid], upgrades[UpgradePath.BotMid]);
            topUpgradesLeftToRight[2].Init(ScenarioManager.Honey, ActiveTower, upgrades[UpgradePath.TopRight], upgrades[UpgradePath.BotRight]);
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(position, Vector2.down, 0.01f, 1 << towerMask);
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
            //TODO: do
        }

        void SellTower()
        {
            SignalBus.FireStatic(new TowerSoldSignal(ActiveTower.SellValue));
            ActiveTower.Destroy();
            Hide();
        }
    }
}