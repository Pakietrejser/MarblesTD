using MarblesTD.Core.Entities.Towers.Projectiles;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public class CannonBoarView : TowerView, ICannonBoarView
    {
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] SpriteRenderer rangeRenderer;
        [SerializeField] Transform bulletSpawnPoint;
        
        float _originalScale;
        
        protected override void Awake()
        {
            base.Awake();
            _originalScale = rangeRenderer.transform.localScale.x;
            rangeRenderer.enabled = false;
            ShowRangeCircle(4f);
        }

        public Projectile SpawnProjectile(ProjectileConfig config)
        {
            var go = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            var projectileView = go.GetComponent<IProjectileView>();
            var projectile = new Projectile(projectileView, new Vector2(bulletSpawnPoint.position.x, bulletSpawnPoint.position.y), config);

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