using System;
using System.Collections.Generic;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers.Upgrades;
using UnityEngine;

namespace MarblesTD.Core.Entities.Towers
{
    public abstract class Tower
    {
        public event Action Selected;
        
        public string Name;
        public string Description;
        public int Cost;
        public TowerType TowerType;
        public Sprite Icon;
        
        public bool IsDestroyed { get; private set; }
        public int SellValue => Convert.ToInt32(Cost * .75f);
        public int KIllCount;
        public IReadOnlyDictionary<Path, Upgrade[]> Upgrades => _upgrades;

        protected readonly ITowerView _view;
        protected Vector2 _position;
        readonly Dictionary<Path, Upgrade[]> _upgrades;
        
        protected Tower(ITowerView view, Vector2 position, Dictionary<Path, Upgrade[]> upgrades)
        {
            _view = view;
            _position = position;
            _upgrades = upgrades;
            KIllCount = 0;
            
            _view.Clicked += SelectTower;
        }

        void SelectTower()
        {
            Selected?.Invoke();
        }

        public virtual void UpdateSettings(SettingsBase settingsBase)
        {
            Name = settingsBase.Name;
            Description = settingsBase.Description;
            Cost = settingsBase.Cost;
            TowerType = settingsBase.TowerType;
            Icon = settingsBase.Icon;
            
            _view.Init(Icon, TowerType);
        }

        public abstract void Update(IEnumerable<Marble> marbles, float delta);
        
        public void ApplyUpgrade(Upgrade upgrade)
        {
            upgrade.Apply(this);
        }

        public void Destroy()
        {
            _view.DestroySelf();
            IsDestroyed = true;
        }

        public void Select() => _view.Select();
        public void Unselect() => _view.Unselect();

        [Serializable]
        public abstract class SettingsBase
        {
            public string Name;
            public string Description;
            public int Cost;
            public TowerType TowerType;
            public Sprite Icon;

            public abstract Dictionary<Path, Upgrade[]> GetUpgrades();
        }
    }
}
