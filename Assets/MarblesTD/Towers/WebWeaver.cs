using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Marbles.Modifiers;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.Towers
{
    public class WebWeaver : Tower<IWebWeaverView>
    {
        public override int Cost => 60;
        public float Range { get; set; } = 2.4f;
        public override AnimalType AnimalType => AnimalType.NightAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new PoisonousWeb()},
            {UpgradePath.TopLeft, new DeadlyPoison()},

            {UpgradePath.BotMid, new StrongerSlow()},
            {UpgradePath.TopMid, new BiggerWeb()},

            {UpgradePath.BotRight, new Sticky()},
            {UpgradePath.TopRight, new WebWorld()},
        };

        public bool Poisonous;
        public bool EvenSlower;
        public bool SuperPoisonous;
        public bool Sticky;
        public bool SlowAllOnNextUpdate;
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            float slowPotency = EvenSlower ? 1f : 0.65f;
            
            foreach (var marble in marbles)
            {
                if (marble.IsDestroyed) continue;
                
                if (SlowAllOnNextUpdate)
                {
                    marble.ApplyModifier(new TimedSlow(this, marble, slowPotency, 3f));
                }
                
                float distance = Vector2.Distance(Position, marble.Position);
                if (distance <= Range)
                {
                    bool applied = marble.ApplyModifier(new Slow(this, marble, slowPotency));
                    if (applied && Poisonous)
                    {
                        marble.ApplyModifier(new Poison(this, marble, SuperPoisonous ? 3 : 1));
                    }
                }
                else
                {
                    bool removed = marble.RemoveModifier<Slow>(this);
                    if (removed && Sticky)
                    {
                        marble.ApplyModifier(new TimedSlow(this, marble, slowPotency, 3f));
                    }
                }
            }

            SlowAllOnNextUpdate = false;
        }
        
        protected override void OnSelected()
        {
            View.ShowRangeCircle(Range);
        }

        protected override void OnDeselected()
        {
            View.HideRangeCircle();
        }

        protected override void OnTowerPlaced()
        {
            View.HideRangeCircle();
            View.ShowWeb(Range);
        }

        public void Refresh()
        {
            View.ShowRangeCircle(Range);
            View.ShowWeb(Range);
        }
    }
    
    public interface IWebWeaverView : Tower.IView
    {
        void ShowWeb(float range);
        void ShowRangeCircle(float range);
        void HideRangeCircle();
    }
}