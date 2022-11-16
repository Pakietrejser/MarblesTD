using System.Collections.Generic;
using MarblesTD.Core.Entities.Marbles;

namespace MarblesTD.Core.Systems.Waves
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