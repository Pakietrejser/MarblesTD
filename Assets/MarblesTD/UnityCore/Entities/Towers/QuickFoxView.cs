using MarblesTD.Core.Entities.Towers.Projectiles;
using MarblesTD.Core.ScenarioSystems;
using MarblesTD.Towers.QuickFoxTower;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class QuickFoxView : TowerView, IQuickFoxView
    {
        [SerializeField] GameObject arrowPrefab;
        
        public Projectile SpawnProjectile(ProjectileConfig config)
        {
            var go = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            var projectileView = go.GetComponent<IProjectileView>();
            var projectile = new Projectile(projectileView, new Vector2(transform.position.x, transform.position.y), config);

            TowerController.ActiveProjectiles.Add(projectile);
            return projectile;
        }
    }
}