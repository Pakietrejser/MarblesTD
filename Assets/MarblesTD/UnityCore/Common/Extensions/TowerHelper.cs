using System;
using MarblesTD.Core.Entities.Towers;
using UnityEngine;

namespace MarblesTD.UnityCore.Common.Extensions
{
    public static class TowerHelper
    {
        const string SpritesPath = "Tower Icons";
        static readonly Sprite[] Sprites = Resources.LoadAll<Sprite>(SpritesPath) ?? throw new Exception($"Unable to load resources at {SpritesPath}");
        
        const string PrefabPath = "Towers";
        static readonly GameObject[] Prefabs = Resources.LoadAll<GameObject>(PrefabPath) ?? throw new Exception($"Unable to load resources at {PrefabPath}");
        
        public static Sprite GetIcon(this Tower tower)
        {
            foreach (var artwork in Sprites)
            {
                if (string.Equals(artwork.name, tower.RawName, StringComparison.OrdinalIgnoreCase)) 
                    return artwork;
            }

            throw new ArgumentException($"No prefab named {tower.RawName} in Resources/{SpritesPath}");
        }
        
        public static GameObject GetPrefab(this Tower tower)
        {
            foreach (var prefab in Prefabs)
            {
                if (string.Equals(prefab.name, tower.RawName, StringComparison.OrdinalIgnoreCase)) 
                    return prefab;
            }

            throw new ArgumentException($"No prefab named {tower.RawName} in Resources/{PrefabPath}");
        }
    }
}