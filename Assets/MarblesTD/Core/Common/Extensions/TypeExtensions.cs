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
        
        public static string GetName(this ScenarioID type)
        {
            return Regex.Replace(type.ToString(), "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))", "$1 ");
        }
    }
}