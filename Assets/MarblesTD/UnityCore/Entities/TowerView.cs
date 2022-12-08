using System;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Projectiles;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
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
        
        public void UpdateRotation(Vector2 target)
        {
            var current = transform.position;
            float x = target.x - current.x;
            float y = target.y - current.z;
            var rotation = (float) (Math.Atan2(y, x) * 180 / Math.PI);

            if (rotation < 0)
            {
                rotation = Math.Abs(rotation) + 90;
            }
            else if (rotation > 90 )
            {
                rotation = 270 + Math.Abs(rotation - 180);
            }
            else
            {
                rotation = Math.Abs(rotation - 90);
            }
            
            transform.rotation = Quaternion.Euler(0, rotation + 180, 0);
        }
    }
}