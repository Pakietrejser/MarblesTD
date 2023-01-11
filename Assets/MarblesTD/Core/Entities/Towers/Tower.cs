using System;
using System.Collections.Generic;
using System.Linq;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Extensions;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers.Projectiles;
using UnityEngine;

namespace MarblesTD.Core.Entities.Towers
{
    public abstract class Tower
    {
        const int MaxUpgrades = 3;
        
        public event Action Selected;

        public string RawName => GetType().GetName();
        public string TranslatedName => this.GetTranslatedName();
        public abstract int Cost { get; }
        public abstract AnimalType AnimalType { get; }
        public bool IsDestroyed { get; private set; }
        public int MarblesKilled { get; set; }
        public abstract Dictionary<UpgradePath, Upgrade> Upgrades { get; }

        public int SellValue
        {
            get
            {
                int totalCost = Cost + Upgrades
                    .Where(upgrade => upgrade.Value.Applied)
                    .Sum(upgrade => upgrade.Value.Cost);
                return Convert.ToInt32(totalCost * .75f);
            }
        }

        public int RemainingUpgrades
        {
            get
            {
                return MaxUpgrades - Upgrades.Count(upgrade => upgrade.Value.Applied);
            }
        }
        
        protected Vector2 Position { get; }
        protected IView View { get; }

        protected Tower(IView view, Vector2 position)
        {
            View = view;
            Position = position;
            View.Clicked += () => Selected?.Invoke();
        }

        public abstract void UpdateTower(IEnumerable<Marble> marbles, float delta);

        public void Destroy()
        {
            View.DestroySelf();
            IsDestroyed = true;
        }

        public void Select() => View.Select();
        public void Deselect() => View.Deselect();
        
        public interface IView
        {
            event Action Clicked;

            void Init(Sprite sprite, AnimalType towerType);
            void Select();
            void Deselect();
            Projectile SpawnProjectile(ProjectileConfig config);
            void DestroySelf();
            void EnableCollider();
            void DisableCollider();
            void ShowAsPlaceable(bool canBePlaced);
            void UpdateRotation(Vector2 closestMarblePosition);
        }
    }
}
