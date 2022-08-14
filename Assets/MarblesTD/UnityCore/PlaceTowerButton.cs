using MarblesTD.Core.Towers;
using MarblesTD.UnityCore.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesTD.UnityCore
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
        }
        
        public void OnBeginDrag(PointerEventData data)
        {
            currentTower = Instantiate(towerPrefab);
        }

        public void OnDrag(PointerEventData data)
        {
            if (currentTower == null) return;
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000.0f,groundMask))
            {
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
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000.0f,groundMask))
            {
                Debug.Log("Placed Tower.");
                currentTower.transform.position = hit.point + Vector3.up * yPlacingHeight;
                // currentTower.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                
                var view = currentTower.GetComponent<ITowerView>();
                
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