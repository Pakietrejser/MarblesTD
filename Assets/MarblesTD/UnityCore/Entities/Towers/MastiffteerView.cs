using DG.Tweening;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public class MastiffteerView : TowerView, IMastiffteerView
    {
        [SerializeField] SpriteRenderer gunShotRenderer;

        void Start()
        {
            gunShotRenderer.enabled = false;
        }

        public void ShowGunShot()
        {
            gunShotRenderer.enabled = true;
            gunShotRenderer.color = Color.white;
            gunShotRenderer.DOKill();
            gunShotRenderer.DOFade(0f, 0.2f).SetEase(Ease.OutFlash);
        }
    }
}