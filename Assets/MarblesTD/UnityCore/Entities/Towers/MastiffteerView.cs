using DG.Tweening;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public class MastiffteerView : TowerView, IMastiffteerView
    {
        [SerializeField] SpriteRenderer gunShotRenderer;

        protected override void Awake()
        {
            base.Awake();
            gunShotRenderer.DOFade(0, 0.001f);
        }

        public void ShowGunShot()
        {
            gunShotRenderer.color = Color.white;
            gunShotRenderer.DOKill();
            gunShotRenderer.DOFade(0f, 0.2f).SetEase(Ease.OutFlash);
        }
    }
}