using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers.CannonBoarTower
{
    public class CannonBoar : Tower
    {
        public override int Cost => 1;
        public override AnimalType AnimalType => AnimalType.WildAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>();
            
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta)
        {
        }
    }
}