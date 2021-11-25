using System;
using System.Collections.Generic;
using MarblesTD.Core.Marbles;
using MarblesTD.Core.Upgrades;
using UnityEngine;

namespace MarblesTD.Core.Towers
{
    public abstract class Tower
    {
        public string Name;
        public string Description;
        public int Cost;
        public TowerType TowerType;
        public Sprite Icon;
        
        protected ITowerView View;
        protected Vector2 Position;
        readonly Dictionary<Path, Upgrade[]> activeUpgrades;
        
        protected Tower(ITowerView view, Vector2 position)
        {
            View = view;
            Position = position;
            activeUpgrades = new Dictionary<Path, Upgrade[]>();
        }
        
        public virtual void UpdateSettings(SettingsBase settingsBase)
        {
            Name = settingsBase.Name;
            Description = settingsBase.Description;
            Cost = settingsBase.Cost;
            TowerType = settingsBase.TowerType;
            Icon = settingsBase.Icon;
        }

        public abstract void Update(IEnumerable<MarblePlacement> marblePlacements, float delta);
        
        public void ApplyUpgrade(Upgrade upgrade)
        {
            upgrade.Apply(this);
            activeUpgrades.Add(Path.None, new []{upgrade});
        }
        bool IsUpgraded(Path path, int tier)
        {
            if (tier <= 0) throw new ArgumentException();
            if (!activeUpgrades.TryGetValue(path, out var upgrades)) return false;
            return upgrades.Length >= tier;
        }
        public int GetHighestActiveTier()
        {
            if (activeUpgrades.Count == 0) return 0;

            int highestTier = 0;
            foreach (var upgrade in activeUpgrades.Values)
            {
                if (upgrade.Length > highestTier)
                    highestTier = upgrade.Length;
            }

            return highestTier;
        }


        [Serializable]
        public class SettingsBase
        {
            public string Name;
            public string Description;
            public int Cost;
            public TowerType TowerType;
            public Sprite Icon;
        }
    }
}
