using MarblesTD.Core.Common.Signals;
using MarblesTD.Core.Common.Signals.List;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarblesTD.UnityCore.Common.UI
{
    public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            SignalBus.FireStatic(new ButtonHoverSignal());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SignalBus.FireStatic(new ButtonClickSignal());
        }
    }
}