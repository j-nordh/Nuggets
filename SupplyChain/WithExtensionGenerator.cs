using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.CodeGeneration;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Types;
using UtilClasses.Files;

namespace SupplyChain
{
    static class WithExtensionGeneratorExtensions
    {


        public static string ExtensionClassName(this Type t) => $"{t.Name}Extensions";
    }
    class WithExtensionGenerator
    {
        private readonly CodeEnvironment _env;
        private readonly Type _t;

        public WithExtensionGenerator(CodeEnvironment env, Type t)
        {
            _env = env;
            _t = t;
        }
        private ClassBuilder GetBuilder() =>
            new ClassBuilder() {AccessModifier = "public", IsStatic = true, Name = _t.ExtensionClassName()}
                .Inject(new WithExtensionInjector(_t));

        public string Generate()
        {
            return new IndentingStringBuilder("\t").AppendObjects(new[] {GetBuilder()}).ToString();
        }
        public  void GenerateFile()
        {
            var fb = new FileBuilder() {Namespace = _env.Dto.ExtensionNs}.Add(GetBuilder());
            var sb = new IndentingStringBuilder("\t").AppendObject(fb);
            FileSaver.SaveIfChanged(FileName, sb.ToString());
        }

        public string FileName => Path.Combine(_env.Dto.ExtensionsDir,$"{_t.ExtensionClassName()}.cs");
    }

    internal class WithExtensionInjector:IInjector
    {
        private readonly Type _t;

        public WithExtensionInjector(Type t)
        {
            _t = t;
        }

        public string Name => "WithExtensions";
        public IEnumerable<string> Using => new [] { _t.Namespace, "UtilClasses.Extensions.Objects" };
        public IEnumerable<string> Fields => new string[] { };
        public IEnumerable<string> ConstructorArgs => new string[] { };
        public IndentingStringBuilder Constructor(IndentingStringBuilder sb) => sb;

        public IndentingStringBuilder Methods(IndentingStringBuilder sb) => 
            sb.AppendObjects(_t.GetProperties(), Render);

        private void Render(IndentingStringBuilder sb, PropertyInfo pi)
        {
            if (!pi.CanWrite) return;
            var setter = $"obj.{pi.Name} = val";
            if (pi.PropertyType.IsGenericType && pi.PropertyType.Name.StartsWith("List"))
                setter = $"obj.{pi.Name}.Add(val)";

            var paramType = Globals.FixForbidden(pi.PropertyType.SaneName().StripAllGenerics());
            sb.Append($"public static {_t.Name} With{StringUtil.ToSingle(pi.Name)}(this {_t.Name} obj, {paramType} val) => obj.Do(() => {setter});");
        }

        public IndentingStringBuilder SubClasses(IndentingStringBuilder sb) => sb;

        public string Inherits => null;
        public string BaseConstructorArguments => "";
        public IEnumerable<string> Implements => new string[] { };
    }
}
