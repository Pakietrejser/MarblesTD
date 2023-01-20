﻿using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.Core.Entities.Marbles.Modifiers
{
    public class Poison : Modifier<Poison>
    {
        public override bool IsActive => _stacks > 0;
        
        const float PoisonTick = 1.4f;
        float _currentPoisonTick;
        
        int _stacks;

        public Poison(Tower dealer, Marble owner, int stacks = 1) : base(dealer, owner)
        {
            _stacks = stacks;
        }
        
        public override void OnApplied()
        {
            _currentPoisonTick = PoisonTick;
            Owner.TogglePoisonView(true);
        }

        public override void OnRemoved()
        {
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
}