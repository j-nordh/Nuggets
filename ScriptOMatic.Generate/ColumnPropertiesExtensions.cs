using System;
using System.Collections.Generic;
using System.Linq;
using SupplyChain.Dto;

namespace ScriptOMatic.Generate
{
    public static class ColumnPropertiesExtensions
    {
        public static string ToCode(this ColumnProperties.Modes m)
        {
            switch (m)
            {
                case ColumnProperties.Modes.Equals:
                    return "@=x";
                case ColumnProperties.Modes.FromTo:
                    return "@<x<@";
                case ColumnProperties.Modes.Omit:
                    return "";
                case ColumnProperties.Modes.Like:
                    return "\u2248";
                case ColumnProperties.Modes.StringMatch:
                    return "str";
                default:
                    throw new ArgumentOutOfRangeException(nameof(m), m, null);
            }
        }

        public static bool AnyTypeContains(this IEnumerable<ColumnProperties> cols, string s) =>
            cols.Any(c => c.Type.Contains(s));
    }
}