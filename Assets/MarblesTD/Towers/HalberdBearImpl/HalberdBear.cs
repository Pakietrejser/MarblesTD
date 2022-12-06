using System;
using System.Collections.Generic;
using MarblesTD.Core.Marbles;
using MarblesTD.Core.Towers;
using MarblesTD.Core.Towers.Upgrades;
using UnityEngine;

namespace MarblesTD.Towers.HalberdBearImpl
{
    public class HalberdBear : Tower
    {
        public HalberdBear(SettingsBase settings, ITowerView view, Vector2 position) : base(view, position, settings.GetUpgrades())
        {
            UpdateSettings(settings);
        }

        public sealed override void UpdateSettings(SettingsBase settingsBase)
        {
            base.UpdateSettings(settingsBase);
        }

        public override void Update(IEnumerable<Marble> marbles, float delta)
        {
            
        }
        
        [Serializable]
        public class Settings : SettingsBase
        {
            public override Dictionary<Path, Upgrade[]> GetUpgrades()
            {
                return new Dictionary<Path, Upgrade[]>();
            }
        }
    }
}