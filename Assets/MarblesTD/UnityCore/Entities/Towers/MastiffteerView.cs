using DG.Tweening;
using MarblesTD.Towers.MastiffteerTower;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class MastiffteerView : TowerView, IMastiffteerView
    {
        [SerializeField] SpriteRenderer gunShotRenderer;
        
        public void ShowGunShot()
        {
            gunShotRenderer.color = Color.white;
            gunShotRenderer.DOKill();
            gunShotRenderer.DOFade(0f, 0.2f).SetEase(Ease.OutFlash);
        }
    }
}