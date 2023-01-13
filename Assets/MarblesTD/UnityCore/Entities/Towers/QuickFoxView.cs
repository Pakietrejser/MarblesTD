using System;
using MarblesTD.Core.Entities.Towers.Projectiles;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public class QuickFoxView : TowerView, IQuickFoxView
    {
        [SerializeField] GameObject arrowPrefab;
        [SerializeField] SpriteRenderer rangeRenderer;

        float _originalScale;
        
        protected override void Awake()
        {
            base.Awake();
            _originalScale = rangeRenderer.transform.localScale.x;
            rangeRenderer.enabled = false;
            ShowRangeCircle(3.5f);
        }

        public Projectile SpawnProjectile(ArrowConfig config)
        {
            var go = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            var projectileView = go.GetComponent<IProjectileView>();
            var projectile = new Projectile(projectileView, new Vector2(transform.position.x, transform.position.y), config);

            TowerController.ActiveProjectiles.Add(projectile);
            return projectile;
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