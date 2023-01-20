using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class Bite : Upgrade<WebWeaver>
    {
        public override int Cost => 1;
        public override string Description => "Raz na jakiś czas gryzie wszystkie Marble w sieci.";
        
        protected override void ExplicitApply(WebWeaver tower)
        {
        }
    }
    
    public class SuperBite : Upgrade<WebWeaver>
    {
        public override int Cost => 1;
        public override string Description => "Ugryzienie zadaje OGROMNE obrażenia.";
        protected override void ExplicitApply(WebWeaver tower)
        {
        }
    }
    
    public class BiggerWeb : Upgrade<WebWeaver>
    {
        public override int Cost => 1;
        public override string Description => "Zwiększa obszar sieci.";
        
        protected override void ExplicitApply(WebWeaver tower)
        {
        }
    }
    
    public class PoisonousWeb : Upgrade<WebWeaver>
    {
        public override int Cost => 1;
        public override string Description => "Sieć ZATRUWA marble.";
        protected override void ExplicitApply(WebWeaver tower)
        {
        }
    }

    public class Sticky : Upgrade<WebWeaver>
    {
        public override int Cost => 1;
        public override string Description => "Spowolnienie działa 3 sekundy po wyjściu z sieci.";
        protected override void ExplicitApply(WebWeaver tower)
        {
        }
    }
    
    public class WebWorld : Upgrade<WebWeaver>
    {
        public override int Cost => 1;
        public override string Description => "NATYCHMIAST spowalnia wszystkie marble.";
        protected override void ExplicitApply(WebWeaver tower)
        {
        }
    }
}