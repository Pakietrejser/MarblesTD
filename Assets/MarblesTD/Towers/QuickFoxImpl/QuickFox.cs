using System;
using System.Collections.Generic;
using MarblesTD.Core.Towers;
using MarblesTD.Core.Upgrades;
using MarblesTD.Towers.QuickFoxImpl.LeftPathUpgrades;
using MarblesTD.Towers.QuickFoxImpl.MiddlePathUpgrades;
using MarblesTD.Towers.QuickFoxImpl.RightPathUpgrades;
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
            [Header("Left Path Upgrades")]
            public LongerRange.Settings LongerRange;
            public EvenLongerRange.Settings EvenLongerRange;
            public DrasticallyMoreRange.Settings DrasticallyMoreRange;
            
            [Header("Middle Path Upgrades")]
            public MoreDamage.Settings MoreDamage;
            public EvenMoreDamage.Settings EvenMoreDamage;
            public DrasticallyMoreDamage.Settings DrasticallyMoreDamage;

            [Header("Right Path Upgrades")] 
            public BetterSpeed.Settings BetterSpeed;
            public EvenBetterSpeed.Settings EvenBetterSpeed;
            public DrasticallyBetterSpeed.Settings DrasticallyBetterSpeed;
            
            public override Dictionary<Path, Upgrade[]> GetUpgrades()
            {
                return new Dictionary<Path, Upgrade[]>
                {
                    {Path.Left, new Upgrade[]
                    {
                        new LongerRange(LongerRange),
                        new EvenLongerRange(EvenLongerRange),
                        new DrasticallyMoreRange(DrasticallyMoreRange),
                    }},
                    {Path.Middle, new Upgrade[]
                    {
                        new MoreDamage(MoreDamage),
                        new EvenMoreDamage(EvenMoreDamage),
                        new DrasticallyMoreDamage(DrasticallyMoreDamage),
                    }},
                    {Path.Right, new Upgrade[]
                    {
                        new BetterSpeed(BetterSpeed),
                        new EvenBetterSpeed(EvenBetterSpeed),
                        new DrasticallyBetterSpeed(DrasticallyBetterSpeed),
                    }},
                };
            }
        }
    }
}