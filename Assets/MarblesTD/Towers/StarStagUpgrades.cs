using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class MorePower : Upgrade<StarStag>
    {
        public override int Cost => 110;
        public override string Description => "Zwiększa szybkość najbliższego sojusznika.";
        
        protected override void ExplicitApply(StarStag tower)
        {
        }
    }
    
    public class EvenMorePower : Upgrade<StarStag>
    {
        public override int Cost => 250;
        public override string Description => "ZNACZNIE zwiększa szybkość najbliższego sojusznika.";
        protected override void ExplicitApply(StarStag tower)
        {
        }
    }
    
    public class Twins : Upgrade<StarStag>
    {
        public override int Cost => 200;
        public override string Description => "Wspiera do dwóch najbliższych sojuszników.";
        
        protected override void ExplicitApply(StarStag tower)
        {
        }
    }
    
    public class Eden : Upgrade<StarStag>
    {
        public override int Cost => 800;
        public override string Description => "Wspiera WSZYSTKICH sojuszników.";
        protected override void ExplicitApply(StarStag tower)
        {
        }
    }

    public class Boom : Upgrade<StarStag>
    {
        public override int Cost => 10;
        public override string Description => "NATYCHMIAST zbija 1 warstę każdej marbli.";
        protected override void ExplicitApply(StarStag tower)
        {
            tower.BoomOnNextUpdate = true;
        }
    }
    
    public class Gloom : Upgrade<StarStag>
    {
        public override int Cost => 1000;
        public override string Description => "NATYCHMIAST niszczy wszystkie marble.";
        protected override void ExplicitApply(StarStag tower)
        {
            tower.GloomOnNextUpdate = true;
        }
    }
}