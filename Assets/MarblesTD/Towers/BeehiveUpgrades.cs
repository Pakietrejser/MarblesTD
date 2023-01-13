using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class BiggerHive : Upgrade<Beehive>
    {
        public override int Cost => 150;
        public override string Description => "Zwiększona produkcja miodu.";
        protected override void ExplicitApply(Beehive tower)
        {
            tower.HoneyCapPerRound = 80;
            tower.HoneyGeneratedEveryReload = 10;
        }
    }
    
    public class GiganticHive : Upgrade<Beehive>
    {
        public override int Cost => 350;
        public override string Description => "ZNACZNIE większa produkcja miodu.";
        protected override void ExplicitApply(Beehive tower)
        {
            tower.HoneyCapPerRound = 200;
            tower.HoneyGeneratedEveryReload = 25;
        }
    }

    public class FasterBees : Upgrade<Beehive>
    {
        public override int Cost => 40;
        public override string Description => "Produkuje miód dwa razy szybciej.";
        protected override void ExplicitApply(Beehive tower)
        {
            tower.ReloadSpeed = 0.75f;
        }
    }
    
    public class DoubleDown : Upgrade<Beehive>
    {
        public override int Cost => 150;
        public override string Description => "Pod koniec każdej rundy otrzymujesz dwa razy więcej miodu.";
        protected override void ExplicitApply(Beehive tower)
        {
            tower.DoubleDown = true;
        }
    }
    
    public class LoveIsLove : Upgrade<Beehive>
    {
        public override int Cost => 50;
        public override string Description => "NATYCHMIAST otrzymujesz 10 życia.";
        protected override void ExplicitApply(Beehive tower)
        {
            SignalBus.FireStatic(new LivesGeneratedSignal(10));
        }
    }
    
    public class BigLove : Upgrade<Beehive>
    {
        public override int Cost => 100;
        public override string Description => "Pod koniec każdej rundy generujesz 20 życia.";
        protected override void ExplicitApply(Beehive tower)
        {
            tower.BigLove = true;
        }
    }
}