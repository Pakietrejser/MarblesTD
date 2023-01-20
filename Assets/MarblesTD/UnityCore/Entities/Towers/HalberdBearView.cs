using DG.Tweening;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public class HalberdBearView : TowerView, IHalberdBearView
    {
        [SerializeField] SpriteRenderer rangeRenderer;
        [SerializeField] HalberdAttackView halberdAttackView;

        float _originalScale;

        protected override void Awake()
        {
            base.Awake();
            _originalScale = rangeRenderer.transform.localScale.x;
            rangeRenderer.enabled = false;
            ShowRangeCircle(2.5f);
        }
        
        public void Attack(HalberdBear owner, int damage, Vector2 target, int angle, float attackDuration)
        {
            var startPosition = owner.Position;
            var endPosition = Quaternion.Euler(Vector3.forward * -angle) * (target - startPosition) + new Vector3(startPosition.x, startPosition.y);
            bool spins = angle == 360;
            
            UpdateRotationUnless(target, spins);
            transform.DOKill();
            transform.DORotate(new Vector3(0, 0, transform.rotation.y - angle), attackDuration, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .OnComplete(() => UpdateRotationUnless(endPosition, spins));

            halberdAttackView.ActivateHalberdFor(attackDuration, owner, damage);
        }

        void UpdateRotationUnless(Vector2 rotateTarget, bool spins)
        {
            if (spins) return;
            UpdateRotation(rotateTarget);
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