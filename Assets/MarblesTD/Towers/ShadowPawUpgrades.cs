using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class SharpBlades : Upgrade<ShadowPaw>
    {
        public override int Cost => 60;
        public override string Description => "Zwiększa moc swoich ostrzy.";
        
        protected override void ExplicitApply(ShadowPaw tower)
        {
            tower.Damage = 2;
            tower.Hits = 5;
        }
    }
    
    public class PoisonousBlades : Upgrade<ShadowPaw>
    {
        public override int Cost => 150;
        public override string Description => "Ostrza ZATRUWAJĄ marble.";
        protected override void ExplicitApply(ShadowPaw tower)
        {
            tower.Poisonous = true;
        }
    }
    
    public class FasterAttack : Upgrade<ShadowPaw>
    {
        public override int Cost => 100;
        public override string Description => "Wymachuje dwa razy szybciej.";
        
        protected override void ExplicitApply(ShadowPaw tower)
        {
            tower.ReloadSpeed = 0.6f;
        }
    }
    
    public class ThirdArm : Upgrade<ShadowPaw>
    {
        public override int Cost => 150;
        public override string Description => "Wymachuje cztery razy szybciej!";
        protected override void ExplicitApply(ShadowPaw tower)
        {
            tower.ReloadSpeed = 0.3f;
        }
    }

    public class Yoink : Upgrade<ShadowPaw>
    {
        public override int Cost => 100;
        public override string Description => "Zgarnia więcej miodu przy zabiciu marbla.";
        protected override void ExplicitApply(ShadowPaw tower)
        {
            tower.StealHoney = true;
        }
    }
    
    public class SweetDeath : Upgrade<ShadowPaw>
    {
        public override int Cost => 0;
        public override string Description => "Zużywa miód by atakować. UWAGA - ostrożnie z tym.";
        protected override void ExplicitApply(ShadowPaw tower)
        {
            tower.UtilizeHoney = true;
        }
    }
}