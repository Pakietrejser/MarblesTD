using System;
using MarblesTD.Core.Entities.Towers.Upgrades;
using UnityEngine;

namespace MarblesTD.Towers.QuickFoxImpl.RightPathUpgrades
{
    public class BetterSpeed : Upgrade<QuickFox, BetterSpeed.Settings>
    {
        public float Speed;
         
        public BetterSpeed(Settings settings) : base(settings) { }

        protected override void ExplicitApply(QuickFox tower)
        {
            tower.AttackSpeed *= Speed;
        }

        protected override void ExplicitUpdateSettings(Settings settings)
        {
            Speed = settings.Speed;
        }

        [Serializable]
        public class Settings : SettingsBase
        {
            [Header("Gameplay Data")]
            public float Speed;
        }
    }
}