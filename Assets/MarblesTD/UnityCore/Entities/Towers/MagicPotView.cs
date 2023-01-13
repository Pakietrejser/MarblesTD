using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public class MagicPotView : TowerView, IMagicPotView
    {
        [SerializeField] SpriteRenderer rangeRenderer;
        [SerializeField] SpriteRenderer fireRenderer;
        
        float _originalScale;
        
        protected override void Awake()
        {
            base.Awake();
            _originalScale = rangeRenderer.transform.localScale.x;
            rangeRenderer.enabled = false;
            fireRenderer.enabled = false;
            ShowRangeCircle(2f);
        }

        public async void ShowBurn(float range)
        {
            fireRenderer.transform.localScale = Vector3.one * _originalScale;
            fireRenderer.enabled = true;

            fireRenderer.transform.DOKill();
            fireRenderer.transform.DOScale(Vector3.one * range * _originalScale, 0.15f).SetEase(Ease.Flash);
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            fireRenderer.transform.DOScale(Vector3.one * _originalScale, 0.05f).SetEase(Ease.Flash)
                .OnComplete(() => fireRenderer.enabled = false);
        }

        public void ShowRangeCircle(float range)
        {
            rangeRenderer.transform.localScale = Vector3.one * range * _originalScale;
            rangeRenderer.enabled = true;
        }

        public void HideRangeCircle()
        {
            rangeRenderer.enabled = false;
        }
    }
}