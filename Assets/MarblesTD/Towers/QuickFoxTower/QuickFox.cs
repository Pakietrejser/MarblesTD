using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Projectiles;
using MarblesTD.Towers.QuickFoxTower.Upgrades;
using UnityEngine;

namespace MarblesTD.Towers.QuickFoxTower
{
    public class QuickFox : Tower
    {
        public int Damage {get; set;}
        public int Pierce {get; set;}
        public float AttackSpeed {get; set;}
        public int Range {get; set;}
        public float ProjectileTravelDistance {get; set;}
        public float ProjectileSpeed {get; set;}

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

        public QuickFox(IView view, Vector2 position) : base(view, position)
        {
        }
        
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
}