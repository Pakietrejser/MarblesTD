using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers.QuickFoxTower.Upgrades
{
    public class SharperArrows : Upgrade<QuickFox>
    {
        public override int Cost => 30;
        public override string Description => "silne strzały";
        
        protected override void ExplicitApply(QuickFox tower)
        {
            tower.Damage += 3;
        }
    }
}