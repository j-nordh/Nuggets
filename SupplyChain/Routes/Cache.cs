using System;
using System.Linq;
using System.Reflection;
using UtilClasses;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Reflections;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Routes
{
    internal class Cache: IAppendable
    {
        private readonly CustomAttributeData _attribute;
        private readonly Method _method;
        //public static RouteDictCacher<GeoArea, int> Cache = new RouteDictCacher<GeoArea, int>(All(), h => h.Id, TimeSpan.FromMinutes(15), true);
        public Cache(CustomAttributeData attribute, Method method)
        {
            _attribute = attribute;
            _method = method;
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            _attribute.NamedArguments.ThrowIfNull(new ArgumentException("This attribute has no named arguments"));
            var name = _attribute.NamedArguments.GetValue<string>("Name")?? "Cache";
            var idType = _attribute.NamedArguments.GetValue<Type>("IdType") ?? typeof(int);
            var extractor = _attribute.NamedArguments.GetValue<string>("Extractor")?? "x => x.Id";
            var timeoutSeconds = _attribute.NamedArguments.GetIntValue("TimeoutSeconds") ?? 900;
            var minRefreshDelaySeconds = _attribute.NamedArguments.GetIntValue("MinRefreshDelaySeconds") ?? 30;
            var updateOnMiss =
                _attribute.NamedArguments.FirstOrDefault(a => a.MemberName.EqualsIc2("UpdateOnMiss")).TypedValue
                    .Value as bool? ?? true
                    ? "true"
                    : "false";

            var fullType = $"RouteDictCacher<{idType.CodeName()}, {_method.Returns.Name.SubstringAfter("<").Trim('<', '>')}>";

            sb.AppendLine($"public static {fullType} {name} = new {fullType}({_method.Name}(), {extractor}, TimeSpan.FromSeconds({timeoutSeconds}),TimeSpan.FromSeconds({minRefreshDelaySeconds}), {updateOnMiss});");
            return sb;
        }
    }
}