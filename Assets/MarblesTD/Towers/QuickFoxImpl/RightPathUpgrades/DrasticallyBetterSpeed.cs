using System;
using MarblesTD.Core.Towers.Upgrades;
using UnityEngine;

namespace MarblesTD.Towers.QuickFoxImpl.RightPathUpgrades
{
    public class DrasticallyBetterSpeed : Upgrade<QuickFox, DrasticallyBetterSpeed.Settings>
    {
        public float Speed;
         
        public DrasticallyBetterSpeed(Settings settings) : base(settings) { }

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