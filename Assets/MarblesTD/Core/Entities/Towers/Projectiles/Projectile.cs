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
        ProjectileConfig _config;
        Vector2 _targetPosition;

        float _remainingHits;
        List<Marble> _hitMarbles;

        public Projectile(IProjectileView view, Vector2 position, ProjectileConfig config)
        {
            _view = view;
            _position = position;
            _config = config;
            _view.Projectile = this;
            _hitMarbles = new List<Marble>();

            _remainingHits = config.Pierce;
            _view.UpdateRotation(config.Target.Position);
            _view.HitMarble += OnMarbleHit;

            var targetPos = config.Target.Position;
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
            
            marble.TakeDamage(_config.Damage);
            _remainingHits--;

            if (marble.IsDestroyed)
            {
                _config.Owner.KIllCount++;
            }

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
    }
    
    public readonly struct ProjectileConfig
    {
        public readonly int Damage;
        public readonly int Pierce;
        public readonly float MaxDistance;
        public readonly float Speed;
        public readonly Marble Target;
        public readonly Tower Owner;

        public ProjectileConfig(int damage, int pierce, float maxDistance,  float speed, Marble target, Tower owner)
        {
            Damage = damage;
            Pierce = pierce;
            MaxDistance = maxDistance;
            Speed = speed;
            Target = target;
            Owner = owner;
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