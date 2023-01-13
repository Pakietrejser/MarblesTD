﻿using System;
using DG.Tweening;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.UnityCore.Common.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.UI
{
    [RequireComponent(typeof(Button))]
    public class PlaceTowerButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Assigned after placing it in the world")]
        [SerializeField] TMP_Text titleText;
        [SerializeField] TowerInfoBox towerInfoBox;
        
        [SerializeField] Image towerImage;
        [SerializeField] TMP_Text costText;
        [SerializeField] Image towerSetColor;
        [SerializeField] Image highlight;
        [SerializeField] GameObject lockedBox;
        [SerializeField] ButtonAudio buttonAudio;

        bool _draggedTowerOutOfPanel;
        bool _canPlaceTowerAtCurrentPosition;
        Tower _currentTower;
        GameObject _currentTowerView;
        Collider2D[] _cashedResult = new Collider2D[2];
        
        void Awake()
        {
            highlight.enabled = false;
        }

        void Start()
        {
            SignalBus.Instance.Subscribe<HoneyChangedSignal>(OnHoneyChanged);
        }

        void OnHoneyChanged(HoneyChangedSignal signal)
        {
            if (lockedBox.activeSelf) return;
            if (_currentTower == null) return;
            costText.color = signal.Honey >= _currentTower.Cost
                ? Color.white
                : new Color(0.87f, 0.23f, 0.28f);
        }

        public void Init(Tower tower, bool unlocked)
        {
            _currentTower = tower;
            
            towerImage.sprite = _currentTower.GetIcon();
            costText.text = $"${_currentTower.Cost}";
            towerSetColor.color = _currentTower.GetColor();
            lockedBox.SetActive(!unlocked);
            buttonAudio.IsActive = unlocked;
        }
        
        public void OnBeginDrag(PointerEventData data)
        {
            if (lockedBox.activeSelf) return;
            _draggedTowerOutOfPanel = false;
        }

        public void OnDrag(PointerEventData data)
        {
            if (lockedBox.activeSelf) return;
            if (EventSystem.current.IsPointerOverGameObject() && !_draggedTowerOutOfPanel)
            {
                return;
            }

            Tower.IView view;
            if (!_draggedTowerOutOfPanel)
            {
                var prefab = _currentTower.GetPrefab();
                _currentTowerView = Instantiate(prefab);
                view = _currentTowerView.GetComponent<Tower.IView>();
                view.DisableCollider();
                _draggedTowerOutOfPanel = true;
            }
            
            if (_currentTowerView == null) return;

            view = _currentTowerView.GetComponent<Tower.IView>();
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // var hit = Physics2D.Raycast(position, Vector2.down, 100);
            
            view.Collider.enabled = true;
            int hit = view.Collider.OverlapCollider(new ContactFilter2D().NoFilter(), _cashedResult);
            view.Collider.enabled = false;
            
            _canPlaceTowerAtCurrentPosition = hit == 1;
            view.ShowAsPlaceable(_canPlaceTowerAtCurrentPosition);
            _currentTowerView.transform.position = new Vector3(position.x, position.y, 0);

            if (hit == 0)
            {
                Destroy(_currentTowerView);
                _currentTowerView = null;
            }
        }

        public async void OnEndDrag(PointerEventData data)
        {
            if (lockedBox.activeSelf) return;
            if (_currentTowerView == null) return;
            if (!_canPlaceTowerAtCurrentPosition)
            {
                Destroy(_currentTowerView);
                _currentTowerView = null;
                return;
            }
            
            bool purchaseCompleted = await Mediator.Instance.SendAsync(new PurchaseRequest(_currentTower.Cost));
            if (purchaseCompleted)
            {
                var view = _currentTowerView.GetComponent<Tower.IView>();
                view.EnableCollider();
                var tower = await Mediator.Instance.SendAsync(new CreateTowerRequest(view, new Vector2(_currentTowerView.transform.position.x, _currentTowerView.transform.position.y), _currentTower.GetType()));
            }
            else
            {
                Destroy(_currentTowerView);
                _currentTowerView = null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowText(_currentTower.GetTranslatedName());
            towerInfoBox.Show(_currentTower);
            if (lockedBox.activeSelf) return;
            highlight.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ShowText("Sojusznicy");
            towerInfoBox.Hide();
            if (lockedBox.activeSelf) return;
            highlight.enabled = false;
        }

        public void ShowText(string text)
        {
            // titleText.text = text;
            // titleText.ChangeAlpha(0);
            // titleText.DOKill();
            // titleText.DOFade(1f, 0.4f);
        }
    }
}