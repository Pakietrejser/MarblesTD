using System;
using System.Collections.Generic;
using System.Linq;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.Towers
{
    public class StarStag : Tower<IStarStagView>
    {
        public override int Cost => 140;
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

        public int SupportedTowers = 1;
        public StagBuff DistributedBuff = StagBuff.Tier1;
        public bool BoomOnNextUpdate;
        public bool SuperBoomOnNextUpdate;
        
        
        void OnTowerCountChanged(TowerCountChangedSignal signal)
        {
            var availableTowers = signal.Towers.Where(tower => !(tower is StarStag) && (int)tower.StagBuff < (int)DistributedBuff).ToArray();
            SortTowersByDistance(ref availableTowers);

            int count = Math.Min(SupportedTowers, availableTowers.Length);
            for (var i = 0; i < count; i++)
            {
                var tower = availableTowers[i];
                tower.StagBuff = DistributedBuff;
            }
        }

        void SortTowersByDistance(ref Tower[] arr)
        {
            for (var i = 0; i < arr.Length; i++) 
            for (var j = 0; j < arr.Length - 1; j++)
            {
                float distanceA = Vector2.Distance(Position, arr[j].Position);
                float distanceB = Vector2.Distance(Position, arr[j + 1].Position);
                if (!(distanceA > distanceB)) continue;
                (arr[j + 1], arr[j]) = (arr[j], arr[j + 1]);
            }
        }
        
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            if (!SuperBoomOnNextUpdate && !BoomOnNextUpdate) return;
            
            foreach (var marble in marbles)
            {
                marble.TakeDamage(BoomOnNextUpdate ? 1 : 1000, this);
            }
            
            SuperBoomOnNextUpdate = false;
            BoomOnNextUpdate = false;
        }

        protected override void OnStagBuffed(StagBuff stagBuff)
        {
            
        }

        protected override void OnTowerPlaced()
        {
            SignalBus.Instance.SubscribeId<TowerCountChangedSignal>(DistributedBuff, OnTowerCountChanged);
        }

        protected override void OnTowerRemoved()
        {
            SignalBus.Instance.UnsubscribeId<TowerCountChangedSignal>(StagBuff.Tier1, OnTowerCountChanged);
            SignalBus.Instance.UnsubscribeId<TowerCountChangedSignal>(StagBuff.Tier2, OnTowerCountChanged);
            SignalBus.Instance.UnsubscribeId<TowerCountChangedSignal>(StagBuff.Tier3, OnTowerCountChanged);
        }

        public void RefreshSignal()
        {
            SignalBus.Instance.UnsubscribeId<TowerCountChangedSignal>(StagBuff.Tier1, OnTowerCountChanged);
            SignalBus.Instance.UnsubscribeId<TowerCountChangedSignal>(StagBuff.Tier2, OnTowerCountChanged);
            SignalBus.Instance.UnsubscribeId<TowerCountChangedSignal>(StagBuff.Tier3, OnTowerCountChanged);
            SignalBus.Instance.SubscribeId<TowerCountChangedSignal>(DistributedBuff, OnTowerCountChanged);
        }
    }
    
    public interface IStarStagView : Tower.IView
    {
        
    }
}