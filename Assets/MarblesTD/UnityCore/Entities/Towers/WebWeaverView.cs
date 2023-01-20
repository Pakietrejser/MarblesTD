using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public class WebWeaverView : TowerView, IWebWeaverView
    {
        [SerializeField] SpriteRenderer rangeRenderer;
        [SerializeField] SpriteRenderer webRenderer;
        
        float _originalScale;
        
        protected override void Awake()
        {
            base.Awake();
            _originalScale = rangeRenderer.transform.localScale.x;
            rangeRenderer.enabled = false;
            webRenderer.enabled = false;
            ShowRangeCircle(2.4f);
        }

        public void ShowWeb(float range)
        {
            webRenderer.enabled = true;
            webRenderer.transform.localScale = Vector3.one * range * _originalScale;
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