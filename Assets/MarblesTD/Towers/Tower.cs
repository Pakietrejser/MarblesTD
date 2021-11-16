using System.Collections.Generic;
using UnityEngine;

namespace MarblesTD.Towers
{
    public class Tower
    {
        private readonly ITowerView _view;
        private readonly Vector2 _position;
        private readonly float _attackSpeed;
        private readonly float _range;

        private float _timeUntilNextAttack;

        public Tower(ITowerView view, Vector3 position, float attackSpeed, float range)
        {
            _view = view;
            _position = position;
            _attackSpeed = attackSpeed;
            _range = range;
            _timeUntilNextAttack = attackSpeed;

            Debug.Log($"Creating {GetType()} at position {_position}");
        }

        public void Update(IEnumerable<MarblePlacement> marblePlacements, float delta)
        {
            _timeUntilNextAttack -= delta;
            if (_timeUntilNextAttack <= 0)
            {
                _timeUntilNextAttack = _attackSpeed;
            }
            else
            {
                return;
            }
            
            if (!SeekClosestMarble(marblePlacements, out var closestMarble)) return;

            var projectileConfig = new ProjectileConfig(10, 2, 10, 10, closestMarble);
            _view.SpawnProjectile(projectileConfig);
        }

        private bool SeekClosestMarble(IEnumerable<MarblePlacement> marblePlacements, out Marble closestMarble)
        {
            float minDistance = float.MaxValue;
            closestMarble = null;
            
            foreach (var marblePlacement in marblePlacements)
            {
                float distance = Vector2.Distance(_position, marblePlacement.Position);
                if (distance > _range) continue;
                if (!(distance < minDistance)) continue;
                
                closestMarble = marblePlacement.Marble;
                minDistance = distance;
            }

            return closestMarble != null;
        }
    }

    public interface ITowerView
    {
        Projectile SpawnProjectile(ProjectileConfig config);
    }

    public readonly struct MarblePlacement
    {
        public readonly Marble Marble;
        public readonly Vector2 Position;
        
        public MarblePlacement(Marble marble, Vector2 position)
        {
            Marble = marble;
            Position = position;
        }
    }
}
