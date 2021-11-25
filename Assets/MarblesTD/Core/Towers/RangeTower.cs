using System;
using System.Collections.Generic;
using MarblesTD.Core.Marbles;
using MarblesTD.Core.Projectiles;
using UnityEngine;

namespace MarblesTD.Core.Towers
{
    public abstract class RangeTower<TSettings> : Tower where TSettings : RangeTower<TSettings>.SettingsRangeBase
    {
        public int Damage;
        public int Pierce;
        public float AttackSpeed;
        public int Range;
        public float ProjectileTravelDistance;
        public float ProjectileSpeed;

        float timeUntilNextAttack;

        protected RangeTower(TSettings settings, ITowerView view, Vector2 position) : base(view, position)
        {
            UpdateSettings(settings);
        }

        protected abstract void ExplicitUpdateSettings(TSettings settings);
        public sealed override void UpdateSettings(SettingsBase settingsBase)
        {
            if (!(settingsBase is TSettings newSettings)) throw new ArgumentException();

            Damage = newSettings.Damage;
            Pierce = newSettings.Pierce;
            AttackSpeed = newSettings.AttackSpeed;
            Range = newSettings.Range;
            ProjectileTravelDistance = newSettings.ProjectileTravelDistance;
            ProjectileSpeed = newSettings.ProjectileSpeed;
            
            ExplicitUpdateSettings(newSettings);
            base.UpdateSettings(newSettings);
        }

        bool SeekClosestMarble(IEnumerable<MarblePlacement> marblePlacements, out Marble closestMarble)
        {
            float minDistance = float.MaxValue;
            closestMarble = null;
            
            foreach (var marblePlacement in marblePlacements)
            {
                float distance = Vector2.Distance(Position, marblePlacement.Position);
                if (distance > Range) continue;
                if (!(distance < minDistance)) continue;
                
                closestMarble = marblePlacement.Marble;
                minDistance = distance;
            }
        
            return closestMarble != null;
        }

        public override void Update(IEnumerable<MarblePlacement> marblePlacements, float delta)
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
            
            var projectileConfig = new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, closestMarble);
            View.SpawnProjectile(projectileConfig);
        }
        
        [Serializable]
        public class SettingsRangeBase : SettingsBase
        {
            public int Damage;
            public int Pierce;
            public float AttackSpeed;
            public int Range;
            public float ProjectileTravelDistance;
            public float ProjectileSpeed;
        }
    }
}