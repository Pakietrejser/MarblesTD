using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers.QuickFoxTower.Upgrades
{
    public class ExplosiveArrows : Upgrade<QuickFox>
    {
        public override int Cost => 1;
        public override string Description => "boom";
        protected override void ExplicitApply(QuickFox tower)
        {
            
        }
    }
}