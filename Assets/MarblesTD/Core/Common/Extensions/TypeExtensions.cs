using System;
using System.Text.RegularExpressions;
using MarblesTD.Core.Common.Enums;

namespace MarblesTD.Core.Common.Extensions
{
    public static class TypeExtensions
    {
        public static string GetName(this Type type)
        {
            return Regex.Replace(type.Name, "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))", "$1 ");
        }

        public static string GetTranslatedName(this ScenarioID type)
        {
            return type switch
            {
                ScenarioID.NULL => "null",
                ScenarioID.Ambush => "Zasadzka",
                ScenarioID.BranchingOut => "Rosnące Gałązki",
                ScenarioID.Garden => "Ogród",
                ScenarioID.HelloWorld => "Witaj Świecie",
                ScenarioID.Infinity => "Grand Prix",
                ScenarioID.Intersection => "Skrzyżowanie",
                ScenarioID.Labyrinth => "Labirynt",
                ScenarioID.Lake => "Staw",
                ScenarioID.LastStand => "Ostatnia Linia Obrony",
                ScenarioID.Loops => "Pętle",
                ScenarioID.LostGlasses => "Zagubione Bryle",
                ScenarioID.MagicAntlers => "Magiczne Rogi",
                ScenarioID.PayUp => "Inflacja",
                ScenarioID.Sandwich => "Kanapeczka",
                ScenarioID.Scribbles => "Bazgroły",
                ScenarioID.Snail => "Ślimak",
                ScenarioID.Snake => "Wąż",
                ScenarioID.SpiderLair => "Leże Pająka",
                ScenarioID.Spider => "Pająk",
                ScenarioID.TwinTowers => "Dwie Wieże",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
        
        public static string GetRawName(this ScenarioID type)
        {
            return Regex.Replace(type.ToString(), "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))", "$1 ");
        }
    }
}