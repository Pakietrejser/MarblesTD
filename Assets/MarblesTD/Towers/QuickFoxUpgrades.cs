using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class FasterAttackAndSeeHidden : Upgrade<QuickFox>
    {
        public override int Cost => 35;
        public override string Description => "Szybciej naciąga cięciwę.";
        
        protected override void ExplicitApply(QuickFox tower)
        {
            tower.ReloadSpeed = 0.65f;
        }
    }
    
    public class PiercingShot : Upgrade<QuickFox>
    {
        public override int Cost => 200;
        public override string Description => "Jego strzała przebija wszystko!";
        protected override void ExplicitApply(QuickFox tower)
        {
            tower.Pierce = 20;
        }
    }
    
    public class SharperArrows : Upgrade<QuickFox>
    {
        public override int Cost => 40;
        public override string Description => "Strzała przebija do 2 warstw marbli.";
        
        protected override void ExplicitApply(QuickFox tower)
        {
            tower.Damage = 2;
        }
    }
    
    public class ExplosiveArrows : Upgrade<QuickFox>
    {
        public override int Cost => 150;
        public override string Description => "Strzała przebija do 10 warstw marbli.";
        protected override void ExplicitApply(QuickFox tower)
        {
            tower.Damage = 10;
        }
    }

    public class DoubleShot : Upgrade<QuickFox>
    {
        public override int Cost => 100;
        public override string Description => "Wystrzeliwuje dwie strzały na raz.";
        protected override void ExplicitApply(QuickFox tower)
        {
            tower.DoubleShot = true;
        }
    }
    
    public class Hydra : Upgrade<QuickFox>
    {
        public override int Cost => 250;
        public override string Description => "Co pięć strzał to nie jedna.";
        protected override void ExplicitApply(QuickFox tower)
        {
            tower.Hydra = true;
        }
    }
}