using System;
using System.Collections.Generic;
using System.Linq;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;

namespace MarblesTD.Towers
{
    public class Mastiffteer : Tower<IMastiffteerView>
    {
        public override int Cost => 60;
        public override AnimalType AnimalType => AnimalType.NobleAnimal;
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new SilverBullet()},
            {UpgradePath.TopLeft, new BouncingBullet()},

            {UpgradePath.BotMid, new QuickReload()},
            {UpgradePath.TopMid, new Heroism()},

            {UpgradePath.BotRight, new OneForAll()},
            {UpgradePath.TopRight, new AllForOne()},
        };
        
        public int Damage { get; set; } = 2;
        public float ReloadSpeed { get; set; } = 2f;
        public bool BouncingBullet = false;
        public bool Heroism = false;
        public bool AllForOne = false;
        public bool OneForAll = false;

        public static int OneForAllBuffs = 0;
        static int Mastiffteers = 0;

        float _reloadTime;
        int _buffModifier;
        
        protected override void OnStagBuffed(StagBuff stagBuff)
        {
            _buffModifier = stagBuff switch
            {
                StagBuff.None => 0,
                StagBuff.Tier1 => 1,
                StagBuff.Tier2 => 2,
                StagBuff.Tier3 => 5,
                _ => throw new ArgumentOutOfRangeException(nameof(stagBuff), stagBuff, null)
            };
        }
            
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            var marblesArray = marbles.Where(x => !x.IsDestroyed).ToArray();
            _reloadTime -= delta;
            if (_reloadTime <= 0)
                _reloadTime = Heroism 
                    ? Math.Max(0.1f, ReloadSpeed - 0.05f * marblesArray.Length)
                    : ReloadSpeed;
            else
                return;
            
            if (marblesArray.Length == 0) return;

            int totalDamage = Damage + _buffModifier;
            
            totalDamage += (int)(OneForAllBuffs * 0.5f);
            if (AllForOne) totalDamage += Mastiffteers * 2;

            var target = marblesArray[0];
            View.UpdateRotation(target.Position);
            View.ShowGunShot();
            target.TakeDamage(totalDamage, this);

            if (BouncingBullet)
            {
                if (marblesArray.Length >= 2) marblesArray[1].TakeDamage(totalDamage, this);
                if (marblesArray.Length >= 3) marblesArray[2].TakeDamage(totalDamage, this);
            }
        }

        protected override void OnTowerPlaced()
        {
            Mastiffteers++;
        }

        protected override void OnTowerRemoved()
        {
            Mastiffteers--;
            if (OneForAll) OneForAllBuffs--;
        }
    }
    
    public interface IMastiffteerView : Tower.IView
    {
        void ShowGunShot();
    }
}