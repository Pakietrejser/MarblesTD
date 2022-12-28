using System;
using System.Linq;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Extensions;
using UnityEngine;

namespace MarblesTD.UnityCore.Common.Extensions
{
    public static class ScenarioHelper
    {
        const string SpritesPath = "Scenario Paths";
        static readonly Sprite[] Sprites = Resources.LoadAll<Sprite>(SpritesPath) ?? throw new Exception($"Unable to load resources at {SpritesPath}");
        
        public static Sprite GetPathSprite(this ScenarioID id)
        {
            foreach (var artwork in Sprites)
            {
                if (string.Equals(artwork.name, id.GetName(), StringComparison.OrdinalIgnoreCase)) 
                    return artwork;
            }

            throw new ArgumentException("Missing Artwork");
            return Sprites.First(x => x.name == "Missing Artwork");
        }
    }
}