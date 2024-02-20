using System;
using System.Text.RegularExpressions;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Types;

namespace SupplyChain.Procs
{
    internal class MappedProp
    {
        public string ParamName { get; }
        private readonly string _varName;
        private readonly string _method;
        private readonly Type _t;
        public bool FromObject { get; }
        public bool IsOut { get; }
        private readonly TypeOfType _tot;

        public MappedProp(string paramName, string varName, string method, Type t, bool fromObject, bool isOut)
        {
            ParamName = paramName;
            _varName = varName;
            _method = method;
            _t = t;
            FromObject = fromObject;
            IsOut = isOut;
            if (t.IsNullable()) _tot = TypeOfType.Nullable;
            else if (t.IsValueType|| Types.ValueTypes.Contains(t.Name.ToLower())) _tot = TypeOfType.Value;
            else if (t.IsEnum) _tot = TypeOfType.Enum;
            else if (t.IsClass) _tot = TypeOfType.Reference;
            else throw new ArgumentOutOfRangeException();
        }

        public enum TypeOfType
        {
            Value,
            Reference,
            Enum,
            Nullable
        }
        public override string ToString()
        {
            if (!FromObject)
            {
                return $"ret.Add(proc.{CodeName}.In({_varName}{_method}));";
            }
            switch (_tot)
            {
                case TypeOfType.Value:
                    return $"ret.Add(proc.{CodeName}.In(obj.{_varName}{_method}));";
                case TypeOfType.Reference:
                    return $"if(null!=obj.{_varName}) ret.Add(proc.{CodeName}.In(obj.{_varName}{_method}));";
                case TypeOfType.Enum:
                    return $"ret.Add(proc.{CodeName}.In((int)obj.{_varName}{_method}));";
                case TypeOfType.Nullable:
                    return $"if(obj.{_varName}.HasValue) ret.Add(proc.{CodeName}.In(obj.{_varName}.Value{_method}));";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string CodeName
        {
            get
            {
                var name = ParamName;
                name = char.ToUpper(name[0]) + name.Substring(1);
                if (name.EqualsOic("Name")) name = "NameParam";
                return name;
            }
        }

        public string ToDeclaration(bool ignoreNullability =false)
        {
            string t = _t.ToString();
            if (_t.IsNullable())
            {
                t = _t.GenericTypeArguments[0] + "?";
            }
            t = t.Replace("System.Int32", "int")
                .Replace("System.String", "string")
                .Replace("System.Int16", "short")
                .Replace("System.Boolean", "bool")
                .Replace("System.Int64", "long");
            if (ignoreNullability) t= t.Replace("?", "");
            return t + " " + Char.ToLower(ParamName[0]) + ParamName.Substring(1);
        }

        public string ToAssignment(bool isHidden, bool ignoreNullability = false)
        {
            var paramName = Char.ToLower(ParamName[0]) + ParamName.Substring(1);
            return isHidden && IsOut
                ? $"ret.Add(proc.{CodeName}.Out());"
                : _t.IsNullable() && !ignoreNullability
                    ? $"if(null!={paramName}) ret.Add(proc.{CodeName}.In({paramName}.Value));"
                    : $"ret.Add(proc.{CodeName}.In({paramName}));";
        }
    }
}