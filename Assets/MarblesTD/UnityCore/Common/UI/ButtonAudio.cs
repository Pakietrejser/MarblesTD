using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        [SerializeField] public bool overrideButton = true;
        
        static readonly Color Pressed = new Color(0.58f, 0.58f, 0.58f);
        static readonly Color Highlighted = new Color(0.8f, 0.8f, 0.8f);
        static readonly Color Disabled = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        public bool IsActive { get; set; } = true;

        void Awake()
        {
            if (!overrideButton) return;
            var button = GetComponent<Button>();
            button.navigation = new Navigation(){mode = Navigation.Mode.None, wrapAround = false};
            button.colors = new ColorBlock
            {
                normalColor = new Color32(255, 255, 255, 255),
                highlightedColor = Highlighted,
                pressedColor = Pressed,
                selectedColor = Highlighted,
                disabledColor = Disabled,
                colorMultiplier = 1.0f,
                fadeDuration = 0.1f
            };
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsActive) return;
            SignalBus.FireStatic(new ButtonHoverSignal());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsActive) return;
            SignalBus.FireStatic(new ButtonClickSignal());
        }
    }
}