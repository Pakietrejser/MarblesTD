using MarblesTD.Core.Projectiles;

namespace MarblesTD.Core.Towers
{
    public interface ITowerView
    {
        Projectile SpawnProjectile(ProjectileConfig config);
    }
}