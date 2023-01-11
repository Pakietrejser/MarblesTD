namespace MarblesTD.Core.Common.Signals.List
{
    public readonly struct HoneyChangedSignal : ISignal
    {
        public readonly int Honey;
        public HoneyChangedSignal(int honey) { Honey = honey; }
    }
}