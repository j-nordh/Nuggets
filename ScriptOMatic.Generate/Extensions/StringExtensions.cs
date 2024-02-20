using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtilClasses.Extensions.Enumerables;

namespace ScriptOMatic.Generate.Extensions
{
    static class StringExtensions
    {
        public static string ToAlias(this string s) => $"[{s.Where(char.IsUpper).AsString()}]";
    }
}
