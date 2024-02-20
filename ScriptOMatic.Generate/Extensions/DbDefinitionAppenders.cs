using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SupplyChain.Dto;
using SupplyChain.Procs;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Objects;

namespace ScriptOMatic.Generate.Extensions
{
    public static class DbDefinitionAppenders
    {

        public static IndentingStringBuilder AppendProcDefs(this IndentingStringBuilder sb, PopulatedBundle b)
        {
            var def = b.GetDbDefinition();
            if(def!= null) Appender(def, sb);
            return sb;
        }
        public static ProcDef Add(this List<ProcDef> lst, SpInfo si)
        {
            if (!si.ShouldCreate())
                return null;
            var ret = new ProcDef(si.Name, si.CodeName);
            lst.Add(ret);
            return ret;
        }

        public static ProcDef Add(this List<ProcDef> lst, ReadWithCol rwc)
        {
            var ret = lst.Add((SpInfo)rwc);
            if (null != ret)
                ret.ColFields = new ColFields(rwc.Column, rwc.AllParameters.Select(p=>p.ToFieldDef()), rwc.ReturnsList);
            if (rwc.Number > 1)
                ret.SpName += rwc.Number;
            return ret;
        }

        public static ProcDef WithCall(this ProcDef proc, Func<CallDef, CallDef> callInitializer) => proc.Do(p => p.Calls.Add(callInitializer(new CallDef())));
        public static CallDef Returns(this CallDef call, string typeName) => call.Returns(typeName, false, false);
        public static CallDef Returns(this CallDef call, string typeName, bool json) => call.Returns(typeName, json, false);
        public static CallDef Void(this CallDef call) => call.Returns("void", false, false);
        public static CallDef ReturnsList(this CallDef call, string typeName, bool json) => call.Returns(typeName, json, true);

        public static CallDef AcceptsList(this CallDef call, string typename)
        {
            if(null == call.Parameters) call.Parameters= new List<string>();
            call.Parameters.Add($"List<{typename}> items");
            return call;
        }
        public static CallDef Returns(this CallDef call, string typeName, bool json, bool list)
        {
            if (list)
                typeName = $"List<{typeName}>";
            if (json)
                typeName = $"json<{typeName}>";
            call.Returns = typeName;
            return call;
        }
        public static CallDef WithNullable(this CallDef call, params string[] paramNames)
        {
            call.NullableParameterNames.AddRange(paramNames.Select(p=>p.TrimStart('@')));
            return call;
        }
        public static CallDef WithNullable(this CallDef call, IEnumerable<string> ps) => call.WithNullable(ps.ToArray());
        public static CallDef WithNullable(this CallDef call, IEnumerable<ParamSpec> ps) => call.WithNullable(ps.ToArray());
        public static CallDef WithNullable(this CallDef call, params ParamSpec[] ps) => call. WithNullable(ps.Select(p => p.Name));
        public static CallDef WithNullable(this CallDef call, IEnumerable<ColumnProperties> cs) => call.WithNullable(cs.Select(c=>c.Name));
        public static CallDef HidingId(this CallDef call, bool doHide = true) => doHide ? call.Hiding("Id") : call;
        public static CallDef Hiding(this CallDef call, string p)
        {
            if (call.Hide == null) call.Hide = new HashSet<string>();
            call.Hide.Add(p);
            return call;
        }
        public static CallDef Hiding(this CallDef call, IEnumerable<string> ps)
        {
            ps.ForEach(p => call.Hiding(p));
            return call;
        }
        public static CallDef Hiding(this CallDef call, IEnumerable<ColumnProperties> ps) => call.Hiding(ps.Select(p => p.Name));

        public static CallDef WithType(this CallDef call, string typename) => call.Do(c => c.Typename = typename);

        public static void Appender(ClassDef c, IndentingStringBuilder sb)
        {
            sb.AppendLine("{").Indent()
                .AppendLines(
                    $"\"ClassName\": \"{c.ClassName}\",",
                    "\"Procedures\": [")
                .AppendObjects(c.Procedures, Appender, ",", false)
                .Outdent()
                .AppendLine("]").Append("}");
        }
        public static void Appender(this IndentingStringBuilder sb, ProcDef p)
        {
            sb.AppendLine("{").Indent()
                .AppendLines($"\"SpName\": \"{p.SpName}\",", $"\"Name\": \"{p.Name}\",")
                .Maybe(p.ColFields != null, () => sb.AppendLine($"\"Colfields\": {JsonConvert.SerializeObject(p.ColFields)},"))
                .Append("\"Calls\" :[");
            if (p.Calls.Count > 1)
            {
                sb.Indent().AppendLine();
            }
            sb.AppendObjects(p.Calls, JsonConvert.SerializeObject, p.Calls.Count() > 1 ? ",\r\n" : ", ", false);
            if (p.Calls.Count > 1)
                sb.Outdent();
            sb.AppendLine("]")
                .Outdent()
                .Append("}");
        }

        public static IEnumerable<ColumnProperties> Keys(this IEnumerable<ColumnProperties> cs) => cs.Where(c => c.IsKey);
    }
}
