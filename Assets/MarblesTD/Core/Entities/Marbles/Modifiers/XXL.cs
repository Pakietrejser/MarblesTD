using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Core.Entities.Marbles.Modifiers
{
    public class XXL : Modifier<XXL>
    {
        public override bool IsActive => true;
        readonly int _bonusHealth;

        public XXL(int bonusHealth = 200)
        {
            _bonusHealth = bonusHealth;
        }
        
        public override void OnApplied()
        {
            Owner.Health += _bonusHealth;
            Owner.SpeedModifier -= 2;
            Owner.MinSpeed = .2f;
            Owner.ToggleXXLView(true);
        }

        public override void OnRemoved()
        {
            SignalBus.FireStatic(new SpawnBonusMarblesSignal(new WaveGroup(10, 4, new XL()), Owner.Position, Owner.Path, Owner.DistanceTravelled));
        }

        public override void Update(float delta) {}
        public override bool TryRemove(Tower tower) => false;
        protected override bool TryExplicitMerge(XXL other) => false;
    }
}