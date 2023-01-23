using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.Towers
{
    public class MagicPot : Tower<IMagicPotView>
    {
        public override int Cost => 200;
        public override AnimalType AnimalType => AnimalType.NightAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new HotPot()},
            {UpgradePath.TopLeft, new LavaPot()},

            {UpgradePath.BotMid, new ToxicFumes()},
            {UpgradePath.TopMid, new Corrosion()},

            {UpgradePath.BotRight, new BiggerRange()},
            {UpgradePath.TopRight, new GiganticRange()},
        };
            
        public int Damage { get; set; } = 1;
        public float Range { get; set; } = 2.2f;
        public float ReloadSpeed { get; set; } = 1.5f;

        public bool Corrosion;
        
        float _reloadTime;
        float _buffModifier;
        
        protected override void OnStagBuffed(StagBuff stagBuff)
        {
            _buffModifier = stagBuff switch
            {
                StagBuff.None => 0,
                StagBuff.Tier1 => .13f,
                StagBuff.Tier2 => .25f,
                StagBuff.Tier3 => .25f,
                _ => throw new ArgumentOutOfRangeException(nameof(stagBuff), stagBuff, null)
            };
        }
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            _reloadTime -= delta;

            if (_reloadTime <= 0)
            {
                _reloadTime = ReloadSpeed - _buffModifier;

                var hitAtLeastOne = false;
                foreach (var marble in marbles)
                {
                    float distance = Vector2.Distance(Position, marble.Position);
                    if (distance > Range) continue;
                    if (marble.IsDestroyed) continue;

                    hitAtLeastOne = true;

                    if (Corrosion && marble.Health > 6)
                    {
                        marble.TakeDamage((Damage + StagBuff == StagBuff.Tier3 ? 1 : 0) * 5, this);
                    }
                    else
                    {
                        marble.TakeDamage(Damage + StagBuff == StagBuff.Tier3 ? 1 : 0 , this);
                    }
                }

                if (!hitAtLeastOne) _reloadTime = 0.1f;
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
            View.ShowBurn(Range);
        }

        public void Refresh()
        {
            View.ShowRangeCircle(Range);
            View.ShowBurn(Range);
        }
    }
    
    public interface IMagicPotView : Tower.IView
    {
        void ShowBurn(float range);
        void ShowRangeCircle(float range);
        void HideRangeCircle();
    }
}