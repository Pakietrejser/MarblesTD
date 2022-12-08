using System;
using System.Collections.Generic;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers.Projectiles;
using UnityEngine;

namespace MarblesTD.Core.Entities.Towers
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

        protected RangeTower(TSettings settings, ITowerView view, Vector2 position) : base(view, position, settings.GetUpgrades())
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

        bool SeekClosestMarble(IEnumerable<Marble> marbles, out Marble closestMarble)
        {
            var minDistance = float.MaxValue;
            closestMarble = null;
            
            foreach (var marble in marbles)
            {
                float distance = Vector2.Distance(_position, marble.Position);
                if (distance > Range) continue;
                if (!(distance < minDistance)) continue;
                
                closestMarble = marble;
                minDistance = distance;
            }
        
            return closestMarble != null;
        }

        public override void Update(IEnumerable<Marble> marbles, float delta)
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
            
            if (!SeekClosestMarble(marbles, out var closestMarble)) return;
            
            var projectileConfig = new ProjectileConfig(Damage, Pierce, ProjectileTravelDistance, ProjectileSpeed, closestMarble, this);
            _view.SpawnProjectile(projectileConfig);
            _view.UpdateRotation(closestMarble.Position);
        }
        
        [Serializable]
        public abstract class SettingsRangeBase : SettingsBase
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