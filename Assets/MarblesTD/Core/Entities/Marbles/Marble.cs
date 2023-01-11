﻿using System;
using MarblesTD.Core.Common.Signals.List;
using UnityEngine;
using Zenject;
using SignalBus = MarblesTD.Core.Common.Signals.SignalBus;

namespace MarblesTD.Core.Entities.Marbles
{
    public class Marble
    {
        readonly SignalBus _signalBus;
        public static event Action<Marble, int> Cracked;

        public Vector2 Position => _position;
        public int Health => _health;

        IMarbleView _view;
        Vector2 _position;
        int _health;
        float _speed;

        public float Speed => _speed;
        public bool IsDestroyed { get; private set; }
        public float DistanceTravelled { get; private set; }

        public Marble(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Init(IMarbleView view, Vector2 position, int health, float speed)
        {
            _view = view;
            _view.Marble = this;
            _position = position;
            _health = health;
            _speed = speed;

            _view.UpdateMarble(_health);
        }

        public void TakeDamage(int damage)
        {
            int cappedDamage = Math.Min(damage, _health);

            var cracks = 0;
            for (var i = 0; i < cappedDamage; i++)
            {
                _health--;
                if (_health < 6)
                {
                    cracks++;
                }
            }
            Cracked?.Invoke(this, cracks);
            _signalBus.Fire(new MarbleDamagedSignal());
            
            if (_health == 0)
            {
                Destroy();
                return;
            }
            _view.UpdateMarble(_health);
        }

        public void Update(float distanceTravelled, Vector2 position, Quaternion rotation, bool stop, float animationSpeed)
        {
            DistanceTravelled = distanceTravelled;
            
            _position = new Vector2(position.x, position.y);
            
            _view.UpdatePosition(_position);
            _view.UpdateRotation(rotation);
            _view.UpdateSorting(distanceTravelled);
            _view.UpdateAnimationSpeed(animationSpeed);
            
            if (stop)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            if (IsDestroyed) return;
            _view.DestroySelf();
            IsDestroyed = true;
        }
        
        public class Pool : MemoryPool<Marble>
        {
            protected override void Reinitialize(Marble marble)
            {
                marble.IsDestroyed = false;
                marble._position = Vector2.zero;
                marble._health = 999;
                marble.DistanceTravelled = 0;
            }
        }
    }

    public interface IMarbleView
    {
        Marble Marble { get; set; }
        void DestroySelf();
        void UpdatePosition(Vector2 vector2);
        void UpdateRotation(Quaternion rotation);
        void UpdateMarble(int health);
        void UpdateSorting(float distanceTravelled);
        void UpdateAnimationSpeed(float speed);
    }
}