namespace MarblesTD.Core.Common.Signals.List
{
    public readonly struct HoneyChangedSignal : ISignal
    {
        public readonly int Honey;
        public HoneyChangedSignal(int honey) { Honey = honey; }
    }

    public readonly struct HoneyGeneratedSignal : ISignal
    {
        public readonly int Honey;

        public HoneyGeneratedSignal(int honey)
        {
            Honey = honey;
        }
    }
    
    public readonly struct LivesGeneratedSignal : ISignal
    {
        public readonly int Lives;

        public LivesGeneratedSignal(int lives)
        {
            Lives = lives;
        }
    }
}