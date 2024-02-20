using SupplyChain.Injectors;
using System.Collections.Generic;
using System.Linq;
using UtilClasses;
using UtilClasses.CodeGeneration;

namespace SupplyChain.Routes
{
    class AccessorGenerator
    {
        private FileBuilder _fb;
        private ClassBuilder _cb;
        private RouteExecutorElement _ree;
        private CrudCallersInjector _cci;
        public string Namespace { get=> _fb.Namespace; set =>_fb.Namespace=value; }
        public string Prefix
        {
            get => _injector.Prefix;
            set
            {
                _injector.Prefix = value;
                _ree.Prefix = value;
                _cb.Name = $"{_injector.Prefix}Client";

            }
        }
        
        public string FilePath { get; set; }
        public List<Controller> Controllers
        {
            get => _injector.Controllers; set
            {
                _injector.Controllers = value;
                _cci.Callers = value.Where(c=>c.IsCrud).Select(c=>c.Name).ToList();
            }
        }

        private AccessorElement _injector;

        public AccessorGenerator()
        {
            _fb = new FileBuilder()
                .Add(_cb = new ClassBuilder() { AccessModifier = "public" }
                    .Inject(_injector = new AccessorElement()))
                    //.Inject(_cci = new CrudCallersInjector()))
                .Add(_ree = new RouteExecutorElement());
        }

        private class AccessorElement : IInjector
        {
            public AccessorElement()
            {
             
            }
            public string Prefix { get; set; }
            public string Name => "Controllers";

            List<string> _using = new List<string>() { "System.Collections.Generic", "System.Threading.Tasks", "UtilClasses.WebClient" };
            public IEnumerable<string> Using => _using;

            public void AddUsing(params string[] args) => _using.AddRange(args);
            public IEnumerable<string> Fields => null;

            public IEnumerable<string> ConstructorArgs => new []{"string baseAddress"};

            public string Inherits => "ClientBase";
            public IEnumerable<string> Implements => new[] { "IRouteExecutor" };

            public string BaseConstructorArguments => "baseAddress";

            internal List<Controller> Controllers { get; set; }

            public IndentingStringBuilder Constructor(IndentingStringBuilder sb) => sb.AppendObjects(Controllers, ControllerInit);


            public IndentingStringBuilder Methods(IndentingStringBuilder sb) => sb.AppendLines(
                $"async Task<T> IRouteExecutor.Execute<T>({Prefix}Route<T> route) => await Execute(route);",
                $"async Task IRouteExecutor.Execute({Prefix}Route route) => await Execute(route);")
                .AppendObjects(Controllers, ControllerProp);

            public IndentingStringBuilder SubClasses(IndentingStringBuilder sb) => sb
                .AppendObjects(Controllers, AppendControllerDef);


            private string ControllerProp(Controller c) => $"public {c.Name}Caller {c.Name} {{get;}}";
            private string ControllerInit(Controller c) => $"{c.Name} = new {c.Name}Caller(this);";
            private void AppendControllerDef(IndentingStringBuilder sb, Controller c)
            {
                var crud = c.IsCrud ? $" : ICrudCaller<{c.CrudTypeName}>" : "";
                sb.AppendLines($"public class {c.Name}Caller{crud}",
                    "{",
                    "private readonly IRouteExecutor _re;",
                    $"public {c.Name}Caller(IRouteExecutor re)",
                    "{",
                    "_re = re;",
                    "}",
                    "")
                    .AppendLines(c.Methods.Select(m => FormatMethod(m, c)))
                    .AppendLine("}");
            }
            private string FormatMethod(Method m, Controller c)
            {
                var returnType = "Task";
                if (m.Returns != null)
                    returnType = $"Task<{m.Returns}>";

                return $"public {returnType} {m.Name}({m.ParameterDeclaration}) => _re.Execute(Routes.{c.Name}.{m.Name}({m.FormattedParameterNames}));";
            }


           
        }
        public override string ToString()
        {
            return _fb.ToString();
            //var sb = new IndentingStringBuilder("\t")
            //    .AutoIndentOnCurlyBraces()
            //    .AppendLines(Requires.AsSorted().Select(n => $"using {n};"))
            //    .OperatingOn(Controllers)
            //    .AppendLine()
            //    .AppendLines(
            //        @"//                                                               ____            ",
            //        @"// This file has been generated using SupplyChain.              /\' .\    _____  ",
            //        @"// Please do not modify it directly, instead:                  /: \___\  / .  /\ ",
            //        @"// * Make the appropriate changes in the server application,   \' / . / /____/..\",
            //        @"// * Compile the changes                                        \/___/  \'  '\  /",
            //        @"// * Run SupplyChain again.                                              \'__'\/ ",
            //        $"namespace {Namespace}",
            //        "{",
            //        $"public class {Prefix}Client : ClientBase, IRouteExecutor",
            //        "{",
            //        $"public {Prefix}Client(string baseAddress) : base(baseAddress)",
            //        "{")
            //    .On<Controller>(ControllerInit)
            //    .AppendLines(
            //        "}",
            //        $"async Task<T> IRouteExecutor.Execute<T>({Prefix}Route<T> route) => await Execute(route);",
            //        $"async Task IRouteExecutor.Execute({Prefix}Route route) => await Execute(route);")
            //    .On<Controller>(ControllerProp)
            //    .AppendLine("}")
            //    .ForEach<Controller>(AppendControllerDef)
            //    .AppendLines(
            //        "public interface IRouteExecutor",
            //        "{",
            //        $"Task<T> Execute<T>({Prefix}Route<T> route);",
            //        $"Task Execute({Prefix}Route route);",
            //        "}",
            //        "}"
            //    );
            //return sb.ToString();
        }

        private class RouteExecutorElement : ICodeElement
        {
            public string Name => "IRouteExecutor";

            public IEnumerable<string> Requires => null;

            public string Prefix { get; set; }

            public void AppendTo(IndentingStringBuilder sb)
            {
                sb.AppendLines(
                "public interface IRouteExecutor",
                "{",
                $"Task<T> Execute<T>({Prefix}Route<T> route);",
                $"Task Execute({Prefix}Route route);",
                "}");
            }
        }

        public AccessorGenerator Requiring(params string[] s)
        {
            _injector.AddUsing(s);
            return this;
        }

    }

}
