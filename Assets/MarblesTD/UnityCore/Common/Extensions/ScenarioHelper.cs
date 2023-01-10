using System;
using MarblesTD.Core.Common.Enums;
using MarblesTD.Core.Common.Extensions;
using UnityEngine;

namespace MarblesTD.UnityCore.Common.Extensions
{
    public static class ScenarioHelper
    {
        const string SpritesPath = "Scenario Paths";
        static readonly Sprite[] Sprites = Resources.LoadAll<Sprite>(SpritesPath) ?? throw new Exception($"Unable to load resources at {SpritesPath}");
        
        const string PrefabPath = "Scenarios";
        static readonly GameObject[] Prefabs = Resources.LoadAll<GameObject>(PrefabPath) ?? throw new Exception($"Unable to load resources at {PrefabPath}");
        
        public static Sprite GetPathSprite(this ScenarioID id)
        {
            foreach (var artwork in Sprites)
            {
                if (string.Equals(artwork.name, id.GetName(), StringComparison.OrdinalIgnoreCase)) 
                    return artwork;
            }

            throw new ArgumentException($"No prefab named {id.GetName()} in Resources/{SpritesPath}");
        }
        
        public static GameObject GetPrefab(this ScenarioID id)
        {
            foreach (var prefab in Prefabs)
            {
                if (string.Equals(prefab.name, id.GetName(), StringComparison.OrdinalIgnoreCase)) 
                    return prefab;
            }

            throw new ArgumentException($"No prefab named {id.GetName()} in Resources/{PrefabPath}");
        }
    }
}