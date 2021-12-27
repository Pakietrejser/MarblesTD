using System;
using MarblesTD.Core.Projectiles;

namespace MarblesTD.Core.Towers
{
    public interface ITowerView
    {
        event Action Clicked;
        
        Projectile SpawnProjectile(ProjectileConfig config);
        void DestroySelf();
    }
}