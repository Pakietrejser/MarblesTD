﻿namespace MarblesTD.Core.ScenarioSystems
{
    public class Player
    {
        public int Lives { get; private set; }
        public int Money { get; private set; }

        public static Player Instance;
        readonly IPlayerView view;

        public Player(IPlayerView view)
        {
            Instance = this;
            this.view = view;
        }

        public void AddLives(int amount)
        {
            Lives += amount;
            view.UpdateLives(Lives);
        }

        public void RemoveLives(int amount)
        {
            Lives -= amount;
            view.UpdateLives(Lives);
        }

        public void AddMoney(int amount)
        {
            Money += amount;
            view.UpdateMoney(Money);
        }

        public void RemoveMoney(int amount)
        {
            Money -= amount;
            view.UpdateMoney(Money);
        }
    }

    public interface IPlayerView
    {
        void UpdateLives(int lives);
        void UpdateMoney(int money);
    }
}