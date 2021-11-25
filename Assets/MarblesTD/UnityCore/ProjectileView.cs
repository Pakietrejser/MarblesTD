using System;
using MarblesTD.Core.Marbles;
using MarblesTD.Core.Projectiles;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore
{
    public class ProjectileView : MonoBehaviour, IProjectileView
    {
        public Projectile Projectile { get; set; }
        public event Action<Marble> HitMarble;
        
        public void UpdatePosition(Vector2 newPosition)
        {
            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.y);
        }

        public void FacePosition(Vector2 facePosition)
        {
            transform.LookAt(facePosition);
        }

        public void DestroySelf()
        {
            HitMarble = null;
            Bootstrap.Instance.Projectiles.Remove(Projectile);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision col)
        {
            if (!col.gameObject.TryGetComponent(out IMarbleView view)) return;
            
            HitMarble?.Invoke(view.Marble);
        }
    }
}