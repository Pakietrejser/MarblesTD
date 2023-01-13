using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class HotPot : Upgrade<MagicPot>
    {
        public override int Cost => 35;
        public override string Description => "Szybciej naciąga cięciwę.";
        
        protected override void ExplicitApply(MagicPot tower)
        {
            
        }
    }
    
    public class LavaPot : Upgrade<MagicPot>
    {
        public override int Cost => 200;
        public override string Description => "Jego strzała przebija wszystko!";
        protected override void ExplicitApply(MagicPot tower)
        {
            
        }
    }
    
    public class ToxicFumes : Upgrade<MagicPot>
    {
        public override int Cost => 40;
        public override string Description => "Strzała przebija do 2 warstw marbli.";
        
        protected override void ExplicitApply(MagicPot tower)
        {
           
        }
    }
    
    public class Corrosion : Upgrade<MagicPot>
    {
        public override int Cost => 150;
        public override string Description => "Strzała przebija do 10 warstw marbli.";
        protected override void ExplicitApply(MagicPot tower)
        {
            
        }
    }

    public class BiggerRange : Upgrade<MagicPot>
    {
        public override int Cost => 100;
        public override string Description => "Zwiększa pole rażenia.";
        protected override void ExplicitApply(MagicPot tower)
        {
            tower.Range = 3f;
            tower.Refresh();
        }
    }
    
    public class GiganticRange : Upgrade<MagicPot>
    {
        public override int Cost => 350;
        public override string Description => "ZNACZNIE większe pole rażenia.";
        protected override void ExplicitApply(MagicPot tower)
        {
            tower.Range = 5f;
            tower.Refresh();
        }
    }
}