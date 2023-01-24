using System.Collections.Generic;
using MarblesTD.Core.Entities.Marbles.Modifiers;

namespace MarblesTD.Core.Entities.Marbles
{
    public readonly struct WaveGroup
    {
        public readonly int MarbleHealth;
        public readonly int MarbleCount;
        public readonly float MarbleSpeed;
        public readonly float MarbleDelay;
        public readonly List<Modifier> Modifiers;

        public WaveGroup(int marbleHealth, int marbleCount, float marbleSpeed = 2.5f, float marbleDelay = .4f, params Modifier[] modifiers)
        {
            MarbleHealth = marbleHealth;
            MarbleCount = marbleCount;
            MarbleSpeed = marbleSpeed;
            MarbleDelay = marbleDelay;
            Modifiers = new List<Modifier>(modifiers);
        }
        
        public WaveGroup(int marbleHealth, int marbleCount, params Modifier[] modifiers)
        {
            MarbleHealth = marbleHealth;
            MarbleCount = marbleCount;
            MarbleSpeed = 2.5f;
            MarbleDelay = .4f;
            Modifiers = new List<Modifier>(modifiers);
        }
    }
}