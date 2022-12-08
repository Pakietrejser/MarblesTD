using System;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.UnityCore.Entities.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.UI
{
    [RequireComponent(typeof(Button))]
    public class PlaceTowerButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] Image towerImage;
        [SerializeField] TMP_Text costText;
        [SerializeField] Image towerSetColor;
        [Space]
        [SerializeField] private GameObject towerPrefab;
        [Space]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float yPlacingHeight;

        bool _draggedTowerOutOfPanel;
        bool _canPlaceTowerAtCurrentPosition;
        
        Button towerButton;
        GameObject currentTower;

        Tower.SettingsBase settings;
        GlobalTowerSettings global;

        private void Awake()
        {
            towerButton = GetComponent<Button>();
        }

        public void Init(Tower.SettingsBase settingsBase, GlobalTowerSettings globalTowerSettings)
        {
            settings = settingsBase;
            global = globalTowerSettings;
            
            towerImage.sprite = settingsBase.Icon;
            costText.text = $"${settingsBase.Cost}";
            towerSetColor.color = globalTowerSettings.GetTowerTypeSettings(settingsBase.TowerType).Color;

            if (settingsBase.Cost == 0) costText.text = "NYI";
        }
        
        
        public void OnBeginDrag(PointerEventData data)
        {
            currentTower = Instantiate(towerPrefab);
            _draggedTowerOutOfPanel = false;
            var view = currentTower.GetComponent<ITowerView>();
            view.Init(settings.Icon, settings.TowerType);
            view.DisableCollider();
        }

        public void OnDrag(PointerEventData data)
        {
            if (currentTower == null) return;
            if (Camera.main == null) throw new NullReferenceException();
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (EventSystem.current.IsPointerOverGameObject() && !_draggedTowerOutOfPanel)
            {
                return;
            }

            _draggedTowerOutOfPanel = true;

            if (Physics.Raycast(ray, out var hit, 1000.0f))
            {
                _canPlaceTowerAtCurrentPosition = hit.collider.gameObject.name == "Map";
                var view = currentTower.GetComponent<ITowerView>();
                view.ShowAsPlaceable(_canPlaceTowerAtCurrentPosition);
                
                
                var go = hit.collider.gameObject.name;
                // Debug.Log($"{go}");
                
                
                currentTower.transform.position = hit.point + Vector3.up * yPlacingHeight;
            }
            else
            {
                Destroy(currentTower);
                currentTower = null;
            }
        }

        public void OnEndDrag(PointerEventData data)
        {
            if (currentTower == null) return;
            if (Camera.main == null) throw new NullReferenceException();

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 1000.0f, groundMask) && Bootstrap.Instance.Player.Money >= settings.Cost && _canPlaceTowerAtCurrentPosition)
            {
                Debug.Log("Placed Tower.");
                currentTower.transform.position = hit.point + Vector3.up * yPlacingHeight;
                // currentTower.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                
                var view = currentTower.GetComponent<ITowerView>();
                view.EnableCollider();
                
                Bootstrap.Instance.Towers.Add(global.CreateTower(settings, view, new Vector2(currentTower.transform.position.x, currentTower.transform.position.z)));
                Bootstrap.Instance.Player.RemoveMoney(settings.Cost);
            }
            else
            {
                Debug.Log("Didn't place anything.");
                Destroy(currentTower);
                currentTower = null;
            }
        }
    }
}