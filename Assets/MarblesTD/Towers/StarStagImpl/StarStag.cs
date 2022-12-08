using System;
using System.Collections.Generic;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.Entities.Towers.Upgrades;
using UnityEngine;

namespace MarblesTD.Towers.StarStagImpl
{
    public class StarStag : Tower
    {
        public StarStag(SettingsBase settings, ITowerView view, Vector2 position) : base(view, position, settings.GetUpgrades())
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