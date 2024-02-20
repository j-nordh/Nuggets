using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Dto;
using UtilClasses;
using UtilClasses.CodeGeneration;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Reflections;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Routes
{
    internal enum Requirements
    {
        Vanguard,
        Liquid,
        NotificationEngine,
        ActionAgent,
        None
    }
    internal class Controller
    {
        private readonly List<Cache> _caches;
        private readonly string _routePrefix;
        private readonly RouteTypes _rt;
        private readonly HashSet<string> _requires;
        public Requirements Requires { get; }
        public bool IsCrud { get; set; }
        public string CrudTypeName { get; set; }
        private static HashSet<string> _declaringTypeBlacklist;

        static Controller()
        {
            _declaringTypeBlacklist = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ApiController", "object" };
        }

        private Controller(string name, List<string> requires, List<Cache> caches, Requirements req, string routePrefix, RouteTypes rt, List<Method> methods)
        {
            _requires = new HashSet<string>
            {
                "System.Diagnostics.Contracts",
                "UtilClasses.WebClient.Extensions",
                "System.Collections.Generic"
            };
            if(RouteTypes.MacsRoute == rt)
            {
                _requires.Add("MACS.Common");
            }
            _requires.UnionWith(requires.Select(n => n.RemoveAll(";").RemoveAll("\"")));
            _caches = caches;
            _routePrefix = routePrefix;
            _rt = rt;
            Name = name;
            Requires = req;
            Methods = methods;
            IsCrud = ComputeIsCrud();
        }

        private bool ComputeIsCrud()
        {
            var getRoute = Methods.FirstOrDefault(m => m.Name.EqualsOic("get") && m.Parameters.Count() == 1);
            if (null == getRoute) return false;
            var t = getRoute.Returns;
            // var requiredMethods = new List<(string name, int? parameters)> { ("post", null), ("get", 0), ("get", 1), ("update", null), ("delete", 1) };
            var requiredMethods = new List<MethodSpec> {
                new MethodSpec("post") { Returns = t.Name }.WithParamType(t.Name),
                new MethodSpec("get").NoParameters(),
                new MethodSpec("get").WithParamType("long"),
                new MethodSpec("put").WithParamType(t.Name),
                new MethodSpec("delete").WithParamType("long") };
            var res = requiredMethods.Select(rm => Methods.Any(rm.Matches)).ToList();
            if (!res.All(b => b)) return false;
            CrudTypeName = t.Name;
            return true;
        }

        public static Controller FromType(TypeInfo controller)
        {
            var caches = new List<Cache>();
            var controllerName = controller.Name.Substring(0, controller.Name.Length - 10);
            var methods = new List<Method>();
            var rt = controller.GetRouteType();
            var prefixAttr = controller.CustomAttributes.FirstOrDefault("RoutePrefixAttribute");
            var routePrefix = prefixAttr?.FirstConstructorArg() ?? controllerName;
            var controllerRoute = controller.CustomAttributes.FirstOrDefault("RouteAttribute")?.FirstConstructorArg();
            //if (rt == RouteTypes.ApiRoute && controllerRoute.IsNullOrEmpty())
            //    controllerRoute = controllerName;

            foreach (var method in controller.GetMethods().Where(m => m.IsPublic && !_declaringTypeBlacklist.Contains(m.DeclaringType.Name)))
            {
                var m = Method.FromMethodInfo(method, controllerName, rt, StringUtil.Combine('/', routePrefix, controllerRoute));
                if (null != m) methods.Add(m);
                var cacheAttribute =
                    method.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name.ContainsOic("GenerateCache"));
                if (cacheAttribute != null)
                {
                    caches.Add(new Cache(cacheAttribute, m));
                }
            }
            var types = methods.SelectMany(m => m.Types).NotNull().ToList();
            var moreTypes = types
                .Where(t => t.Contains("<"))
                .Select(t => t.SubstringBefore("<")).ToList();
            types.AddRange(moreTypes);

            var requires = types
                .Select(n => n.TrimEnd('?'))
                .Where(Globals.Map.Contains)
                .Select(n => Globals.Map.GetNamespace(n))
                .ToList();
            requires.AddRange(controller.CustomAttributes
                .Where(a => a.AttributeType.Name.ContainsOic("RequiresAttr"))
                .Select(a => a.ConstructorArguments.First().ToString()));

            requires.AddRange(types.Where(t => t.Contains('<'))
                .Select(t => t.SubstringAfter("<").Replace("<", ",").Replace(">", ","))
                .Where(t => t.IsNotNullOrEmpty())
                .SelectMany(t => t.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(t => t.IsNotNullOrEmpty() && Globals.Map.Contains(t))
                .Select(t => Globals.Map.GetNamespace(t)));

            if (caches.Any()) requires.Add("System");

            requires = requires.Distinct().ToList();

            var req = Requirements.Vanguard;
            if (controller.CustomAttributes.Any(a => a.AttributeType.Name.StartsWithOic("Liquid")))
                req = Requirements.Liquid;
            if (controller.CustomAttributes.Any(a => a.AttributeType.Name.StartsWithOic("RequiresActionAgent")))
                req = Requirements.ActionAgent;
            if (controller.CustomAttributes.Any(a => a.AttributeType.Name.StartsWithOic("NotificationEngine")))
                req = Requirements.NotificationEngine;

            var c = new Controller(controllerName, requires, caches, req, routePrefix, rt, methods);

            return c.Methods.Any() ? c : null;
        }

        public static Controller FromVerbDescriptors(KeyValuePair<string, List<VerbDescriptor>> kvp) =>
            FromVerbDescriptors(kvp.Key, kvp.Value);
        public static Controller FromVerbDescriptors(string route, List<VerbDescriptor> vds)
        {
            var controllerName = route.Trim('/');
            var methods = vds.Select(vd => Method.FromVerbDescriptor(vd, controllerName)).ToList();
            var types = methods.SelectMany(m => m.Types).NotNull().ToList();
            var moreTypes = types
                .Where(t => t.Contains("<"))
                .Select(t => t.SubstringBefore("<")).ToList();
            types.AddRange(moreTypes);
            var requires = types
                .Select(n => n.TrimEnd('?'))
                .Where(Globals.Map.Contains)
                .Select(n => Globals.Map.GetNamespace(n))
                .ToList();
            return new Controller(controllerName, requires, new List<Cache>(), Requirements.None, "",
                RouteTypes.MacsRoute, methods);
        }


        private class MethodSpec
        {
            public string Name { get; set; }
            public string Returns { get; set; }
            public List<ParamSpec> Parameters { get; set; }
            public MethodSpec()
            {

            }
            public MethodSpec(string name)
            {
                Name = name;
            }
            public MethodSpec WithParam(string name, string type)
            {
                Parameters ??= new List<ParamSpec>();
                Parameters.Add(new ParamSpec() { Name = name, Type = type });
                return this;
            }
            public MethodSpec WithParamType(string type) => WithParam(null, type);
            public MethodSpec NoParameters() => this.Do(m => m.Parameters = new List<ParamSpec>());

            public bool Matches(Method m)
            {
                if (!Name.IsNullOrEqualsOic(m.Name)) return false;
                if (!Returns.IsNullOrEqualsOic(m.Returns?.Name)) return false;
                if (null == Parameters) return true;
                if (m.Parameters.Count() != Parameters.Count) return false;
                return Parameters.All(p => m.Parameters.Any(mp => p.Matches(mp)));
            }
        }

        private class ParamSpec
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public bool Matches(Parameter p) => Name.IsNullOrEquals(p.Name) && Type.IsNullOrEquals(p.Type);
        }

        public string Name { get; }
        public List<Method> Methods { get; }

        public override string ToString()
        {
            var fb = new FileBuilder()
            {
                Namespace = Namespace,
                HeaderSteps = new[] { "Make the appropriate changes in the server application", "Compile the changes", "Run SupplyChain again." }
            }.Add(new ClassBuilder()
            {
                Name = Name,                
                AccessModifier = "public",
                IsStatic = true,                
            }
                .Inject(new ControllerInjector(_requires,_caches, Methods)));
            return fb.ToString();
        }

        private class ControllerInjector : IInjector
        {
            private readonly List<Cache> _caches;
            readonly List<string> _requires;
            readonly List<Method> _methods;

            public ControllerInjector(IEnumerable<string> requires, List<Cache> caches, List<Method> methods)
            {
                _methods = methods;
                _requires = requires as List<string> ?? requires?.ToList()?? new List<string>();
                _caches = caches;
            }

            public string Name => "Controller";

            public IEnumerable<string> Using => _requires.ToArray();

            public IEnumerable<string> Fields => new string[] {};

            public IEnumerable<string> ConstructorArgs => new string[] { };

            public string Inherits => null;

            public IEnumerable<string> Implements => null;

            public string BaseConstructorArguments => null;

            public IndentingStringBuilder Constructor(IndentingStringBuilder sb) => sb;

            public IndentingStringBuilder Methods(IndentingStringBuilder sb) => _methods.AppendAll(sb);

            public IndentingStringBuilder SubClasses(IndentingStringBuilder sb) => sb
                .Maybe(_caches.Any(), () =>
                    sb.AppendLine("public static class Cache")
                    .AppendLine("{")
                    .AppendObjects(_caches)
                    .AppendLine("}"));
        }

        public string Namespace =>
            _rt switch
            {
                RouteTypes.RecsRoute => "Recs.Routes",
                RouteTypes.MacsRoute => "MACS.Routes.Trion",
                _ => throw new ArgumentOutOfRangeException()
            };
    }


    static class Extensions
    {

        public static RouteTypes GetRouteType(this TypeInfo c)
        {
            if (null == c) throw new ArgumentNullException();
            var fn = c.FullName;
            if (fn.IsNullOrEmpty()) throw new ArgumentException("The controller does not seem to have a name?!?!?!");
            if (fn.ContainsOic("recs")) return RouteTypes.RecsRoute;
            throw new ArgumentException("I have no idea who that is....");
        }
    }
}