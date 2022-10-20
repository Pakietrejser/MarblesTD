using System;
using MarblesTD.Core.Projectiles;
using UnityEngine;

namespace MarblesTD.Core.Towers
{
    public interface ITowerView
    {
        event Action Clicked;

        void Init(Sprite sprite, TowerType towerType);
        void Select();
        void Unselect();
        Projectile SpawnProjectile(ProjectileConfig config);
        void DestroySelf();
    }
}