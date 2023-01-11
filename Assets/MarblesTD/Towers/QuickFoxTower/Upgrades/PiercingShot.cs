using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers.QuickFoxTower.Upgrades
{
    public class PiercingShot : Upgrade<QuickFox>
    {
        public override int Cost => 1;
        public override string Description => "shoot";
        protected override void ExplicitApply(QuickFox tower)
        {
        }
    }
}