using System.Linq;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Upgrades;
using MarblesTD.UnityCore.Systems.Scenario;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.UI
{
    public class UpgradePathPanel : MonoBehaviour
    {
        [Header("Owned")] 
        [SerializeField] TMP_Text latestOwnedUpgradeText;
        [SerializeField] Image[] ownedUpgradeGemImages;

        [Header("Next Upgrade")] 
        [SerializeField] TMP_Text nextUpgradeName;
        [SerializeField] Button nextUpgradeButton;
        [SerializeField] TMP_Text nextUpgradePrice;
        
        Tower _tower;
        Upgrade[] _upgrades;

        void Awake()
        {
            nextUpgradeButton.onClick.AddListener(OnUpgradeClicked);
        }

        void OnUpgradeClicked()
        {
            var nextUpgrade = _upgrades.FirstOrDefault(upgrade => upgrade.IsActive == false);
            if (nextUpgrade == null)
            {
                Debug.Log("error: no upgrade available");
            }
            else
            {
                if (Bootstrap.Instance.Player.Money < nextUpgrade.Cost)
                {
                    return;
                }
                    
                Bootstrap.Instance.Player.RemoveMoney(nextUpgrade.Cost);
                _tower.ApplyUpgrade(nextUpgrade);
            }
            
            UpdateOwned();
            UpdateNextUpgrade();
        }

        public void Init(Tower tower, Upgrade[] upgrades)
        {
            _tower = tower;
            _upgrades = upgrades;

            UpdateOwned();
            UpdateNextUpgrade();
        }

        void UpdateNextUpgrade()
        {
            var nextUpgrade = _upgrades.FirstOrDefault(upgrade => upgrade.IsActive == false);
            if (nextUpgrade == null)
            {
                nextUpgradeButton.gameObject.SetActive(false);
            }
            else
            {
                nextUpgradeName.text = nextUpgrade.Name;
                nextUpgradePrice.text = nextUpgrade.Cost.ToString();
                nextUpgradeButton.gameObject.SetActive(true);
            }
        }

        void UpdateOwned()
        {
            int upgradesLength = _upgrades.Length;
            int ownedUpgradesLength = _upgrades.Count(x => x.IsActive);

            var i = 0;
            for (; i < ownedUpgradesLength; i++)
            {
                ownedUpgradeGemImages[i].gameObject.SetActive(true);
                ownedUpgradeGemImages[i].color = Color.white;
                latestOwnedUpgradeText.text = _upgrades[i].Name;
            }
            for (; i < upgradesLength; i++)
            {
                ownedUpgradeGemImages[i].gameObject.SetActive(true);
                ownedUpgradeGemImages[i].color = new Color(0.42f, 0.42f, 0.42f);
            }
            for (; i < ownedUpgradeGemImages.Length; i++)
            {
                ownedUpgradeGemImages[i].gameObject.SetActive(false);
            }
        }
    }
}