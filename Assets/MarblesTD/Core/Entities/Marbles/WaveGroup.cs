namespace MarblesTD.Core.Entities.Marbles
{
    public readonly struct WaveGroup
    {
        public readonly int MarbleHealth;
        public readonly int MarbleCount;
        public readonly float MarbleSpeed;
        public readonly float MarbleDelay;
        
        public WaveGroup(int marbleHealth, int marbleCount, float marbleSpeed = 2.5f, float marbleDelay = .4f)
        {
            MarbleHealth = marbleHealth;
            MarbleCount = marbleCount;
            MarbleSpeed = marbleSpeed;
            MarbleDelay = marbleDelay;
        }
    }
}