namespace MarblesTD.Core.Common.Signals.List
{
    public readonly struct TowerSoldSignal : ISignal
    {
        public readonly int Honey;
        public TowerSoldSignal(int honey) { Honey = honey; }
    }
}