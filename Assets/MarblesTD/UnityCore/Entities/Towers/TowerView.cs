using System;
using DG.Tweening;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarblesTD.UnityCore.Entities.Towers
{
    public abstract class TowerView : MonoBehaviour, Tower.IView
    {
        [SerializeField] SpriteRenderer selectRenderer;
        [SerializeField] Collider2D towerCollider;
        [SerializeField] Transform stagBuffStar;

        public Collider2D Collider => towerCollider;

        SpriteRenderer[] _renderers;
        protected virtual void Awake()
        {
            _renderers = GetComponentsInChildren<SpriteRenderer>();
            stagBuffStar.gameObject.SetActive(false);
        }

        public event Action Clicked;

        public void Select()
        {
            selectRenderer.enabled = true;
        }

        public void Deselect()
        {
            selectRenderer.enabled = false;
        }

        void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Clicked?.Invoke();
        }
        
        public void DestroySelf() => Destroy(gameObject);
        
        public void EnableCollider()
        {
            towerCollider.enabled = true;
        }

        public void DisableCollider()
        {
            towerCollider.enabled = false;
        }

        public void ShowAsPlaceable(bool canBePlaced)
        {
            foreach (var spriteRenderer in _renderers)
            {
                spriteRenderer.color = canBePlaced ? Color.white : Color.red;
            }
        }
        
        public void UpdateRotation(Vector2 target)
        {
            var current = transform.position;
            float x = target.x - current.x;
            float y = target.y - current.y;
            var rotation = (float) (Math.Atan2(y, x) * 180 / Math.PI);
            transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
        }

        public void ShowStagBuff(StagBuff stagBuff)
        {
            float rotationSpeed = stagBuff switch
            {
                StagBuff.None => 0,
                StagBuff.Tier1 => 24f,
                StagBuff.Tier2 => 7f,
                StagBuff.Tier3 => 2f,
                _ => throw new ArgumentOutOfRangeException(nameof(stagBuff), stagBuff, null)
            };
            
            stagBuffStar.gameObject.SetActive(stagBuff != StagBuff.None);
            stagBuffStar.DOKill();
            
            if (stagBuff == StagBuff.None) return;
            stagBuffStar.DORotate(new Vector3(0, 0, transform.rotation.y - 360), rotationSpeed, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }
}