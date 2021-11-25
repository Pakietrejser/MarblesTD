using System.Collections.Generic;
using MarblesTD.Towers.Upgrades;
using UnityEngine;

namespace MarblesTD.Towers
{

    public abstract class Tower
    {
        public abstract int Damage { get; set; }
        public abstract int Pierce { get; }
        public abstract float AttackSpeed { get; }
        public abstract int Range { get; }
        public abstract float ProjectileDistance { get; }
        public abstract float ProjectileSpeed { get; }

        readonly ITowerView view;
        readonly Vector2 position;

        float timeUntilNextAttack;


        protected Tower(ITowerView towerView, Vector3 spawnPosition)
        {
            view = towerView;
            position = spawnPosition;

            Debug.Log($"Creating {GetType()} at position {this.position}");
        }
        
        // Dictionary<Path, IUpgrade[]> Upgrades;
        
        //public void AddUpgrade(Path, Upgrade<>){}

        // void ApplyUpgrade(IUpgrade upgrade)
        // {
        //     upgrade.Apply(this);
        // }
        //
        // void ApplyUpgrade(Upgrade<Ttest> upgrade)
        // {
        //     upgrade.Apply(this);
        // }
        

        public void Update(IEnumerable<MarblePlacement> marblePlacements, float delta)
        {
            timeUntilNextAttack -= delta;
            if (timeUntilNextAttack <= 0)
            {
                timeUntilNextAttack = AttackSpeed;
            }
            else
            {
                return;
            }
            
            if (!SeekClosestMarble(marblePlacements, out var closestMarble)) return;

            var projectileConfig = new ProjectileConfig(Damage, Pierce, ProjectileDistance, ProjectileSpeed, closestMarble);
            view.SpawnProjectile(projectileConfig);
        }

        bool SeekClosestMarble(IEnumerable<MarblePlacement> marblePlacements, out Marble closestMarble)
        {
            float minDistance = float.MaxValue;
            closestMarble = null;
            
            foreach (var marblePlacement in marblePlacements)
            {
                float distance = Vector2.Distance(position, marblePlacement.Position);
                if (distance > Range) continue;
                if (!(distance < minDistance)) continue;
                
                closestMarble = marblePlacement.Marble;
                minDistance = distance;
            }

            return closestMarble != null;
        }
    }

    public interface ITowerView
    {
        Projectile SpawnProjectile(ProjectileConfig config);
    }

    public readonly struct MarblePlacement
    {
        public readonly Marble Marble;
        public readonly Vector2 Position;
        
        public MarblePlacement(Marble marble, Vector2 position)
        {
            Marble = marble;
            Position = position;
        }
    }
}
