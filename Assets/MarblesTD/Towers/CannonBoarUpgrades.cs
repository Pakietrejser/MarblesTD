using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class StrongerBullets : Upgrade<CannonBoar>
    {
        public override int Cost => 70;
        public override string Description => "Kulę przebijają do 2 warstw marbli.";
        
        protected override void ExplicitApply(CannonBoar tower)
        {
            tower.Damage += 1;
        }
    }
    
    public class Commando : Upgrade<CannonBoar>
    {
        public override int Cost => 200;
        public override string Description => "Po prostu lepszy pod każdym względem.";
        protected override void ExplicitApply(CannonBoar tower)
        {
            tower.Damage += 1;
            tower.Range = 5f;
            tower.ReloadSpeed -= 0.05f;
            tower.MissAngle = 25f;
            tower.Pierce = 2;
        }
    }
    
    public class SteadyAim : Upgrade<CannonBoar>
    {
        public override int Cost => 40;
        public override string Description => "Zmniejsza kąt wystrzału kul.";
        
        protected override void ExplicitApply(CannonBoar tower)
        {
            tower.MissAngle = 15f;
        }
    }
    
    public class BulletStream : Upgrade<CannonBoar>
    {
        public override int Cost => 250;
        public override string Description => "Tworzy strumień kul w linii prostej.";
        protected override void ExplicitApply(CannonBoar tower)
        {
            tower.MissAngle = 0f;
            tower.Pierce = 4;
            tower.ReloadSpeed -= 0.05f;
        }
    }

    public class FasterFire : Upgrade<CannonBoar>
    {
        public override int Cost => 100;
        public override string Description => "Szybciej wystrzeliwuje swoj kule.";
        protected override void ExplicitApply(CannonBoar tower)
        {
            tower.ReloadSpeed -= 0.1f;
        }
    }
    
    public class Crazy : Upgrade<CannonBoar>
    {
        public override int Cost => 300;
        public override string Description => "DZIKA FURIA!!!";
        protected override void ExplicitApply(CannonBoar tower)
        {
            tower.Crazy = true;
            tower.ReloadSpeed -= 0.07f;
            tower.Damage += 6;
        }
    }
}