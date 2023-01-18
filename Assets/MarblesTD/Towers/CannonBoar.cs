using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Projectiles;
using UnityEngine;

namespace MarblesTD.Towers
{
    public class CannonBoar : Tower<ICannonBoarView>
    {
        public int Damage { get; set; } = 1;
        public int Pierce { get; set; } = 1;
        public float ReloadSpeed { get; set; } = 0.25f;
        public float Range { get; set; } = 4f;
        public float ProjectileTravelDistance { get; set; } = 30;
        public float ProjectileSpeed { get; set; } = 20;

        public float MissAngle = 30f;
        public bool Crazy = false;

        public override int Cost => 120;
        public override AnimalType AnimalType => AnimalType.WildAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new StrongerBullets()},
            {UpgradePath.TopLeft, new Commando()},

            {UpgradePath.BotMid, new SteadyAim()},
            {UpgradePath.TopMid, new BulletStream()},

            {UpgradePath.BotRight, new FasterFire()},
            {UpgradePath.TopRight, new Crazy()},
        };
            
        float _reloadTime;
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            _reloadTime -= delta;
            if (!(_reloadTime <= 0) || !SeekClosestMarble(marbles, out var closestMarble)) return;
            
            _reloadTime = ReloadSpeed;

            if (Crazy)
            {
                float randomAngleRange = MissAngle > 20 
                    ? Random.Range(-180, 180)
                    : Random.Range(-90, 90);

                var randomDirection = Quaternion.Euler(Vector3.forward * randomAngleRange) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, randomDirection, this));
                View.UpdateRotation(randomDirection);
            }
            else
            {
                View.UpdateRotation(closestMarble.Position);
                float randomAngleRange = Random.Range(-MissAngle, MissAngle);
                var randomDirection = Quaternion.Euler(Vector3.forward * randomAngleRange) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, randomDirection, this));
            }
        }
        
        bool SeekClosestMarble(IEnumerable<Marble> marbles, out Marble closestMarble)
        {
            var minDistance = float.MaxValue;
            closestMarble = null;
            
            foreach (var marble in marbles)
            {
                float distance = Vector2.Distance(Position, marble.Position);
                if (distance > Range) continue;
                if (!(distance < minDistance)) continue;
                if (marble.IsDestroyed) continue;
                
                closestMarble = marble;
                minDistance = distance;
            }
        
            return closestMarble != null;
        }
        
        protected override void OnSelected()
        {
            View.ShowRangeCircle(Range);
        }

        protected override void OnDeselected()
        {
            View.HideRangeCircle();
        }

        protected override void OnTowerPlaced()
        {
            View.HideRangeCircle();
        }
    }
    
    public interface ICannonBoarView : Tower.IView
    {
        Projectile SpawnProjectile(ProjectileConfig projectileConfig);
        void ShowRangeCircle(float range);
        void HideRangeCircle();
    }
}