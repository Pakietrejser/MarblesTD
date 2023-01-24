using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Core.Entities.Marbles.Modifiers
{
    public class Armored : Modifier<Armored>
    {
        public override bool IsActive => true;
        readonly int _armorGain;

        public Armored(int armorGain = 1)
        {
            _armorGain = armorGain;
        }
        
        public override void OnApplied()
        {
            Owner.Armor += _armorGain;
            Owner.ToggleArmorView(true);
        }

        public override void OnRemoved()
        {
            Owner.Armor -= _armorGain;
            Owner.ToggleArmorView(false);
        }

        public override void Update(float delta) {}
        public override bool TryRemove(Tower tower) => false;
        protected override bool TryExplicitMerge(Armored other) => false;
    }
}