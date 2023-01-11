using DG.Tweening;
using UnityEngine;

namespace MarblesTD.UnityCore.Common.UI
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] float duration = 1f;

        void Awake()
        {
            transform.DOKill();
            transform.DORotate(Vector3.forward * 360, duration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}