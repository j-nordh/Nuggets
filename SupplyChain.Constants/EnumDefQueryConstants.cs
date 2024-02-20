using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UtilClasses.CodeGeneration;
using UtilClasses.Extensions.Integers;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;
namespace SupplyChain.Constants
{
    public class EnumDefQueryEnumBuilder : QueryEnumBuilder
    {
        private readonly EnumDef _def;
        private readonly string _attributeColumn;

        public override string Name => _def.Name;
        protected override string Query => $"SELECT {_def.IdColumn} as Id, {_def.NameColumn} as Name{_attributeColumn} from {_def.Table};";
        protected override string Namespace => _env.Dto.Namespace;

        protected override string DbTable => _def.Table;

        protected override string LocalPath => Path.Combine(Name + ".cs");

        //protected override Func<IdNameAttr<bool>, string> Format => x => ((_withAttribute && x.Attribute) ? $"[{_def.AttributeName}]\r\n" : "") + base.Format(x);


        protected override Func<IDictionary<string, object>, EnumElement.Member> Format => o =>
            new EnumElement.Member() { Attributes = _def.Attributes.Select(a=>a.ToString(o)).ToList(), Id = o["Id"].AsInt(), Name = o["Name"].ToString().MakeIt().PascalCase() };
            
        

        public EnumDefQueryEnumBuilder(EnumDef def, CodeEnvironment env) : base(env)
        {
            _def = def;
            _attributeColumn = "";
            var ext = new ExtensionClassElement(def.Name);
            foreach (var attr in _def.Attributes)
            {
                _attributeColumn += $", {attr.Column}";
                switch (attr.Style)
                {
                    case AttributeStyle.PresentIfTrue:

                        ExtraElements.Add(new AttributeElement(attr.Name));
                        ext.Add(
                            sb => sb.AppendLine($"public static bool {attr.Name}(this {def.Name} x) => ")
                            .Indent()
                            .AppendLine($"typeof({def.Name}).GetMember(x.ToString())")
                            .Indent()
                            .AppendLines(".FirstOrDefault()",
                                $".HasCustomAttribute<{attr.Name}Attribute>();")
                                .Outdent(2))
                            .Using("System.Linq", "UtilClasses.Extensions.Reflections");
                        break;
                    case AttributeStyle.CastedValue:
                        ExtraElements.Add(new AttributeElement(attr.Name, attr.Type, "val"));
                        ext.Add(
                            sb => sb.AppendLine(
                                $"public static {attr.Type} {attr.Name}(this {def.Name} x) => ")
                                .Indent()
                                .AppendLine($"typeof({def.Name}).GetMember(x.ToString())")
                                .Indent()
                                .AppendLines(".FirstOrDefault()",
                                ".GetCustomAttributes(false)",
                                $".OfType<{attr.Name}Attribute>()",
                                ".FirstOrDefault()",
                                ".Val;").Outdent(2))
                                .Using("System.Linq", "UtilClasses.Extensions.Reflections");
                        break;
                }
            }
            if (ext.Methods.Any())
                ExtraElements.Add(ext);
        }
    }
}