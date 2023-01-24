using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Core.Entities.Marbles.Modifiers
{
    public class XL : Modifier<XL>
    {
        public override bool IsActive => true;
        readonly int _bonusHealth;

        public XL(int bonusHealth = 50)
        {
            _bonusHealth = bonusHealth;
        }
        
        public override void OnApplied()
        {
            Owner.Health += _bonusHealth;
            Owner.SpeedModifier -= 2;
            Owner.MinSpeed = .2f;
            Owner.ToggleXLView(true);
        }

        public override void OnRemoved()
        {
            SignalBus.FireStatic(new SpawnBonusMarblesSignal(new WaveGroup(10, 10), Owner.Position, Owner.Path, Owner.DistanceTravelled));
            Owner.ToggleXLView(false);
        }

        public override void Update(float delta) {}
        public override bool TryRemove(Tower tower) => false;
        protected override bool TryExplicitMerge(XL other) => false;
    }
}