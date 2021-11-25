using MarblesTD.Towers;
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
        [SerializeField] private RectTransform canvas;
        [SerializeField] private float zDistance;
        [Space]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float yPlacingHeight;
        
        private Button towerButton;
        
        private GameObject currentTower;
        GlobalTowerSettings settings;

        private void Awake()
        {
            towerButton = GetComponent<Button>();
        }

        public void Init(GlobalTowerSettings globalTowerSettings)
        {
            settings = globalTowerSettings;
            var x = globalTowerSettings.QfSettings;

            towerImage.sprite = x.towerIcon;
            costText.text = $"${x.towerCost}";
            towerSetColor.color = globalTowerSettings.Get(x.towerSet).color;

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
                currentTower.transform.position = hit.point + Vector3.up * yPlacingHeight;
                currentTower.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                
                var view = currentTower.GetComponent<ITowerView>();
                
                Bootstrap.Instance.Towers.Add(settings.Create<QuickFox>(view, new Vector2(currentTower.transform.position.x, currentTower.transform.position.z)));
                Bootstrap.Instance.Player.RemoveMoney(settings.QfSettings.towerCost);
            }
            else
            {
                Destroy(currentTower);
                currentTower = null;
            }
        }
    }
}