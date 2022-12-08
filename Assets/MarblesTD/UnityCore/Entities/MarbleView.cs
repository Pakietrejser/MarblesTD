using MarblesTD.Core.Entities.Marbles;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class MarbleView : MonoBehaviour, IMarbleView
    {
        [Header("Renderer")] 
        [SerializeField] SpriteRenderer marbleRenderer;

        [Header("Animations")]
        [SerializeField] Animator animator;
        [SerializeField] string[] animationStrings;

        string _currentAnimation;
        
        public Marble Marble { get; set; }
        
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
        
        public void UpdatePosition(Vector2 newPosition)
        {
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }

        public void UpdateRotation(Quaternion rotation)
        {
            marbleRenderer.transform.rotation = rotation.eulerAngles.y < 100 
                ? Quaternion.Euler(0, 0, rotation.eulerAngles.y - rotation.eulerAngles.x) 
                : Quaternion.Euler(0, 0, rotation.eulerAngles.x - 90);
        }

        public void UpdateMarble(int health)
        {
            string newAnimation = health > 6 ? animationStrings[6] : animationStrings[health - 1];
            if (_currentAnimation != newAnimation)
            {
                _currentAnimation = newAnimation;
                animator.Play(_currentAnimation);
            }
        }

        public void UpdateSorting(float distanceTravelled)
        {
            marbleRenderer.sortingOrder = (int) -(distanceTravelled * 10);
        }

        public void UpdateAnimationSpeed(float speed)
        {
            animator.speed = speed;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.TryGetComponent(out MarbleView view)) return;
            
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
    }
}