using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
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
    
    public class PiercingShot : Upgrade<QuickFox>
    {
        public override int Cost => 1;
        public override string Description => "shoot";
        protected override void ExplicitApply(QuickFox tower)
        {
        }
    }
    
    public class SharperArrows : Upgrade<QuickFox>
    {
        public override int Cost => 30;
        public override string Description => "silne strzały";
        
        protected override void ExplicitApply(QuickFox tower)
        {
            tower.Damage += 3;
        }
    }
    
    public class ExplosiveArrows : Upgrade<QuickFox>
    {
        public override int Cost => 1;
        public override string Description => "boom";
        protected override void ExplicitApply(QuickFox tower)
        {
            
        }
    }
    
    public class SeekingArrows : Upgrade<QuickFox>
    {
        public override int Cost => 1;
        public override string Description => "Twoje strzały szukają przeciwników.";
        protected override void ExplicitApply(QuickFox tower)
        {
        }
    }
    
    public class TripleShot : Upgrade<QuickFox>
    {
        public override int Cost => 1;
        public override string Description => "Trzy strzały zamiast jednej";
        protected override void ExplicitApply(QuickFox tower)
        {
        }
    }
}