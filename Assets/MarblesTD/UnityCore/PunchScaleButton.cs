using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore
{
    [RequireComponent(typeof(Button))]
    public class PunchScaleButton : MonoBehaviour
    {
        [SerializeField] Vector3 punch = Vector3.one;
        [SerializeField] float duration = .5f;
        [SerializeField] int vibrato = 1;
        [SerializeField] float elasticity = 1;
        
        Button button;
        bool isBeingPunched;
        
        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PunchScale);
        }

        void PunchScale()
        {
            if (isBeingPunched) return;
            isBeingPunched = true;

            button.transform
                .DOPunchScale(punch, duration, vibrato, elasticity)
                .OnComplete(() => isBeingPunched = false);
        }
    }
}