using System;
using DG.Tweening;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;

namespace MarblesTD.UnityCore.Common.UI
{
    public class TowerInfoBox : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] TMP_Text towerName;
        [SerializeField] TMP_Text towerDescription;
        [SerializeField] TMP_Text typeDescription;

        void Awake()
        {
            canvasGroup.alpha = 0;
        }

        public void Show(Tower tower)
        {
            string type = tower.AnimalType switch
            {
                AnimalType.WildAnimal => "Dziki",
                AnimalType.NobleAnimal => "Zacny",
                AnimalType.NightAnimal => "Nocny",
                _ => throw new ArgumentOutOfRangeException()
            };
            
            towerName.text = tower.GetTranslatedName();
            towerDescription.text = tower.GetTranslatedDescription();
            typeDescription.text = $"{type} sojusznik.";
            
            canvasGroup.alpha = 0;
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, 0.4f);
        }

        public void Hide()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, 0.2f);
        }
    }
}