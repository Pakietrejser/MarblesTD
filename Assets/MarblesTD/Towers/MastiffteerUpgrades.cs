using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class SilverBullet : Upgrade<Mastiffteer>
    {
        public override int Cost => 60;
        public override string Description => "Zbija do 5 warstw marbli jednym strzałem.";
        protected override void ExplicitApply(Mastiffteer tower)
        {
            tower.Damage = 5;
        }
    }
    
    public class BouncingBullet : Upgrade<Mastiffteer>
    {
        public override int Cost => 200;
        public override string Description => "Pocisk odbija się do dwóch następnych marbli.";
        protected override void ExplicitApply(Mastiffteer tower)
        {
            tower.BouncingBullet = true;
        }
    }

    public class QuickReload : Upgrade<Mastiffteer>
    {
        public override int Cost => 60;
        public override string Description => "Przeładuje broń 2x szybciej.";
        protected override void ExplicitApply(Mastiffteer tower)
        {
            tower.ReloadSpeed = 1f;
        }
    }
    
    public class Heroism : Upgrade<Mastiffteer>
    {
        public override int Cost => 300;
        public override string Description => "Im więcej marbli tym szybciej strzela.";
        protected override void ExplicitApply(Mastiffteer tower)
        {
            tower.Heroism = true;
        }
    }
    
    public class OneForAll : Upgrade<Mastiffteer>
    {
        public override int Cost => 40;
        public override string Description => "Zwiększa obrażenia WSZYSTKICH muszkieterów.";
        protected override void ExplicitApply(Mastiffteer tower)
        {
            tower.OneForAll = true;
            Mastiffteer.OneForAllBuffs++;
        }
    }
    
    public class AllForOne : Upgrade<Mastiffteer>
    {
        public override int Cost => 400;
        public override string Description => "Zwiększa obrażenia za KAŻDEGO muszkietera.";
        protected override void ExplicitApply(Mastiffteer tower)
        {
            tower.AllForOne = true;
        }
    }
}