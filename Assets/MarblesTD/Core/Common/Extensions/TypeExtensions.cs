using System;
using System.Text.RegularExpressions;

namespace MarblesTD.Core.Common.Extensions
{
    public static class TypeExtensions
    {
        public static string GetName(this Type type)
        {
            return Regex.Replace(type.Name, "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))", "$1 ");
        }
    }
}