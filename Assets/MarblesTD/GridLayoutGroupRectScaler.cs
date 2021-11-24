using System;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class GridLayoutGroupRectScaler : MonoBehaviour
    {
        [SerializeField] private bool runInUpdateLoop;
        
        private GridLayoutGroup gridLayoutGroup;
        private RectTransform rectTransform;

        private void Awake()
        {
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
            rectTransform = GetComponent<RectTransform>();
        }
        
        public void UpdateHeight()
        {
            float width = rectTransform.sizeDelta.x;

            int layoutElements = transform.childCount;
            var horizontalElements = (int) (width / gridLayoutGroup.cellSize.x);
                
            float height = gridLayoutGroup.cellSize.y * (int)((layoutElements + 1) / horizontalElements) +
                           gridLayoutGroup.spacing.y * (int)((layoutElements + 1) / horizontalElements - 1) +
                           gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom;

            rectTransform.sizeDelta = new Vector2(width, height);
        }

        private void Update()
        {
            if (runInUpdateLoop) UpdateHeight();
        }
    }
}