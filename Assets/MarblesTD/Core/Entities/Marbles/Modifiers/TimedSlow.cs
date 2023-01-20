using System.Collections.Generic;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Core.Entities.Marbles.Modifiers
{
    public class TimedSlow : Modifier<TimedSlow>
    {
        float _potency;
        float _duration;
        public override bool IsActive => _duration > 0 && _towers.Count != 0;

        readonly List<Tower> _towers = new List<Tower>();

        public TimedSlow(Tower dealer, Marble owner, float potency, float duration) : base(dealer, owner)
        {
            _potency = potency;
            _duration = duration;
            _towers.Add(dealer);
        }
        
        public override void OnApplied()
        {
            Owner.SpeedModifier -= _potency;
        }

        public override void OnRemoved()
        {
            Owner.SpeedModifier += _potency;
        }

        public override void Update(float delta)
        {
            _duration -= delta;
        }

        protected override bool TryExplicitMerge(TimedSlow other)
        {
            if (_towers.Contains(other.Dealer))
            {
                _duration = other._duration;
                return true;
            }
            
            float difference = other._potency - _potency;
            if (difference > 0)
            {
                _potency = other._potency;
                Owner.SpeedModifier -= difference;
            }

            _duration = other._duration;
            _towers.Add(other.Dealer);
            
            return true;
        }
        
        public override bool TryRemove(Tower tower)
        {
            if (!_towers.Contains(tower)) return false;
            _duration = 0;
            _towers.Remove(tower);
            return true;
        }
    }
}