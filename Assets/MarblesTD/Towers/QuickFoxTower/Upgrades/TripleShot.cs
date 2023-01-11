using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers.QuickFoxTower.Upgrades
{
    public class TripleShot : Upgrade<QuickFox>
    {
        public override int Cost => 1;
        public override string Description => "Trzy strzały zamiast jednej";
        protected override void ExplicitApply(QuickFox tower)
        {
        }
    }
}