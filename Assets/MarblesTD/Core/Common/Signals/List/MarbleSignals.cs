using UnityEngine;

namespace MarblesTD.Core.Common.Signals.List
{
    public readonly struct MarbleDestroyedSignal : ISignal {}
    public readonly struct MarbleDamagedSignal : ISignal {}

    public readonly struct MarbleWaveBeginSpawnSignal : ISignal
    {
        public readonly GameObject MarblePrefab;

        public MarbleWaveBeginSpawnSignal(GameObject marblePrefab)
        {
            MarblePrefab = marblePrefab;
        }
    }
}