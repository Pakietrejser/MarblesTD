using MarblesTD.Core.Player;
using MarblesTD.Core.Towers;
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

        [Header("Middle Box")] 
        [SerializeField] TMP_Text killCountText;
        
        Tower activeTower;
        Player activePlayer;

        void Awake()
        {
            sellTowerButton.onClick.AddListener(SellTower);
        }

        void SellTower()
        {
            activePlayer.AddMoney(activeTower.SellValue);
            activeTower.Destroy();
            HidePanel();
        }
        
        public void Init(Player player)
        {
            activePlayer = player;
        }

        public void ShowPanel(Tower tower)
        {
            if (tower == activeTower)
            {
                HidePanel();
                return;
            }
            
            activeTower = tower;
            towerNameText.text = activeTower.Name;
            towerIconImage.sprite = activeTower.Icon;
            
            gameObject.SetActive(true);
        }

        public void HidePanel()
        {
            gameObject.SetActive(false);
            activeTower = null;
        }

        public void UpdatePanel()
        {
            if (activeTower == null) return;

            killCountText.text = $"{activeTower.KIllCount}";
            sellTowerText.text = $"Sell for ${activeTower.SellValue}";
        }
    }
}