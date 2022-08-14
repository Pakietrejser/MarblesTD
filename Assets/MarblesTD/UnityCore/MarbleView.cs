using System;
using MarblesTD.Core.Marbles;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore
{
    public class MarbleView : MonoBehaviour, IMarbleView
    {
        [Header("Renderer")] 
        [SerializeField] SpriteRenderer marbleRenderer;
        [SerializeField] Color[] marbleColors;

        [Header("Animations")]
        [SerializeField] Animator animator;
        [SerializeField] string marbleAnimation;
        [SerializeField] string jawbreakerAnimation;

        string _currentAnimation;
        
        public Marble Marble { get; set; }
        
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
        
        public void UpdatePosition(Vector2 newPosition)
        {
            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.y);
        }

        public void UpdateMarble(int health)
        {
            string newAnimation = health > 6 ? jawbreakerAnimation : marbleAnimation;
            if (_currentAnimation != newAnimation)
            {
                _currentAnimation = newAnimation;
                animator.Play(_currentAnimation);
            }

            if (_currentAnimation == marbleAnimation)
            {
                marbleRenderer.color = marbleColors[health - 1];
            }
        }

        private void OnCollisionEnter(Collision col)
        {
            if (!col.gameObject.TryGetComponent(out MarbleView view)) return;
            
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }
        
    }
}