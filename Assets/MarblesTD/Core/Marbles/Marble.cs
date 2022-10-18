﻿

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

        public int Speed => _speed;
        public bool IsDestroyed { get; private set; }
        public float DistanceTravelled { get; private set; }

        public Marble(IMarbleView view, Vector2 position, int health, int speed)
        {
            _view = view;
            _view.Marble = this;
            _position = position;
            _health = health;
            _speed = speed;
            
            _view.UpdateMarble(_health);

            Debug.Log($"Creating {GetType()} at position {_position} w {_health}HP");
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Destroy();
                return;
            }
            _view.UpdateMarble(_health);
        }

        public void Update(float distanceTravelled, Vector3 position, Quaternion rotation, bool stop)
        {
            DistanceTravelled = distanceTravelled;
            _position = new Vector2(position.x, position.z);
            
            _view.UpdatePosition(_position);
            _view.UpdateRotation(rotation);

            if (stop)
            {
                Destroy();
            }
        }

        void Destroy()
        {
            _view.DestroySelf();
            IsDestroyed = true;
        }
    }

    public interface IMarbleView
    {
        Marble Marble { get; set; }
        void DestroySelf();
        void UpdatePosition(Vector2 vector2);
        void UpdateRotation(Quaternion rotation);
        void UpdateMarble(int health);
    }
}