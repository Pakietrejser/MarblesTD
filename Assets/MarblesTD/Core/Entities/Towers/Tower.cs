using System;
using System.Collections.Generic;
using System.Linq;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Extensions;
using MarblesTD.Core.Entities.Marbles;
using UnityEngine;

namespace MarblesTD.Core.Entities.Towers
{
    public abstract class Tower<TView> : Tower where TView : Tower.IView
    {
        protected TView View { get; private set; }
        
        public sealed override void Init(IView view, Vector2 position)
        {
            if (!(view is TView actualView)) throw new ArgumentException();

            View = actualView;
            View.Clicked += SelectTower;
        }
        
        public sealed override void Destroy()
        {
            View.DestroySelf();
            IsDestroyed = true;
        }

        public sealed override void Select() => View?.Select();
        public sealed override void Deselect() => View?.Deselect();
    }
    
    public abstract class Tower
    {
        const int MaxUpgrades = 3;
        
        public event Action<Tower> Selected;

        public string RawName => GetType().GetName();
        public abstract int Cost { get; }
        public abstract AnimalType AnimalType { get; }
        public bool IsDestroyed { get; protected set; }
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
        
        protected Vector2 Position { get; private set; }

        public virtual void Init(IView view, Vector2 position)
        {
            Position = position;
        }
        
        public abstract void UpdateTower(IEnumerable<Marble> marbles, float delta);

        protected void SelectTower() => Selected?.Invoke(this);
        public abstract void Select();
        public abstract void Deselect();
        public abstract void Destroy();

        public interface IView
        {
            event Action Clicked;
            Collider2D Collider { get; }
            void Select();
            void Deselect();
            void DestroySelf();
            void EnableCollider();
            void DisableCollider();
            void ShowAsPlaceable(bool canBePlaced);
            void UpdateRotation(Vector2 target);
        }
    }
}
