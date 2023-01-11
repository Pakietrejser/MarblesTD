using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers.QuickFoxTower.Upgrades
{
    public class SeekingArrows : Upgrade<QuickFox>
    {
        public override int Cost => 1;
        public override string Description => "Twoje strzały szukają przeciwników.";
        protected override void ExplicitApply(QuickFox tower)
        {
        }
    }
}