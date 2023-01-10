using UnityEngine;

namespace MarblesTD.UnityCore.Common.Extensions
{
    public static class ProgressionHelper
    {
        static readonly Color[] ColorRange = 
        {
            new Color32(240, 174, 255, 255),
            new Color32(241, 179, 255, 255),
            new Color32(241, 183, 255, 255),
            new Color32(242, 187, 255, 255),
            new Color32(243, 191, 255, 255),
            new Color32(244, 195, 255, 255),
            new Color32(245, 200, 255, 255),
            new Color32(245, 204, 255, 255),
            new Color32(246, 208, 255, 255),
            new Color32(247, 213, 255, 255),
            new Color32(248, 217, 255, 255),
            new Color32(249, 221, 255, 255),
            new Color32(249, 225, 255, 255),
            new Color32(250, 229, 255, 255),
            new Color32(251, 234, 255, 255),
            new Color32(252, 238, 255, 255),
            new Color32(253, 242, 255, 255),
            new Color32(253, 247, 255, 255),
            new Color32(254, 251, 255, 255),
            new Color32(255, 255, 255, 255),
        };

        public static Color GetProgressionColor(int completedQuests, int allQuests)
        {
            return ColorRange[(int)(completedQuests * (ColorRange.Length / (float)allQuests))];
        }
    }
}