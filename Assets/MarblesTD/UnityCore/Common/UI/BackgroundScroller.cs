using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.UI
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] RawImage image;
        [SerializeField] float x = 0.01f;
        [SerializeField] float y = 0.01f;

        void Update()
        {
            image.uvRect = new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
        }
    }
}