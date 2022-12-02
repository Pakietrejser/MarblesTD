using MarblesTD.Core.Marbles;
using UnityEngine;

namespace MarblesTD.UnityCore
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
            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.y);
        }

        public void UpdateRotation(Quaternion rotation)
        {
            marbleRenderer.transform.rotation = Quaternion.Euler(90, rotation.eulerAngles.y + 180, 0);
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

        void OnCollisionEnter(Collision col)
        {
            if (!col.gameObject.TryGetComponent(out MarbleView view)) return;
            
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }
    }
}