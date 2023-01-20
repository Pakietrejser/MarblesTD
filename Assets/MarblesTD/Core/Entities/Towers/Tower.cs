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

            base.Init(view, position);
            View = actualView;
            View.Clicked += SelectTower;
            OnTowerPlaced();
        }

        public sealed override void Destroy()
        {
            OnTowerRemoved();
            View.DestroySelf();
            IsDestroyed = true;
        }

        protected virtual void OnTowerPlaced(){}
        protected virtual void OnTowerRemoved(){}
        
        protected virtual void OnSelected(){}
        protected virtual void OnDeselected(){}

        public sealed override void Select()
        {
            if (View == null) return;
            
            View.Select();
            OnSelected();
        }

        public sealed override void Deselect()
        {
            if (View == null) return;
            
            View.Deselect();
            OnDeselected();
        }
    }
    
    public abstract class Tower
    {
        const int MaxUpgrades = 3;
        
        public event Action<Tower> Selected;

        public string RawName => GetType().GetName();
        public abstract int Cost { get; }
        public abstract AnimalType AnimalType { get; }
        public bool IsDestroyed { get; protected set; }
        public virtual int MarblesKilled { get; set; }
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
        
        public Vector2 Position { get; private set; }

        public virtual void Init(IView view, Vector2 position)
        {
            Position = position;
        }
        
        public abstract void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale);

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
