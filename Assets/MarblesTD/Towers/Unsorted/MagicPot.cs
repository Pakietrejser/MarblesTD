using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class MagicPot : Tower<IMagicPotView>
    {
        public override int Cost => 1;
        public override AnimalType AnimalType => AnimalType.NightAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>();
            
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta)
        {
        }
    }
    
    public interface IMagicPotView : Tower.IView
    {
        
    }
}