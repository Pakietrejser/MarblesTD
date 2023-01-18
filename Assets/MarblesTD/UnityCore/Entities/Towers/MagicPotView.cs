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

        public void ShowBurn(float range)
        {
            fireRenderer.enabled = true;
            fireRenderer.transform.localScale = Vector3.one * range * _originalScale;
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