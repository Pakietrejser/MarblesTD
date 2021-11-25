using MarblesTD.Core.Player;
using MarblesTD.Towers;
using TMPro;
using UnityEngine;

namespace MarblesTD.UnityCore
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] TMP_Text playerLivesText;
        [SerializeField] TMP_Text playerMoneyText;
        
        public void UpdateLives(int lives)
        {
            playerLivesText.text = $"{lives}";
        }

        public void UpdateMoney(int money)
        {
            playerMoneyText.text = $"${money}";
        }
    }
}