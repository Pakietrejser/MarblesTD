namespace MarblesTD.Core.Common.Signals.List
{
    public readonly struct MarbleDestroyedSignal : ISignal {}
    public readonly struct MarbleDamagedSignal : ISignal {}

    // public readonly struct MarbleWaveSpawnedSignal : ISignal
    // {
    //     public readonly GameObject MarblePrefab;
    //
    //     public MarbleWaveSpawnedSignal(GameObject marblePrefab)
    //     {
    //         MarblePrefab = marblePrefab;
    //     }
    // }
}