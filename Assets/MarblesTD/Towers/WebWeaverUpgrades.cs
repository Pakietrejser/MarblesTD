using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class PoisonousWeb : Upgrade<WebWeaver>
    {
        public override int Cost => 200;
        public override string Description => "Sieć ZATRUWA marble.";
        protected override void ExplicitApply(WebWeaver tower)
        {
            tower.Poisonous = true;
        }
    }
    
    public class DeadlyPoison : Upgrade<WebWeaver>
    {
        public override int Cost => 400;
        public override string Description => "Sieć ZATRUWA marble z potrójną siłą.";
        
        protected override void ExplicitApply(WebWeaver tower)
        {
            tower.SuperPoisonous = true;
        }
    }

    public class StrongerSlow : Upgrade<WebWeaver>
    {
        public override int Cost => 100;
        public override string Description => "Sieci jeszcze mocniej spowalniają.";
        
        protected override void ExplicitApply(WebWeaver tower)
        {
            tower.EvenSlower = true;
        }
    }
    
    public class BiggerWeb : Upgrade<WebWeaver>
    {
        public override int Cost => 200;
        public override string Description => "Zwiększa obszar sieci.";
        
        protected override void ExplicitApply(WebWeaver tower)
        {
            tower.Range = 3.2f;
            tower.Refresh();
        }
    }

    public class Sticky : Upgrade<WebWeaver>
    {
        public override int Cost => 100;
        public override string Description => "Spowolnienie działa dodatkowe 3 sekundy po wyjściu z sieci.";
        protected override void ExplicitApply(WebWeaver tower)
        {
            tower.Sticky = true;
        }
    }
    
    public class WebWorld : Upgrade<WebWeaver>
    {
        public override int Cost => 10;
        public override string Description => "NATYCHMIAST spowalnia wszystkie marble.";
        protected override void ExplicitApply(WebWeaver tower)
        {
            tower.SlowAllOnNextUpdate = true;
        }
    }
}