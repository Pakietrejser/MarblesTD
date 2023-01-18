using System.Collections.Generic;
using DG.Tweening;
using MarblesTD.Core.Entities.Marbles;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class MeleeAttackView : MonoBehaviour
    {
        [SerializeField] SpriteRenderer slashA;
        [SerializeField] SpriteRenderer slashB;
        [SerializeField] Collider2D collider2d;
        
        int _damage;
        int _hits;
        Tower _owner;
        bool _doA;
        bool _poisonous;
        readonly List<Marble> _hitMarbles = new List<Marble>();

        void Awake()
        {
            slashA.enabled = false;
            slashB.enabled = false;
            collider2d.enabled = false;
            _doA = true;
        }

        public void Init(Tower owner, int damage, int hits, float attackDuration, bool poisonous)
        {
            _owner = owner;
            _damage = damage;
            _hits = hits;
            _poisonous = poisonous;
            collider2d.enabled = true;
            _hitMarbles.Clear();

            if (_doA)
            {
                slashA.color = new Color(0.03f, 0.17f, 0.13f, 1f);
                slashA.enabled = true;
                slashA.DOKill();
                slashA.DOFade(0f, attackDuration).OnComplete(OnAttackCompleted);
            }
            else
            {
                slashB.color = new Color(0.03f, 0.17f, 0.13f, 1f);
                slashB.enabled = true;
                slashB.DOKill();
                slashB.DOFade(0f, attackDuration).OnComplete(OnAttackCompleted);
            }
            
            _doA = !_doA;
        }

        void OnAttackCompleted()
        {
            slashA.enabled = false;
            slashB.enabled = false;
            collider2d.enabled = false;
            _hitMarbles.Clear();
        }
        
        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.TryGetComponent(out IMarbleView view)) return;

            var marble = view.Marble;
            
            if (_hitMarbles.Contains(marble)) return;
            if (marble.IsDestroyed) return;
            _hitMarbles.Add(marble);

            if (_poisonous) marble.PoisonStacks++;
            marble.TakeDamage(_damage, _owner);
            _hits--;

            if (_hits == 0)
            {
                collider2d.enabled = false;
                _hitMarbles.Clear();
            }
        }
    }
}