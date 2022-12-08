namespace MarblesTD.Core.Entities.Marbles
{
    public readonly struct WaveGroup
    {
        public readonly int MarbleHealth;
        public readonly int MarbleCount;
        public readonly int MarbleSpeed;
        public readonly float MarbleDelay;
        
        public WaveGroup(int marbleHealth, int marbleCount, int marbleSpeed = 50, float marbleDelay = .4f)
        {
            MarbleHealth = marbleHealth;
            MarbleCount = marbleCount;
            MarbleSpeed = marbleSpeed;
            MarbleDelay = marbleDelay;
        }
    }
}