using System.Collections.Generic;

namespace MarblesTD.Core.Entities.Marbles
{
    public class MarbleWave
    {
        public int WaveIndex { get; }
        public bool FinishedSpawning { get; set; }
        
        public readonly List<Marble> Marbles;

        public MarbleWave(int waveIndex)
        {
            WaveIndex = waveIndex;
            FinishedSpawning = false;
            Marbles = new List<Marble>();
        }

        public void Add(Marble marble)
        {
            Marbles.Add(marble);
        }
    }
}