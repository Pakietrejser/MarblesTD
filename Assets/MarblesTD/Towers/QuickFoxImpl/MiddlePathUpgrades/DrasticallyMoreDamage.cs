using System;
using MarblesTD.Core.Towers.Upgrades;
using UnityEngine;

namespace MarblesTD.Towers.QuickFoxImpl.MiddlePathUpgrades
{
    public class DrasticallyMoreDamage : Upgrade<QuickFox, DrasticallyMoreDamage.Settings>
    {
        public int Damage;
         
        public DrasticallyMoreDamage(Settings settings) : base(settings) { }

        protected override void ExplicitApply(QuickFox tower)
        {
            tower.Damage += Damage;
        }

        protected override void ExplicitUpdateSettings(Settings settings)
        {
            Damage = settings.Damage;
        }

        [Serializable]
        public class Settings : SettingsBase
        {
            [Header("Gameplay Data")]
            public int Damage;
        }
    }
}