using System;
using System.Collections.Generic;
using System.Linq;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.Towers
{
    public class HalberdBear : Tower<IHalberdBearView>
    {
        public int Damage { get; set; } = 4;
        public float ReloadSpeed { get; set; } = 2f;
        public float Range { get; set; } = 2.5f;
        
        public override int Cost => 300;
        public override AnimalType AnimalType => AnimalType.NobleAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new BearStrength()},
            {UpgradePath.TopLeft, new MarbleCrusher()},

            {UpgradePath.BotMid, new FasterSwing()},
            {UpgradePath.TopMid, new AutoSwing()},

            {UpgradePath.BotRight, new Sweep()},
            {UpgradePath.TopRight, new Whirlwind()},
        };
        
        int _buffModifier;
        
        protected override void OnStagBuffed(StagBuff stagBuff)
        {
            _buffModifier = stagBuff switch
            {
                StagBuff.None => 0,
                StagBuff.Tier1 => 2,
                StagBuff.Tier2 => 4,
                StagBuff.Tier3 => 8,
                _ => throw new ArgumentOutOfRangeException(nameof(stagBuff), stagBuff, null)
            };
        }
            
        public int Angle = 45;
        public bool FasterSwing;
        public bool AutoSwing;
        
        float _reloadTime;
        const float ReloadErrorMargin = 0.01f;
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            _reloadTime -= delta;
            if (!(_reloadTime <= 0) || !SeekClosestFirstMarble(marbles, out var closestMarble)) return;

            float attackDuration = ReloadSpeed / timeScale * (Angle / 360f);
            if (FasterSwing && Angle == 360) attackDuration = (ReloadSpeed - .6f) / timeScale * (Angle / 360f);
            _reloadTime = AutoSwing ? attackDuration : FasterSwing ? ReloadSpeed - .6f : ReloadSpeed;
            _reloadTime += ReloadErrorMargin;
            
            View.Attack(this, Damage + _buffModifier, closestMarble.Position, Angle, attackDuration);
        }
        
        bool SeekClosestFirstMarble(IEnumerable<Marble> marbles, out Marble closestMarble)
        {
            var marblesArray = marbles
                .Where(marble 
                    => !marble.IsDestroyed 
                       && Vector2.Distance(Position, marble.Position) <= Range)
                .ToArray();

            closestMarble = marblesArray.Length == 0 ? null : marblesArray[0];
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
    
    public interface IHalberdBearView : Tower.IView
    {
        void ShowRangeCircle(float range);
        void HideRangeCircle();
        void Attack(HalberdBear owner, int damage, Vector2 target, int angle, float attackDuration);
    }
}