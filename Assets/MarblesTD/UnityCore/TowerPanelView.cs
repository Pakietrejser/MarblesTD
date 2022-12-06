using MarblesTD.Core.Players;
using MarblesTD.Core.Towers;
using MarblesTD.Core.Towers.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore
{
    public class TowerPanelView : MonoBehaviour
    {
        [Header("Left Box")] 
        [SerializeField] Button sellTowerButton;
        [SerializeField] TMP_Text sellTowerText;

        [Header("Right Box")] 
        [SerializeField] TMP_Text towerNameText;
        [SerializeField] Image towerIconImage;
        [Space] 
        [SerializeField] UpgradePathPanel leftPath;
        [SerializeField] UpgradePathPanel middlePath;
        [SerializeField] UpgradePathPanel rightPath;

        [Header("Middle Box")] 
        [SerializeField] TMP_Text killCountText;

        Tower _activeTower;

        Tower ActiveTower
        {
            get => _activeTower;
            set
            {
                _activeTower?.Unselect();
                _activeTower = value;
                _activeTower?.Select();
            }
        }
        Player activePlayer;

        void Awake()
        {
            sellTowerButton.onClick.AddListener(SellTower);
        }

        void SellTower()
        {
            activePlayer.AddMoney(ActiveTower.SellValue);
            ActiveTower.Destroy();
            HidePanel();
        }
        
        public void Init(Player player)
        {
            activePlayer = player;
        }

        public void ShowPanel(Tower tower)
        {
            if (tower.Upgrades == null) return;

            if (tower == ActiveTower)
            {
                HidePanel();
                return;
            }
            
            ActiveTower = tower;
            towerNameText.text = ActiveTower.Name;
            towerIconImage.sprite = ActiveTower.Icon;
            
            leftPath.Init(tower, tower.Upgrades[Path.Left]);
            middlePath.Init(tower, tower.Upgrades[Path.Middle]);
            rightPath.Init(tower, tower.Upgrades[Path.Right]);

            Debug.Log("opening");
            gameObject.SetActive(true);
        }

        public void HidePanel()
        {
            Debug.Log("hiding");
            gameObject.SetActive(false);
            ActiveTower = null;
        }

        public void UpdatePanel()
        {
            if (ActiveTower == null) return;

            killCountText.text = $"{ActiveTower.KIllCount}";
            sellTowerText.text = $"Sell for ${ActiveTower.SellValue}";
        }
    }
}