using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.CodeGen;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Types;
using TypeExtensions = UtilClasses.Extensions.Types.TypeExtensions;
namespace SupplyChain.Procs
{
    public class CallGenerator : IAppendable
    {
        public CallDef Def { get; }
        [JsonIgnore]
        public bool IsJson { get; set; }

        public string Namespace;
        private string _procName;
        private string _modeName;
        private string _typename;
        [JsonIgnore] private List<MappedProp> _props;
        private TypeLoader _loader;

        public CallGenerator( CallDef def)
        {
            Def = def;
            _loader = new TypeLoader(Globals.Map);
        }

        static CallGenerator()
        {
            
        }

        //private Assembly GetAssembly(string path) 
        //{
        //    var name = Path.GetFileNameWithoutExtension(path);
        //    var ass = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies()
        //        .FirstOrDefault(a => a.GetName().Name.EqualsIc2(name));
        //    return ass ?? Assembly.ReflectionOnlyLoadFrom(path);
        //}
        public void Init(ProcGenerator procGen, string basePath)
        {
            _props = new List<MappedProp>();
            _procName = procGen.Def.Name;
            if (Def.Name == null) Def.Name = _procName;
            if (Def.Typename.IsNullOrWhitespace())
            {
                var ps = Def.Only.Any() ? procGen.Parameters.Where(p => Def.Only.Contains(p.Name.Substring(1))) : procGen.Parameters;
                _modeName = procGen.Parameters.FirstOrDefault(p => p.Name.Substring(1).EqualsOic("mode"))?.Name.Substring(1);
                _props = ps.Select(p =>
                    new MappedProp(p.Name.Substring(1), char.ToUpper(p.Name[1]) + p.Name.Substring(2), "",
                        p.DbType.ToType().AsNullable(Def.NullableParameterNames.Contains(p.Name.Substring(1))), false, p.IsOut)).ToList();

                Namespace = Globals.Map.GetNamespace(Def.Returns.StripJsonTag());
                return;
            }
            var dllPath = Globals.Map.GetDllPath(Def.Typename);
            if (null == dllPath) throw new Exception($"Could not find a DllPath for type {Def.Typename}");
            dllPath = Path.Combine(basePath, dllPath);
            var ass = _loader.GetAssembly(dllPath);

            var typeName = Def.Typename ?? Def.Returns;
            if (typeName.StartsWithOic("json<"))
            {
                typeName = typeName.StripJsonTag();
                IsJson = true;
            }

            var t = ass.DefinedTypes.FirstOrDefault(a => a.Name.EqualsIc2(typeName));
            if (null == t) throw new Exception($"Could not find the type {Def.Typename}");
            _typename = t.FullName;
            Namespace = t.Namespace;
            if (IsJson) return;

            var sbError = new StringBuilder();
            foreach (var param in procGen.Parameters)
            {
                var name = Def.Mapping?.Maybe(param.Name) ?? Def.Mapping?.Maybe(param.Name.Substring(1)) ?? param.Name.Substring(1);
                var method = "";
                if (name.Contains("|"))
                {
                    var parts = name.SplitREE('|');
                    name = parts[0];
                    method = parts[1];
                }
                var i = t.GetPropFieldInfo(name);
                if (i == null && name.Contains('.'))
                {
                    var parts = name.SplitREE(".");
                    var realNameParts = new List<string>();
                    var st = t;
                    Type ValueType = null;
                    foreach (var part in parts)
                    {
                        var si = st.GetPropFieldInfo(part);
                        realNameParts.Add(si.Name);
                        ValueType = si.ValueType;
                        st = si.ValueType.GetTypeInfo();
                    }
                    i = new TypeExtensions.PropFieldInformation(realNameParts.Join("."), ValueType);

                }
                if (null != i)
                {
                    var n = param.Name.Substring(1);
                    _props.Add(new MappedProp(n, i.Name, method, i.ValueType.AsNullable(Def.NullableParameterNames.Contains(n)), true, false));
                    continue;
                }

                var suppliedParameter =
                    Def.Parameters.FirstOrDefault(p => p.EqualsIc2(param.Name) || p.EqualsIc2(param.Name.Substring(1)));
                if (suppliedParameter != null)
                {
                    _props.Add(new MappedProp(param.Name.Substring(1), suppliedParameter, "", param.DbType.ToType(), false, false));
                    continue;
                }

                var stringFilterParameter = t.GetPublicPropFieldInfo().FirstOrDefault(pfi =>
                    pfi.ValueType.Name.ContainsOic("StringFilter") && param.Name.Substring(1).StartsWithOic(pfi.Name));
                if (stringFilterParameter != null)
                {
                    if (param.Name.EndsWithIc2("Mode"))
                    {
                        _props.Add(new MappedProp(param.Name.Substring(1), $"{stringFilterParameter.Name}", ".Mode", stringFilterParameter.ValueType, true, false));
                        continue;
                    }
                    if (param.Name.EndsWithIc2("Value"))
                    {
                        _props.Add(new MappedProp(param.Name.Substring(1), $"{stringFilterParameter.Name}", ".Value", stringFilterParameter.ValueType, true, false));
                        continue;
                    }

                }
                sbError.AppendLine($"{procGen.Def.SpName}: Could not find a suitable property or field for parameter {param.Name}");

            }
            if (sbError.Length == 0) return;

            Console.Error.WriteLine(sbError.ToString());
        }

