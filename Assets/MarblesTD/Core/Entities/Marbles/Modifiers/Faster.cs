using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Core.Entities.Marbles.Modifiers
{
    public class Faster : Modifier<Faster>
    {
        public override bool IsActive => true;
        readonly float _potency;

        public Faster(float potency = 1)
        {
            _potency = potency;
        }
        
        public override void OnApplied()
        {
            Owner.SpeedModifier += _potency;
        }

        public override void OnRemoved()
        {
        }

        public override void Update(float delta) {}
        public override bool TryRemove(Tower tower) => false;
        protected override bool TryExplicitMerge(Faster other) => false;
    }
}