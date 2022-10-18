using System;
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
            // UpdateRotation(newPosition);
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
        
        // void UpdateRotation(Vector2 target)
        // {
        //     var current = transform.position;
        //     float x = target.x - current.x;
        //     float y = target.y - current.z;
        //     var rotation = (float) (Math.Atan2(y, x) * 180 / Math.PI);
        //
        //     if (rotation < 0)
        //     {
        //         rotation = Math.Abs(rotation) + 90;
        //     }
        //     else if (rotation > 90 )
        //     {
        //         rotation = 270 + Math.Abs(rotation - 180);
        //     }
        //     else
        //     {
        //         rotation = Math.Abs(rotation - 90);
        //     }
        //     
        //     marbleRenderer.transform.rotation = Quaternion.Euler(90, rotation + 180, 0);
        // }

        void OnCollisionEnter(Collision col)
        {
            if (!col.gameObject.TryGetComponent(out MarbleView view)) return;
            
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }
    }
}