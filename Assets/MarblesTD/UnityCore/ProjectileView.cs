using System;
using MarblesTD.Core.Marbles;
using MarblesTD.Core.Towers.Projectiles;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore
{
    public class ProjectileView : MonoBehaviour, IProjectileView
    {
        public Projectile Projectile { get; set; }
        public event Action<Marble> HitMarble;
        
        public void UpdatePosition(Vector2 targetPosition)
        {
            transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.y);
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
            
            transform.rotation = Quaternion.Euler(0, rotation + 90, 0);
        }

        public void DestroySelf()
        {
            HitMarble = null;
            Bootstrap.Instance.Projectiles.Remove(Projectile);
            Destroy(gameObject);
        }

        void OnCollisionEnter(Collision col)
        {
            if (!col.gameObject.TryGetComponent(out IMarbleView view)) return;
            
            HitMarble?.Invoke(view.Marble);
        }
    }
}