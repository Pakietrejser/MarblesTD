using System;
using MarblesTD.Core.Projectiles;
using MarblesTD.Core.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore
{
    public class TowerView : MonoBehaviour, ITowerView
    {
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] SpriteRenderer towerRenderer;
        [SerializeField] SpriteRenderer selectRenderer;
        [SerializeField] Collider collider;
        
        public event Action Clicked;

        public void Init(Sprite sprite, TowerType towerType)
        {
            towerRenderer.sprite = sprite;
        }

        public void Select()
        {
            selectRenderer.enabled = true;
        }

        public void Unselect()
        {
            selectRenderer.enabled = false;
        }

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
        
        public void DestroySelf() => Destroy(gameObject);
        
        public void EnableCollider()
        {
            collider.enabled = true;
        }

        public void DisableCollider()
        {
            collider.enabled = false;
        }

        public void ShowAsPlaceable(bool canBePlaced)
        {
            towerRenderer.color = canBePlaced ? Color.white : Color.red;
        }
    }
}