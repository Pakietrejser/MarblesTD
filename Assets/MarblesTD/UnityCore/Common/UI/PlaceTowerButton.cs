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
            _draggedTowerOutOfPanel = false;
        }

        public void OnDrag(PointerEventData data)
        {
            if (EventSystem.current.IsPointerOverGameObject() && !_draggedTowerOutOfPanel)
            {
                return;
            }
            
            if (!_draggedTowerOutOfPanel)
            {
                currentTower = Instantiate(towerPrefab);
                var view = currentTower.GetComponent<ITowerView>();
                view.Init(settings.Icon, settings.TowerType);
                view.DisableCollider();
                _draggedTowerOutOfPanel = true;
            }

            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(position, Vector2.down, 100);
            if (hit.collider != null)
            {
                _canPlaceTowerAtCurrentPosition = hit.collider.gameObject.name == "Map";
                var view = currentTower.GetComponent<ITowerView>();
                view.ShowAsPlaceable(_canPlaceTowerAtCurrentPosition);
                currentTower.transform.position = new Vector3(position.x, position.y, 0);
            }
            else
            {
                Debug.Log("early exit");
                Destroy(currentTower);
                currentTower = null;
            }
        }

        public void OnEndDrag(PointerEventData data)
        {
            if (currentTower == null) return;

            if (Bootstrap.Instance.Player.Money >= settings.Cost && _canPlaceTowerAtCurrentPosition)
            {
                var view = currentTower.GetComponent<ITowerView>();
                view.EnableCollider();
                Bootstrap.Instance.Towers.Add(global.CreateTower(settings, view, new Vector2(currentTower.transform.position.x, currentTower.transform.position.y)));
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