using System;
using MarblesTD.Core.Projectiles;
using MarblesTD.Core.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore
{
    public class TowerView : MonoBehaviour, ITowerView
    {
        [SerializeField] private GameObject projectilePrefab;
        
        public event Action Clicked;

        public Projectile SpawnProjectile(ProjectileConfig config)
        {
            var go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var projectileView = go.GetComponent<IProjectileView>();
            var projectile = new Projectile(projectileView, new Vector2(transform.position.x, transform.position.z), config);

            Bootstrap.Instance.Projectiles.Add(projectile);
            return projectile;
        }

        void OnMouseDown()
        {
            Debug.Log("Click");
            Clicked?.Invoke();
        }
    }
}