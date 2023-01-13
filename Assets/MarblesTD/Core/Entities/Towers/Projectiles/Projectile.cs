using System;
using System.Collections.Generic;
using MarblesTD.Core.Entities.Marbles;
using UnityEngine;

namespace MarblesTD.Core.Entities.Towers.Projectiles
{
    public class Projectile
    {
        IProjectileView _view;
        Vector2 _position;
        ArrowConfig _config;
        Vector2 _targetPosition;

        float _remainingHits;
        List<Marble> _hitMarbles;

        bool _seeking;
        
        public Projectile(IProjectileView view, Vector2 position, ArrowConfig config)
        {
            _view = view;
            _position = position;
            _config = config;
            _view.Projectile = this;
            _hitMarbles = new List<Marble>();
            _seeking = config.Seeking;
            
            _remainingHits = config.Pierce;
            _view.UpdateRotation(config.TargetPosition);
            _view.HitMarble += OnMarbleHit;

            var targetPos = config.TargetPosition;
            float currentDistance = Vector2.Distance(position, targetPos);
            float distanceToAdjust = config.MaxDistance / currentDistance;
            _targetPosition = new Vector2(
                position.x +(targetPos.x - position.x) * distanceToAdjust,
                position.y +(targetPos.y - position.y) * distanceToAdjust
                );
            
            // Debug.Log($"Creating {GetType()} at position {_position}, target position {_targetPosition}");
        }

        void OnMarbleHit(Marble marble)
        {
            if (_hitMarbles.Contains(marble)) return;
            _hitMarbles.Add(marble);
            
            marble.TakeDamage(_config.Damage, _config.Owner);
            _remainingHits--;

            if (_remainingHits == 0)
            {
                _view.DestroySelf();
            }
        }

        public void Update(float delta)
        {
            _position = Vector2.MoveTowards(_position, _targetPosition, _config.Speed * delta);
            _view.UpdatePosition(_position);

            if (_position == _targetPosition)
            {
                _view.DestroySelf();
            }
        }

        public void Destroy() => _view.DestroySelf();
    }
    
    public readonly struct ArrowConfig
    {
        public readonly int Damage;
        public readonly int Pierce;
        public readonly float MaxDistance;
        public readonly float Speed;
        public readonly Vector2 TargetPosition;
        public readonly Tower Owner;
        public readonly bool Seeking;

        public ArrowConfig(int damage, int pierce, float maxDistance,  float speed, Vector2 targetPosition, Tower owner, bool seeking)
        {
            Damage = damage;
            Pierce = pierce;
            MaxDistance = maxDistance;
            Speed = speed;
            TargetPosition = targetPosition;
            Owner = owner;
            Seeking = seeking;
        }
    }

    public interface IProjectileView
    {
        Projectile Projectile { get; set; }
        event Action<Marble> HitMarble;
        void UpdatePosition(Vector2 targetPosition);
        void UpdateRotation(Vector2 target);
        void DestroySelf();
    }
}