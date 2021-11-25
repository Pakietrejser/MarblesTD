using MarblesTD.Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesTD.UnityCore
{
    [RequireComponent(typeof(Button))]
    public class PlaceMarbleButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private int marbleSpeed = 10;
        [SerializeField] private int marbleHealth;
        [Space]
        [SerializeField] private GameObject marblePrefab;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private float zDistance;
        [Space]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float yPlacingHeight;
        
        private Button marbleButton;
        private GameObject currentMarble;

        private void Awake()
        {
            marbleButton = GetComponent<Button>();
        }
        
        public void OnBeginDrag(PointerEventData data)
        {
            currentMarble = Instantiate(marblePrefab);
        }

        public void OnDrag(PointerEventData data)
        {
            if (currentMarble == null) return;
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000.0f,groundMask))
            {
                currentMarble.transform.position = hit.point + Vector3.up * yPlacingHeight;
            }
            else
            {
                Destroy(currentMarble);
                currentMarble = null;
            }
        }

        public void OnEndDrag(PointerEventData data)
        {
            if (currentMarble == null) return;
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000.0f,groundMask))
            {
                currentMarble.transform.position = hit.point + Vector3.up * yPlacingHeight;
                currentMarble.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                
                var view = currentMarble.GetComponent<IMarbleView>();
                Bootstrap.Instance.Marbles.Add(new Marble(view, new Vector2(currentMarble.transform.position.x, currentMarble.transform.position.z), marbleHealth, marbleSpeed));
            }
            else
            {
                Destroy(currentMarble);
                currentMarble = null;
            }
        }
    }
}