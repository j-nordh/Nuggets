using System;
using System.Collections.Generic;
using System.Linq;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Types;
using static UtilClasses.Extensions.Types.TypeExtensions;
namespace SupplyChain.Implement
{
    class EquotableImplementer
    {
        public List<PropFieldInformation> Members { get; }

        public EquotableImplementer(Type t)
        {
            T = t;
            Members= new List<PropFieldInformation>(T.GetPublicPropFieldInfo());
        }

        public Type T { get; }

        public override string ToString() => new IndentingStringBuilder("\t", 2)
            .AppendEquals(this)
            .AppendLine()
            .AppendObjEquals(this)
            .AppendLine()
            .AppendGetHashCode(this)
            .ToString();
    }

    static class ImplementerExtensions
    {
        public static IndentingStringBuilder
            AppendGetHashCode(this IndentingStringBuilder sb, EquotableImplementer ei) => sb
            .AutoIndentOnCurlyBraces()
            .AppendLine("public override int GetHashCode()")
            .AppendLine("{")
            .AppendLine("unchecked")
            .AppendLine("{")
            .AppendLine("int hashCode = 17;")
            .AppendLines(ei.Members.Select(m =>
                    $"hashCode = hashCode * 397 + {m.Name}" +
                    (m.ValueType.IsNullable() || m.ValueType.IsClass
                        ? "?.GetHashCode() ?? 0;"
                        : m.ValueType.IsValueType
                            ? ".GetHashCode();"
                            : null))
                .NotNull())
            .AppendLine("return hashCode;")
            .AppendLine("}",2);

        public static IndentingStringBuilder AppendEquals(this IndentingStringBuilder sb, EquotableImplementer ei) => sb
            .AutoIndentOnCurlyBraces()
            .AppendLine($"public bool Equals({ei.T.Name} other)")
            .AppendLine("{")
            .AppendLine("if (ReferenceEquals(null, other)) return false;")
            .AppendLine("if (ReferenceEquals(this, other)) return true;")
            .AppendLine("return")
            .Indent(x => x
                .Append(ei.Members.Select(m => $"{m.Name} == other.{m.Name}").Join(" && \r\n"))
                .AppendLine(";"));

        public static IndentingStringBuilder AppendObjEquals(this IndentingStringBuilder sb, EquotableImplementer ei) =>
            sb
                .AutoIndentOnCurlyBraces()
                .AppendLine("public override bool Equals(object obj")
                .AppendLine("}")
                .AppendLine("if (ReferenceEquals(null, obj)) return false;")
                .AppendLine("if (ReferenceEquals(this, obj)) return true;")
                .AppendLine($"var typed = obj as {ei.T.Name}")
                .AppendLine("return null != typed && Equals(typed);")
                .AppendLine("}");
    }
}
