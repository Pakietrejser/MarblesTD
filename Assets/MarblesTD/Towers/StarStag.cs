using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class StarStag : Tower<IStarStagView>
    {
        public override bool CanBeStagBuffed => false;
        public override int Cost => 90;
        public override AnimalType AnimalType => AnimalType.WildAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new MorePower()},
            {UpgradePath.TopLeft, new EvenMorePower()},

            {UpgradePath.BotMid, new Twins()},
            {UpgradePath.TopMid, new Eden()},

            {UpgradePath.BotRight, new Boom()},
            {UpgradePath.TopRight, new Gloom()},
        };
        
        public bool BoomOnNextUpdate;
        public bool GloomOnNextUpdate;
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            foreach (var marble in marbles)
            {
                if (GloomOnNextUpdate)
                {
                    marble.TakeDamage(1, this);
                }

                if (BoomOnNextUpdate)
                {
                    marble.TakeDamage(1000, this);
                }
            }
            
            GloomOnNextUpdate = false;
            BoomOnNextUpdate = false;
        }
    }
    
    public interface IStarStagView : Tower.IView
    {
        
    }
}