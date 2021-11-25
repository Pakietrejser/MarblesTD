

using UnityEngine;

namespace MarblesTD.Core.Marbles
{
    public class Marble
    {
        public Vector2 Position => _position;
        
        private IMarbleView _view;
        private Vector2 _position;
        private int _health;
        private int _speed;

        public Marble(IMarbleView view, Vector2 position, int health, int speed)
        {
            _view = view;
            _view.Marble = this;
            _position = position;
            _health = health;
            _speed = speed;

            Debug.Log($"Creating {GetType()} at position {_position} w {_health}HP");
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _view.DestroySelf();
            }
        }

        public void Update(Transform endPosition, float deltaTime)
        {
            _position = Vector2.MoveTowards(_position, new Vector2(endPosition.position.x, endPosition.position.z), _speed * deltaTime);
            _view.UpdatePosition(_position);

            if (_position == new Vector2(endPosition.position.x, endPosition.position.z))
            {
                _view.DestroySelf();
            }
        }
    }

    public interface IMarbleView
    {
        Marble Marble { get; set; }
        void DestroySelf();
        void UpdatePosition(Vector2 vector2);
    }
}