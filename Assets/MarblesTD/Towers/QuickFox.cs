using System;
using UnityEngine;

namespace MarblesTD.Towers
{
    public class QuickFox : AbstractTower
    {
        public override int Damage => settings.Damage;
        public override int Pierce => settings.Pierce;
        public override float AttackSpeed => settings.AttackSpeed;
        public override int Range => settings.Range;
        public override float ProjectileDistance => settings.ProjectileDistance;
        public override float ProjectileSpeed => settings.ProjectileSpeed;

        readonly Settings settings;
        
        public QuickFox(Settings settings, ITowerView towerView, Vector3 spawnPosition) : base(towerView, spawnPosition)
        {
            this.settings = settings;
        }

        [Serializable]
        public class Settings
        {
            public int Damage = 1;
            public int Pierce = 2;
            public float AttackSpeed = .95f;
            public int Range = 6;
            [Space]
            public int ProjectileDistance = 10;
            public int ProjectileSpeed = 20;
        }
    }
}