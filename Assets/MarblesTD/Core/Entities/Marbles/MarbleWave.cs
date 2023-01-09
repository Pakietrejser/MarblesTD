using System.Collections.Generic;

namespace MarblesTD.Core.Entities.Marbles
{
    public class MarbleWave
    {
        public readonly int WaveIndex;
        public int HoneyReward { get; }
        public bool FinishedSpawning { get; set; }
        
        public readonly List<Marble> Marbles;

        public MarbleWave(int honeyReward, int waveIndex)
        {
            WaveIndex = waveIndex;
            HoneyReward = honeyReward;
            FinishedSpawning = false;
            Marbles = new List<Marble>();
        }

        public void Add(Marble marble)
        {
            Marbles.Add(marble);
        }
    }
}