        public override string ToString()
        {
            var sb = new IndentingStringBuilder("\t");
            sb.AppendObject(this);
            return sb.ToString();
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            var separateProps = Def.HideAll
                ? new List<MappedProp>()
                : _props
                    .Where(p => !p.FromObject)
                    .Where(p => !Def.Hide.Contains(p.ParamName))
                    .Where(p => Def.Mode.IsNullOrEmpty() || !p.ParamName.EqualsOic("MODE"))
                    .Where(p => !Def.StaticStrings.ContainsKey(p.ParamName))
                    .Where(p => !Def.StaticInts.ContainsKey(p.ParamName))
                    .ToList();

            var callName = Def.Name.IsNullOrEmpty() ? _procName : Def.Name;
            var reg = new Regex("_[0-9]*$");
            callName = reg.Replace(callName, "");
            string typeName = null;
            if (_typename.IsNotNullOrEmpty())
            {
                typeName = _typename.Replace(Namespace + ".", "");
                if (Globals.ForbiddenNames.Contains(typeName))
                    typeName = _typename;
            }

            var rt = GetReturnType();
            if (IsJson)
            {
                sb.AppendLines($"public static I{rt} {callName}({typeName} obj)",
                    "{",
                    $"var proc = new Procs.{_procName}();",
                    $"return new {rt}(proc)"
                    ).Indent(() => sb.AppendLine($".Add(proc.Json.In(JsonConvert.SerializeObject(obj)));"))
                    .AppendLine("}");
            }
            else if (_typename != null)
            {
                sb.Append($"public static I{rt} {callName}(")
                    .Append(separateProps.Select(p => p.ToDeclaration()).Join(", "))
                    .MaybeAppend(separateProps.Any(), ", ")
                    .AppendLine($"{typeName} obj)")
                    .AppendLine("{")
                    .AppendLine($"var proc = new Procs.{_procName}();")
                    .AppendLine($"var ret = new {rt}(proc);")
                    .AppendLines(_props.Select(p => p.ToString()))
                    .AppendLine("return ret;")
                    .AppendLine("}");
            }
            else
            {
                sb.Append($"public static I{rt} {callName}(")
                    .Append(separateProps.Select(p => p.ToDeclaration(Def.Require.Any(r => r.EqualsIc2(p.ParamName)))).Join(", ")).AppendLine(" )")
                    .AppendLine("{")
                    .AppendLine($"var proc = new Procs.{_procName}();")
                    .AppendLine($"var ret = new {rt}(proc);")
                    .AppendLines(separateProps.Select(p => p.ToAssignment(Def.Hide.Contains(p.ParamName), Def.Require.Any(r => r.EqualsIc2(p.ParamName)))))
                    .AppendLines(Def.StaticStrings.Select(sv => $"ret.Add(proc.{sv.Key}.In(\"{sv.Value}\"));"))
                    .AppendLines(Def.StaticInts.Select(sv => $"ret.Add(proc.{sv.Key}.In({sv.Value}));"))
                    .AppendLines(_props.Where(p => p.IsOut).Where(p => Def.Hide.Contains(p.ParamName)).Select(p => p.ToAssignment(true, true)))
                    .Maybe(Def.Mode.IsNotNullOrEmpty(), s => s.AppendLine($"ret.Add(proc.{_modeName}.In(\"{Def.Mode}\"));"))
                    .AppendLine("return ret;")
                    .AppendLine("}");
            }
            return sb;
        }

        private string GetReturnType()
        {
            var realType = Def.Returns.StripAllGenerics();
            var rt = Globals.ForbiddenNames.Contains(realType) ? Def.Returns.Replace(realType, $"{Namespace}.{realType}") : Def.Returns;
            
            if (rt.IsNullOrEmpty() || rt.EqualsIc2("void")) return "DbCall";
            if (Types.ValueTypes.Contains(rt)) return $"ValDbCall<{rt}>";
            if (rt.EqualsIc2("json")) return "JsonDbCall";
            if (rt.StartsWithOic("json<") && rt.EndsWith(">")) return "JsonDbCall" + rt.Substring(4);
            return $"DbCall<{rt}>";
        }


    }

    internal static class Types
    {
        [JsonIgnore]
        public static HashSet<string> ValueTypes { get; }

        private static Dictionary<DbType, Type> _typeMap;
        static Types()
        {
            ValueTypes = new HashSet<string>(new[]
            {
                "bool", "byte", "char", "decimal", "double", "enum", "float", "int", "long", "sbyte", "short", "struct",
                "uint", "ulong", "ushort"
            }, StringComparer.OrdinalIgnoreCase);

            _typeMap = new Dictionary<DbType, Type>();
            DbType.Int16.As<short?>();
            DbType.Int32.As<int?>();
            DbType.String.As<string>();
            DbType.Guid.As<Guid>();
            DbType.Boolean.As<bool?>();
            DbType.DateTime.As<DateTime?>();
            DbType.Int64.As<long>();
            DbType.Byte.As<byte?>();
            DbType.Decimal.As<decimal>();
        }

        static void As<T>(this DbType t) => _typeMap[t] = typeof(T);
        public static Type ToType(this DbType t) => _typeMap.Maybe(t, () => throw new ArgumentException($"Unmapped DbType: {t}"));
    }

}
