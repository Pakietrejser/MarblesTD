using MarblesTD.Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesTD.Core
{
    [RequireComponent(typeof(Button))]
    public class PlaceTowerButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float range = 5f;
        [SerializeField] private float attackSpeed = 1f;
        [Space]
        [SerializeField] private GameObject towerPrefab;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private float zDistance;
        [Space]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float yPlacingHeight;
        
        private Button towerButton;
        
        private GameObject currentTower;

        private void Awake()
        {
            towerButton = GetComponent<Button>();
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
                Bootstrap.Instance.Towers.Add(new Tower(view, new Vector2(currentTower.transform.position.x, currentTower.transform.position.z), attackSpeed, range));
            }
            else
            {
                Destroy(currentTower);
                currentTower = null;
            }
        }
    }
}