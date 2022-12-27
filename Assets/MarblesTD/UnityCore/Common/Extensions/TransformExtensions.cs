using System;
using UnityEngine;

namespace MarblesTD.UnityCore.Common.Extensions
{
    public static class TransformExtensions
    {
        public static void Rotate2D(this Transform transform, Vector2 target)
        {
            var current = transform.position;
            float x = target.x - current.x;
            float y = target.y - current.y;
            var rotation = (float) (Math.Atan2(y, x) * 180 / Math.PI);
            transform.rotation = Quaternion.Euler(0, 0, rotation + 180);
        }
    }
}