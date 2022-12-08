using System;
using MarblesTD.Core.Entities.Towers.Projectiles;
using UnityEngine;

namespace MarblesTD.Core.Entities.Towers
{
    public interface ITowerView
    {
        event Action Clicked;

        void Init(Sprite sprite, TowerType towerType);
        void Select();
        void Unselect();
        Projectile SpawnProjectile(ProjectileConfig config);
        void DestroySelf();
        void EnableCollider();
        void DisableCollider();
        void ShowAsPlaceable(bool canBePlaced);
        void UpdateRotation(Vector2 closestMarblePosition);
    }
}