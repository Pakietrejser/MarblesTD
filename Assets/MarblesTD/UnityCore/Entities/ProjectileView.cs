using System;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers.Projectiles;
using MarblesTD.UnityCore.Systems.ScenarioSystems;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class ProjectileView : MonoBehaviour, IProjectileView
    {
        public Projectile Projectile { get; set; }
        public event Action<Marble> HitMarble;
        
        public void UpdatePosition(Vector2 targetPosition)
        {
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }
        
        public void UpdateRotation(Vector2 target)
        {
            var current = transform.position;
            float x = target.x - current.x;
            float y = target.y - current.y;
            var rotation = (float) (Math.Atan2(y, x) * 180 / Math.PI);
            transform.rotation = Quaternion.Euler(0, 0, rotation + 180);
        }

        public void DestroySelf()
        {
            HitMarble = null;
            Bootstrap.Instance.Projectiles.Remove(Projectile);
            Destroy(gameObject);
        }
        
        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.TryGetComponent(out IMarbleView view)) return;
            
            HitMarble?.Invoke(view.Marble);
        }
    }
}