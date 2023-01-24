using System;
using DG.Tweening;
using MarblesTD.Core.Entities.Marbles;
using UnityEngine;

namespace MarblesTD.UnityCore.Entities
{
    public class MarbleView : MonoBehaviour, IMarbleView
    {
        [SerializeField] GameObject poisoned;
        [SerializeField] GameObject armored;
        
        [Header("Renderer")] 
        [SerializeField] SpriteRenderer marbleRenderer;

        [Header("Animations")]
        [SerializeField] Animator animator;
        [SerializeField] string[] animationStrings;

        string _currentAnimation;
        bool _XL;
        bool _XXL;
        
        public Marble Marble { get; set; }

        void Awake()
        {
            poisoned.SetActive(false);
            armored.SetActive(false);
        }

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
            if (_XL) newAnimation = health > 50 ? animationStrings[6] : animationStrings[Math.Max(0, (int)(health / 10f))];
            if (_XXL) newAnimation = health > 160 ? animationStrings[6] : animationStrings[Math.Max(0, (int)(health / 40f))];
            
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

        public void TogglePoisonView(bool show)
        {
            poisoned.SetActive(show);
        }

        public void ToggleArmorView(bool show)
        {
            armored.SetActive(show);
        }

        public void ToggleXLView(bool show)
        {
            _XL = show;
            transform.DOKill();
            if (show)
            {
                transform.DOScale(Vector3.one * 2, 0.5f);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }

        public void ToggleXXLView(bool show)
        {
            _XXL = show;
            transform.DOKill();
            if (show)
            {
                transform.DOScale(Vector3.one * 3.5f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.TryGetComponent(out MarbleView view)) return;
            
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
    }
}