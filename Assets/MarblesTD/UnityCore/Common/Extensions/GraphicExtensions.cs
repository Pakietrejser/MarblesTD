using UnityEngine.UI;

namespace MarblesTD.UnityCore.Common.Extensions
{
    public static class GraphicExtensions
    {
        public static T ChangeAlpha<T>(this T g, float alpha) where T : Graphic
        {
            var color = g.color;
            color.a = alpha;
            g.color = color;
            return g;
        }
    }
}