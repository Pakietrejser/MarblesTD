using System;
using System.Collections.Generic;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Core.ScenarioSystems;

namespace MarblesTD.Towers
{
    public class Beehive : Tower<IBeehiveView>
    {
        public override int Cost => 100;
        public override AnimalType AnimalType => AnimalType.NobleAnimal;

        public int HoneyCapPerRound = 30;
        public int  HoneyGeneratedEveryReload = 5;
        public float ReloadSpeed { get; set; } = 1.5f;
        public bool DoubleDown;
        public bool BigLove;

        float _honeyGeneratedThisRound;
        float _reloadTime;
        int _buffModifier;
        
        protected override void OnStagBuffed(StagBuff stagBuff)
        {
            _buffModifier = stagBuff switch
            {
                StagBuff.None => 0,
                StagBuff.Tier1 => 20,
                StagBuff.Tier2 => 40,
                StagBuff.Tier3 => 120,
                _ => throw new ArgumentOutOfRangeException(nameof(stagBuff), stagBuff, null)
            };
        }
        
        public override Dictionary<UpgradePath, Upgrade> Upgrades { get; } = new Dictionary<UpgradePath, Upgrade>()
        {
            {UpgradePath.BotLeft, new BiggerHive()},
            {UpgradePath.TopLeft, new GiganticHive()},

            {UpgradePath.BotMid, new FasterBees()},
            {UpgradePath.TopMid, new DoubleDown()},

            {UpgradePath.BotRight, new LoveIsLove()},
            {UpgradePath.TopRight, new BigLove()},
        };
            
        public override void UpdateTower(IEnumerable<Marble> marbles, float delta, float timeScale)
        {
            _reloadTime -= delta;

            if (_reloadTime <= 0 && _honeyGeneratedThisRound < HoneyCapPerRound + _buffModifier)
            {
                _reloadTime = ReloadSpeed;
                _honeyGeneratedThisRound += HoneyGeneratedEveryReload;
                SignalBus.FireStatic(new HoneyGeneratedSignal(HoneyGeneratedEveryReload));
                View.ShowHoney(HoneyGeneratedEveryReload);
            }
        }
        
        void OnRoundStarted(RoundStartedSignal signal)
        {
            _honeyGeneratedThisRound = 0;
        }

        void OnRoundEnded(RoundEndedSignal signal)
        {
            if (DoubleDown)
            {
                SignalBus.FireStatic(new HoneyGeneratedSignal(signal.HoneyReward));
                View.ShowHoney(signal.HoneyReward);
            }

            if (BigLove)
            {
                SignalBus.FireStatic(new LivesGeneratedSignal(20));
            }
        }

        protected override void OnTowerPlaced()
        {
            _honeyGeneratedThisRound = MarbleController.ProcessingRound ? 0 : 99999;
            SignalBus.Instance.Subscribe<RoundStartedSignal>(OnRoundStarted);
            SignalBus.Instance.Subscribe<RoundEndedSignal>(OnRoundEnded);
        }

        protected override void OnTowerRemoved()
        {
            SignalBus.Instance.Unsubscribe<RoundStartedSignal>(OnRoundStarted);
            SignalBus.Instance.Unsubscribe<RoundEndedSignal>(OnRoundEnded);
        }
    }
    
    public interface IBeehiveView : Tower.IView
    {
        public void ShowHoney(int text);
    }
}