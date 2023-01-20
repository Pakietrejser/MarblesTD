using System;
using System.Collections.Generic;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.Core.Entities.Marbles.Modifiers
{
    public class Slow : Modifier<Slow>
    {
        public override bool IsActive => _towers.Count != 0;

        readonly List<Tower> _towers = new List<Tower>();

        public Slow(Tower dealer, Marble owner) : base(dealer, owner)
        {
            _towers.Add(dealer);
        }
        
        public override void OnApplied()
        {
            Debug.Log("Applying slow...");
            Owner.Speed -= 1.5f;
        }

        public override void OnRemoved()
        {
            Debug.Log("Removing slow...");
            Owner.Speed += 1.5f;
        }

        public override void Update(float delta)
        {
            
        }

        protected override bool TryExplicitMerge(Slow other)
        {
            _towers.Add(other.Dealer);
            return true;
        }
        
        public override bool TryRemove(Tower tower)
        {
            _towers.Remove(tower);
            return true;
        }
    }
    
    public class Poison : Modifier<Poison>
    {
        public override bool IsActive => _stacks > 0;
        
        const float PoisonTick = 1.4f;
        float _currentPoisonTick;
        
        int _stacks;

        public Poison(Tower dealer, Marble owner) : base(dealer, owner)
        {
            _stacks = 1;
        }
        
        public override void OnApplied()
        {
            Debug.Log("Applying poison...");
            Owner.TogglePoisonView(true);
        }

        public override void OnRemoved()
        {
            Debug.Log("Removing poison...");
            Owner.TogglePoisonView(false);
        }

        public override void Update(float delta)
        {
            if (Owner.IsDestroyed) return;
            _currentPoisonTick -= delta;
            if (!(_currentPoisonTick <= 0)) return;

            _currentPoisonTick = PoisonTick;
            Owner.TakeDamage(_stacks, Dealer);
        }

        public override bool TryRemove(Tower tower)
        {
            _stacks = 0;
            return true;
        }

        protected override bool TryExplicitMerge(Poison other)
        {
            _stacks += other._stacks;
            return true;
        }
    }
    
    public abstract class Modifier<T> : Modifier where T : Modifier
    {
        protected Modifier(Tower dealer, Marble owner) : base(dealer, owner){}
        
        public sealed override bool TryMerge(Modifier other)
        {
            if (!(other is T actualOther)) throw new ArgumentException();
            return TryExplicitMerge(actualOther);
        }

        protected abstract bool TryExplicitMerge(T other);
    }

    public abstract class Modifier
    {
        protected Tower Dealer { get; }
        protected Marble Owner { get; }
        
        public abstract bool IsActive { get; }

        public abstract void OnApplied();
        public abstract void OnRemoved();
        public abstract void Update(float delta);
        public abstract bool TryMerge(Modifier other);
        public abstract bool TryRemove(Tower tower);

        protected Modifier(Tower dealer, Marble owner)
        {
            Dealer = dealer;
            Owner = owner;
        }
    }
}