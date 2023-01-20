using System;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Core.Entities.Marbles.Modifiers
{
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