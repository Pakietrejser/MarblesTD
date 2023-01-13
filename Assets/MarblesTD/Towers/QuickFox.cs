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
        public float AttackSpeed { get; set; } = 0.95f;
        public float Range { get; set; } = 3.5f;
        public float ProjectileTravelDistance { get; set; } = 30;
        public float ProjectileSpeed { get; set; } = 20;


        public bool SeekingArrows = false;
        public bool TripleShot = false;
        float _floatTimeUntilNextAttack;
        
        public override int Cost => 50;
        public override AnimalType AnimalType => AnimalType.WildAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new FasterAttackAndSeeHidden()},
            {UpgradePath.TopLeft, new PiercingShot()},

            {UpgradePath.BotMid, new SharperArrows()},
            {UpgradePath.TopMid, new ExplosiveArrows()},

            {UpgradePath.BotRight, new TripleShot()},
            {UpgradePath.TopRight, new SeekingArrows()},
        };
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta)
        {
            _floatTimeUntilNextAttack -= delta;
            if (!(_floatTimeUntilNextAttack <= 0) || !SeekClosestMarble(marbles, out var closestMarble)) return;
            
            _floatTimeUntilNextAttack = AttackSpeed;
            View.UpdateRotation(closestMarble.Position);
            
            var projectileConfig = new ArrowConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, closestMarble.Position, this, SeekingArrows);
            View.SpawnProjectile(projectileConfig);

            
            if (TripleShot)
            {
                var leftArrow = Quaternion.Euler(Vector3.forward * 20) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                var leftConfig = new ArrowConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, leftArrow, this, SeekingArrows);
                View.SpawnProjectile(leftConfig);
                
                var rightArrow = Quaternion.Euler(Vector3.forward * -20) * (closestMarble.Position - Position) + new Vector3(Position.x, Position.y);
                var rightConfig = new ArrowConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, rightArrow, this, SeekingArrows);
                View.SpawnProjectile(rightConfig);
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
                Debug.Log(distance);
                
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
        Projectile SpawnProjectile(ArrowConfig config);
        void ShowRangeCircle(float range);
        void HideRangeCircle();
    }
}