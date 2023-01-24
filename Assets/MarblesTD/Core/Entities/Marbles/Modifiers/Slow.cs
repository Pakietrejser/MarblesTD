using System.Collections.Generic;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Core.Entities.Marbles.Modifiers
{
    public class Slow : Modifier<Slow>
    {
        public override bool IsActive => _towers.Count != 0;
        float _potency;
        readonly List<Tower> _towers = new List<Tower>();

        public Slow(float potency)
        {
            _potency = potency;
        }
        
        public override void OnApplied()
        {
            _towers.Add(Dealer);
            Owner.SpeedModifier -= _potency;
        }

        public override void OnRemoved()
        {
            Owner.SpeedModifier += _potency;
        }

        public override void Update(float delta)
        {
        }

        protected override bool TryExplicitMerge(Slow other)
        {
            if (_towers.Contains(other.Dealer)) return true;
            
            _towers.Add(other.Dealer);
            float difference = other._potency - _potency;
            if (difference > 0)
            {
                _potency = other._potency;
                Owner.SpeedModifier -= difference;
            }

            return true;
        }
        
        public override bool TryRemove(Tower tower)
        {
            if (!_towers.Contains(tower)) return false;
            _towers.Remove(tower);
            return true;
        }
    }
}