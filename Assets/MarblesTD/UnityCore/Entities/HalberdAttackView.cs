using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class HalberdAttackView : MonoBehaviour
    {
        [SerializeField] Collider2D collider2d;
        
        int _damage;
        Tower _owner;
        readonly List<Marble> _hitMarbles = new List<Marble>();

        void Awake()
        {
            collider2d.enabled = false;
        }

        public async void ActivateHalberdFor(float duration, HalberdBear owner, int damage)
        {
            _owner = owner;
            _damage = damage;
            collider2d.enabled = true;
            
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            
            _hitMarbles.Clear();
            collider2d.enabled = false;
        }
        
        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.TryGetComponent(out IMarbleView view)) return;

            var marble = view.Marble;
            
            if (_hitMarbles.Contains(marble)) return;
            if (marble.IsDestroyed) return;
            _hitMarbles.Add(marble);

            marble.TakeDamage(_damage, _owner);
        }
    }
}