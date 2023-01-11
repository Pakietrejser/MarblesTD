using System;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Projectiles;
using MarblesTD.Core.ScenarioSystems;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class TowerView : MonoBehaviour, Tower.IView
    {
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] SpriteRenderer selectRenderer;
        [SerializeField] Collider2D towerCollider;

        public Collider2D Collider => towerCollider;

        SpriteRenderer[] _renderers;
        void Awake()
        {
            _renderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public event Action Clicked;

        public void Select()
        {
            selectRenderer.enabled = true;
        }

        public void Deselect()
        {
            selectRenderer.enabled = false;
        }

        public Projectile SpawnProjectile(ProjectileConfig config)
        {
            var go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var projectileView = go.GetComponent<IProjectileView>();
            var projectile = new Projectile(projectileView, new Vector2(transform.position.x, transform.position.y), config);

            TowerController.ActiveProjectiles.Add(projectile);
            return projectile;
        }

        void OnMouseDown()
        {
            Clicked?.Invoke();
        }
        
        public void DestroySelf() => Destroy(gameObject);
        
        public void EnableCollider()
        {
            towerCollider.enabled = true;
        }

        public void DisableCollider()
        {
            towerCollider.enabled = false;
        }

        public void ShowAsPlaceable(bool canBePlaced)
        {
            foreach (var spriteRenderer in _renderers)
            {
                spriteRenderer.color = canBePlaced ? Color.white : Color.red;
            }
        }
        
        public void UpdateRotation(Vector2 target)
        {
            var current = transform.position;
            float x = target.x - current.x;
            float y = target.y - current.y;
            var rotation = (float) (Math.Atan2(y, x) * 180 / Math.PI);
            transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
        }
    }
}