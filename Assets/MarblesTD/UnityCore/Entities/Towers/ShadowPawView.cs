using MarblesTD.Core.Entities.Towers;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public class ShadowPawView : TowerView, IShadowPawView
    {
        [SerializeField] SpriteRenderer rangeRenderer;
        [SerializeField] MeleeAttackView meleeAttack;
        
        float _originalScale;
        
        protected override void Awake()
        {
            base.Awake();
            _originalScale = rangeRenderer.transform.localScale.x;
            rangeRenderer.enabled = false;
            ShowRangeCircle(2f);
        }
        
        public void Strike(Tower owner, int damage, int hits, float attackDuration, bool poisonous)
        {
            meleeAttack.Init(owner, damage, hits, attackDuration, poisonous);
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