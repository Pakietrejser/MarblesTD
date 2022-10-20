using System;
using MarblesTD.Core.Upgrades;
using UnityEngine;

namespace MarblesTD.Towers.QuickFoxImpl.LeftPathUpgrades
{
    public class DrasticallyMoreRange : Upgrade<QuickFox, DrasticallyMoreRange.Settings>
    {
        public int Range;
         
        public DrasticallyMoreRange(Settings settings) : base(settings) { }

        protected override void ExplicitApply(QuickFox tower)
        {
            tower.Range += Range;
        }

        protected override void ExplicitUpdateSettings(Settings settings)
        {
            Range = settings.Range;
        }

        [Serializable]
        public class Settings : SettingsBase
        {
            [Header("Gameplay Data")]
            public int Range;
        }
    }
}