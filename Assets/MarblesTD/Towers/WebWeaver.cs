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
        public override int Cost => 1;
        public override AnimalType AnimalType => AnimalType.NightAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>();
            
        
        public int Damage { get; set; } = 1;
        public float Range { get; set; } = 2.4f;
        public float ReloadSpeed { get; set; } = 1.5f;

        public bool Poisonous;
        
        float _reloadTime;
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            var hitAtLeastOne = false;
            foreach (var marble in marbles)
            {
                if (marble.IsDestroyed) continue;
                float distance = Vector2.Distance(Position, marble.Position);
                if (distance > Range)
                {
                    marble.RemoveModifier<Slow>(this);
                    continue;
                }

                hitAtLeastOne = true;
                
                marble.ApplyModifier(new Slow(this, marble));
            }
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