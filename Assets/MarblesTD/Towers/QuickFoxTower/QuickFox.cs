using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Projectiles;
using MarblesTD.Towers.QuickFoxTower.Upgrades;
using UnityEngine;

namespace MarblesTD.Towers.QuickFoxTower
{
    public class QuickFox : Tower<IQuickFoxView>
    {
        public int Damage { get; set; } = 1;
        public int Pierce { get; set; } = 2;
        public float AttackSpeed { get; set; } = 0.95f;
        public int Range { get; set; } = 5;
        public float ProjectileTravelDistance { get; set; } = 30;
        public float ProjectileSpeed { get; set; } = 20;

        float _floatTimeUntilNextAttack;
        
        public override int Cost => 50;
        public override AnimalType AnimalType => AnimalType.WildAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new FasterAttackAndSeeHidden()},
            {UpgradePath.TopLeft, new PiercingShot()},

            {UpgradePath.BotMid, new SharperArrows()},
            {UpgradePath.TopMid, new ExplosiveArrows()},

            {UpgradePath.BotRight, new SeekingArrows()},
            {UpgradePath.TopRight, new TripleShot()},
        };
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta)
        {
            _floatTimeUntilNextAttack -= delta;
            if (_floatTimeUntilNextAttack <= 0)
            {
                _floatTimeUntilNextAttack = AttackSpeed;
            }
            else
            {
                return;
            }
            
            if (!SeekClosestMarble(marbles, out var closestMarble)) return;
            View.UpdateRotation(closestMarble.Position);
            
            var projectileConfig = new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, closestMarble, this);
            View.SpawnProjectile(projectileConfig);
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
                
                closestMarble = marble;
                minDistance = distance;
            }
        
            return closestMarble != null;
        }
    }
    
    public interface IQuickFoxView : Tower.IView
    {
        Projectile SpawnProjectile(ProjectileConfig config);
    }
}