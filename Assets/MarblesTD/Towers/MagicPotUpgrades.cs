using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class HotPot : Upgrade<MagicPot>
    {
        public override int Cost => 100;
        public override string Description => "Bucha dwa razy częściej.";
        
        protected override void ExplicitApply(MagicPot tower)
        {
            tower.ReloadSpeed = .75f;
        }
    }
    
    public class LavaPot : Upgrade<MagicPot>
    {
        public override int Cost => 200;
        public override string Description => "Bucha praktycznie cały czas.";
        protected override void ExplicitApply(MagicPot tower)
        {
            tower.ReloadSpeed = .31f;
        }
    }
    
    public class ToxicFumes : Upgrade<MagicPot>
    {
        public override int Cost => 40;
        public override string Description => "Zbija do 2 warst marbli jednym buchem.";
        
        protected override void ExplicitApply(MagicPot tower)
        {
            tower.Damage = 2;
        }
    }
    
    public class Corrosion : Upgrade<MagicPot>
    {
        public override int Cost => 500;
        public override string Description => "Buch zadaje OKRUTNE obrażenia Łamiszczękom.";
        protected override void ExplicitApply(MagicPot tower)
        {
            tower.Corrosion = true;
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