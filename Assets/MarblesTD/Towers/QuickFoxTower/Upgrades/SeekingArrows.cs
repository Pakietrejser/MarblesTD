using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers.QuickFoxTower.Upgrades
{
    public class SeekingArrows : Upgrade<QuickFox>
    {
        public override int Cost { get; }
        public override string Description { get; }
        protected override void ExplicitApply(QuickFox tower)
        {
            throw new System.NotImplementedException();
        }
    }
}