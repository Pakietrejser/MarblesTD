using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.Towers
{
    public class ShadowPaw : Tower<IShadowPawView>
    {
        public int Damage { get; set; } = 1;
        public int Hits { get; set; } = 3;
        public float AttackDuration = 0.4f;
        public float ReloadSpeed { get; set; } = 1.2f;
        public float Range { get; set; } = 2f;
        
        public override int Cost => 80;
        public override AnimalType AnimalType => AnimalType.NightAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new SharpBlades()},
            {UpgradePath.TopLeft, new PoisonousBlades()},

            {UpgradePath.BotMid, new FasterAttack()},
            {UpgradePath.TopMid, new ThirdArm()},

            {UpgradePath.BotRight, new Yoink()},
            {UpgradePath.TopRight, new SweetDeath()},
        };
        
        int _marblesKilled;
        public override int MarblesKilled { 
            get => _marblesKilled;
            set
            {
                int difference = value - _marblesKilled;
                _marblesKilled = value;
                if (StealHoney)
                {
                    SignalBus.FireStatic(new HoneyGeneratedSignal(difference));
                }
            } 
        }

        public bool Poisonous;
        public bool StealHoney;
        public bool UtilizeHoney;
        
        float _reloadTime;
        float _buffModifier;
        
        protected override void OnStagBuffed(StagBuff stagBuff)
        {
            _buffModifier = stagBuff switch
            {
                StagBuff.None => 0,
                StagBuff.Tier1 => .1f,
                StagBuff.Tier2 => .15f,
                StagBuff.Tier3 => .3f,
                _ => throw new ArgumentOutOfRangeException(nameof(stagBuff), stagBuff, null)
            };
        }
        
        public override async void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            _reloadTime -= delta;
            if (!(_reloadTime <= 0) || !SeekClosestMarble(marbles, out var closestMarble)) return;

            if (UtilizeHoney)
            {
                bool purchaseCompleted = await Mediator.Instance.SendAsync(new PurchaseRequest(10));
                if (purchaseCompleted)
                {
                    _reloadTime = (ReloadSpeed - _buffModifier ) / 2f;
                    View.UpdateRotation(closestMarble.Position);
                    View.Strike(this, Damage * 4, Hits * 4, AttackDuration / timeScale, Poisonous);
                    return;
                }
            }
           
            _reloadTime = ReloadSpeed - _buffModifier;
            View.UpdateRotation(closestMarble.Position);
            View.Strike(this, Damage, Hits, AttackDuration / timeScale, Poisonous);
        }
        
        bool SeekClosestMarble(IEnumerable<Marble> marbles, out Marble closestMarble)
        {
            var minDistance = float.MaxValue;
            closestMarble = null;
            
            foreach (var marble in marbles)
            {
                float distance = Vector2.Distance(Position, marble.Position);
                if (distance > Range) continue;
                if (!(distance < minDistance)) continue;
                if (marble.IsDestroyed) continue;
                
                closestMarble = marble;
                minDistance = distance;
            }
        
            return closestMarble != null;
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
        }
    }
    
    public interface IShadowPawView : Tower.IView
    {
        void ShowRangeCircle(float range);
        void HideRangeCircle();
        void Strike(Tower owner, int damage, int hits, float attackDuration, bool poisonous);
    }
}