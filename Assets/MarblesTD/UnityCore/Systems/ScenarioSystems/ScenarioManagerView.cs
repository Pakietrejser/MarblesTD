using MarblesTD.Core.ScenarioSystems;
using TMPro;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.ScenarioSystems
{
    public class ScenarioManagerView : MonoBehaviour, ScenarioManager.IView
    {
        [SerializeField] TMP_Text livesText; 
        [SerializeField] TMP_Text honeyText;

        public void UpdateLivesText(int lives) =>  livesText.text = lives.ToString();
        public void UpdateHoneyText(int honey) => honeyText.text = honey.ToString();
    }
}