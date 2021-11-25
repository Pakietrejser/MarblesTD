using System;
using MarblesTD.Core.Upgrades;

namespace MarblesTD.Towers.QuickFoxImpl.LeftPathUpgrades
{
    public class RuleOfThree : Upgrade<QuickFox, RuleOfThree.Settings>
    {
        public int Damage;
        public int Range;
         
        public RuleOfThree(Settings settings) : base(settings) { }

        protected override void ExplicitApply(QuickFox tower)
        {
            tower.Damage += Damage * Range;
        }

        protected override void ExplicitUpdateSettings(Settings settings)
        {
            Damage = settings.Damage;
            Range = settings.Range;
        }

        [Serializable]
        public class Settings : SettingsBase
        {
            public int Damage;
            public int Range;
        }
    }
}