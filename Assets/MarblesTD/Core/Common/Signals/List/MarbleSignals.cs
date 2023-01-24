using MarblesTD.Core.Entities.Marbles;
using UnityEngine;

namespace MarblesTD.Core.Common.Signals.List
{
    public readonly struct MarbleDamagedSignal : ISignal {}

    public readonly struct SpawnBonusMarblesSignal : ISignal
    {
        public readonly float DistanceTravelled;
        public readonly Vector2 Position;
        public readonly int PathIndex;
        public readonly WaveGroup WaveGroup;

        public SpawnBonusMarblesSignal(WaveGroup waveGroup, Vector2 position, int pathIndex, float distanceTravelled)
        {
            DistanceTravelled = distanceTravelled;
            WaveGroup = waveGroup;
            Position = position;
            PathIndex = pathIndex;
        }
    }
}