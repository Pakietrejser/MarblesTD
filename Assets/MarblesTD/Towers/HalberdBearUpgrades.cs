using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class BearStrength : Upgrade<HalberdBear>
    {
        public override int Cost => 300;
        public override string Description => "Halabarda niszczy do 5 warstw marbli.";
        
        protected override void ExplicitApply(HalberdBear tower)
        {
            tower.Damage = 5;
        }
    }
    
    public class MarbleCrusher : Upgrade<HalberdBear>
    {
        public override int Cost => 700;
        public override string Description => "Halabarda zadaję OGROMNE obrażenia.";
        protected override void ExplicitApply(HalberdBear tower)
        {
            tower.Damage = 10;
        }
    }
    
    public class FasterSwing : Upgrade<HalberdBear>
    {
        public override int Cost => 400;
        public override string Description => "Wymachuję szybciej.";
        
        protected override void ExplicitApply(HalberdBear tower)
        {
            tower.FasterSwing = true;
        }
    }
    
    public class AutoSwing : Upgrade<HalberdBear>
    {
        public override int Cost => 400;
        public override string Description => "Wymachuję bez przerwy.";
        protected override void ExplicitApply(HalberdBear tower)
        {
            tower.AutoSwing = true;
        }
    }

    public class Sweep : Upgrade<HalberdBear>
    {
        public override int Cost => 100;
        public override string Description => "Zwiększa kąt ataku do 90 stopni.";
        protected override void ExplicitApply(HalberdBear tower)
        {
            tower.Angle = 90;
        }
    }
    
    public class Whirlwind : Upgrade<HalberdBear>
    {
        public override int Cost => 500;
        public override string Description => "Kręci się w kółko podczas ataku.";
        protected override void ExplicitApply(HalberdBear tower)
        {
            tower.Angle = 360;
        }
    }
}