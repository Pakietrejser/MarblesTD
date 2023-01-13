using DG.Tweening;
using MarblesTD.Towers;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public class BeehiveView : TowerView, IBeehiveView
    {
        [SerializeField] TMP_Text honeyText;

        protected override void Awake()
        {
            base.Awake();
            honeyText.alpha = 0;
        }

        public void ShowHoney(int text)
        {
            honeyText.text = $"+{text}$";
            honeyText.ChangeAlpha(1);
            honeyText.DOKill();
            honeyText.DOFade(0f, 0.4f).SetEase(Ease.InSine);
        }
    }
}