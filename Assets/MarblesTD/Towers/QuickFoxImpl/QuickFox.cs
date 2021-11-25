using System;
using MarblesTD.Core.Towers;
using MarblesTD.Towers.QuickFoxImpl.LeftPathUpgrades;
using UnityEngine;

namespace MarblesTD.Towers.QuickFoxImpl
{
    public class QuickFox : RangeTower<QuickFox.Settings>
    {
        public QuickFox(Settings settings, ITowerView view, Vector2 position) : base(settings, view, position) { }
        protected override void ExplicitUpdateSettings(Settings settings) {}

        [Serializable]
        public class Settings : SettingsRangeBase
        {
            public RuleOfThree.Settings RuleOfThree;
        }
    }
}