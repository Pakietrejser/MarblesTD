using System.Collections.Generic;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Core.Common.Signals.List
{
    public readonly struct TowerSoldSignal : ISignal
    {
        public readonly int Honey;
        public TowerSoldSignal(int honey) { Honey = honey; }
    }

    public readonly struct TowerCountChangedSignal : ISignal
    {
        public readonly IReadOnlyList<Tower> Towers;
        public TowerCountChangedSignal(IReadOnlyList<Tower> towers) { Towers = towers; }
    }
}