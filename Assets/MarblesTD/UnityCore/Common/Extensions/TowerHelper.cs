using System;
using MarblesTD.Core.Entities.Towers;
using MarblesTD.Towers;
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
        
        public static string GetTranslatedName(this Tower tower)
        {
            return tower switch
            {
                QuickFox _ => "Lisek",
                CannonBoar _ => "Dzik Machina",
                StarStag _ => "Gwiezdy Daniel",
                Mastiffteer _ => "Muszkieter",
                HalberdBear _ => "Strażmiś",
                Beehive _ => "Ul",
                ShadowPaw _ => "Łapka Cienia",
                MagicPot _ => "Kocioł",
                WebWeaver _ => "Sieciarz",
                _ => "Coś poszło nie tak"
            };
        }
        
        public static string GetTranslatedDescription(this Tower tower)
        {
            return tower switch
            {
                QuickFox _ => "Strzela przebijającymi strzałami.",
                CannonBoar _ => "Niszczy marble swoim potężnym działem.",
                StarStag _ => "Wspiera najbliższego sojusznika.",
                Mastiffteer _ => "Strzela na nieograniczony zasięg.",
                HalberdBear _ => "Używa halabardy by niszczyć najsilniejsze z marbli.",
                Beehive _ => "Generuje więcej funduszy dla ciebie.",
                ShadowPaw _ => "Jego ostrza mają krótki zasięg ale są zabójcze.",
                MagicPot _ => "Podpala wszystkie marble wokół niego.",
                WebWeaver _ => "Spowalania marble swoją siecią.",
                _ => "Coś poszło nie tak"
            };
        }

        public static Color GetColor(this Tower tower)
        {
            return tower.AnimalType switch
            {
                AnimalType.WildAnimal => new Color(0.62f, 0.89f, 0.51f),
                AnimalType.NobleAnimal => new Color(0.96f, 0.91f, 0.52f),
                AnimalType.NightAnimal => new Color(0.66f, 0.52f, 0.93f),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}