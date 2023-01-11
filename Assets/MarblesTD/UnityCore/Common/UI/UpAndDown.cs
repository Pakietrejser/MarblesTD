using DG.Tweening;
using UnityEngine;

namespace MarblesTD.UnityCore.Common.UI
{
    public class UpAndDown : MonoBehaviour
    {
        [SerializeField] float strength = .1f;
        [SerializeField] float duration = 1f;
        [SerializeField] Ease ease = Ease.InFlash;
        [SerializeField] bool goDownFirst = true;
        
        void Awake()
        {
            var pos = transform.position;
            if (goDownFirst)
            {
                pos.y -= strength * .75f;
            }
            else
            {
                pos.y += strength * .75f;
            }

            transform.localPosition = pos;
            transform.DOKill();
            transform.DOLocalMoveY(pos.y + strength, duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
        }
    }
}