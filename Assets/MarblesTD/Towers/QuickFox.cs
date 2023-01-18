using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Projectiles;
using UnityEngine;

namespace MarblesTD.Towers
{
    public class QuickFox : Tower<IQuickFoxView>
    {
        public int Damage { get; set; } = 1;
        public int Pierce { get; set; } = 2;
        public float ReloadSpeed { get; set; } = 0.95f;
        public float Range { get; set; } = 3.5f;
        public float ProjectileTravelDistance { get; set; } = 30;
        public float ProjectileSpeed { get; set; } = 20;
        
        public bool Hydra = false;
        public bool DoubleShot = false;
        
        public override int Cost => 50;
        public override AnimalType AnimalType => AnimalType.WildAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new FasterAttackAndSeeHidden()},
            {UpgradePath.TopLeft, new PiercingShot()},

            {UpgradePath.BotMid, new SharperArrows()},
            {UpgradePath.TopMid, new ExplosiveArrows()},

            {UpgradePath.BotRight, new DoubleShot()},
            {UpgradePath.TopRight, new Hydra()},
        };
        
        float _reloadTime;
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta)
        {
            _reloadTime -= delta;
            if (!(_reloadTime <= 0) || !SeekClosestMarble(marbles, out var closestMarble)) return;
            
            _reloadTime = ReloadSpeed;
            View.UpdateRotation(closestMarble.Position);

            if (Hydra)
            {
                View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, closestMarble.Position, this));
                
                var leftArrow = Quaternion.Euler(Vector3.forward * 15) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, leftArrow, this));
                
                var rightArrow = Quaternion.Euler(Vector3.forward * -15) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, rightArrow, this));
                
                var leftArrow2 = Quaternion.Euler(Vector3.forward * 30) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, leftArrow2, this));
                
                var rightArrow2 = Quaternion.Euler(Vector3.forward * -30) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, rightArrow2, this));
                
                return;
            }
            
            if (DoubleShot)
            {
                var leftArrow = Quaternion.Euler(Vector3.forward * 10) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, leftArrow, this));
                
                var rightArrow = Quaternion.Euler(Vector3.forward * -10) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, rightArrow, this));
                
                return;
            }
            
            View.SpawnProjectile(new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, closestMarble.Position, this));
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
    
    public interface IQuickFoxView : Tower.IView
    {
        Projectile SpawnProjectile(ProjectileConfig config);
        void ShowRangeCircle(float range);
        void HideRangeCircle();
    }
}