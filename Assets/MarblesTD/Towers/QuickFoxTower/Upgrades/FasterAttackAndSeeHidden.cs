using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers.QuickFoxTower.Upgrades
{
    public class FasterAttackAndSeeHidden : Upgrade<QuickFox>
    {
        public override int Cost => 30;
        public override string Description => "Strzelaj";
        
        protected override void ExplicitApply(QuickFox tower)
        {
            tower.AttackSpeed *= 0.8f;
        }
    }
}