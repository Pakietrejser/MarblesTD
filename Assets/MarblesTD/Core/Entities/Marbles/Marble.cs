using System;
using System.Collections.Generic;
using System.Linq;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Marbles.Modifiers;
using MarblesTD.Core.Entities.Towers;
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

        public float Speed => Math.Max(_speed + SpeedModifier, .5f);
        float _speed;
        public float SpeedModifier { get; set; }
        public bool IsDestroyed { get; private set; }
        public float DistanceTravelled { get; private set; }
        public int Path { get; set; }

        public readonly List<Modifier> Modifiers = new List<Modifier>();

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

        public void TakeDamage(int damage, Tower dealer)
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
            dealer.MarblesKilled += cracks;
            _signalBus.Fire(new MarbleDamagedSignal());
            
            if (_health <= 0)
            {
                Destroy();
                return;
            }
            _view.UpdateMarble(_health);
        }

        public void Update(float distanceTravelled, Vector2 position, Quaternion rotation, bool stop, float timeDelta, float timeScale)
        {
            DistanceTravelled = distanceTravelled;
            
            _position = new Vector2(position.x, position.y);
            
            _view.UpdatePosition(_position);
            _view.UpdateRotation(rotation);
            _view.UpdateSorting(distanceTravelled);
            _view.UpdateAnimationSpeed(timeScale);
            
            for (int i = Modifiers.Count - 1; i >= 0; i--)
            {
                var modifier = Modifiers[i];
                if (modifier.IsActive)
                {
                    modifier.Update(timeDelta);
                }
                else
                {
                    modifier.OnRemoved();
                    Modifiers.RemoveAt(i);
                }
            }
            
            if (stop)
            {
                Destroy();
            }
        }

        public bool ApplyModifier(Modifier modifier)
        {
            var modifierOfType = Modifiers.FirstOrDefault(x => x.GetType() == modifier.GetType());
            if (modifierOfType != null && modifierOfType.TryMerge(modifier)) return false;
            
            modifier.OnApplied();
            Modifiers.Add(modifier);
            return true;
        }

        public bool RemoveModifier<T>(Tower tower)
        {
            var modifier = Modifiers.FirstOrDefault(x => x is T);
            if (modifier == null) return false;
            
            return modifier.TryRemove(tower);
        }

        public void TogglePoisonView(bool show) => _view.TogglePoisonView(show);

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
                marble.Modifiers.Clear();
                marble.SpeedModifier = 0;
                marble.Path = 0;
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
        void TogglePoisonView(bool show);
    }
}