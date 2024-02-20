using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Dto;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Reflections;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Routes
{
    internal class Method : IAppendable
    {
        private readonly RouteTypes _routeType;
        public TypeDescriptor Returns { get; }
        public string ControllerName { get; }
        public bool IsAnonymous { get; }
        public string HttpMethod { get; }
        public TypeDescriptor Body { get; }
        public List<string> Headers { get; }

        private Method(string name, string returns, string controllerName, IEnumerable<Parameter> parameters, IEnumerable<string> headers,
            bool isAnonymous, string httpMethod, string route, RouteTypes rt)
        {
            _routeType = rt;
            Returns = new TypeDescriptor(Globals.FixForbidden(returns), "");
            ControllerName = controllerName;
            IsAnonymous = isAnonymous;
            HttpMethod = httpMethod;
            Name = name;
            Parameters = parameters.ToList();
            Headers = headers.ToList();
            Route = route;
        }
        private Method(string name, TypeDescriptor returns, TypeDescriptor body, string controllerName, IEnumerable<Parameter> parameters, IEnumerable<string> headers,
            bool isAnonymous, string httpMethod, string route, RouteTypes rt)
        {
            if (returns.IsSet())
                returns.Name = Globals.FixForbidden(returns.Name);
            if (body.IsSet())
                body.Name = Globals.FixForbidden(body.Name);
            _routeType = rt;
            Returns = returns;
            Body = body;
            ControllerName = controllerName;
            IsAnonymous = isAnonymous;
            HttpMethod = httpMethod;
            Name = name;
            var ps = parameters.ToList();
            if (body.IsSet())
                ps.Insert(0, new Parameter("body", body.Name, Parameter.ParameterTypes.Body, false));
            Parameters = ps.ToList();


            Headers = headers.ToList();
            Route = route;
        }

        public static Method FromVerbDescriptor(VerbDescriptor vd, string controllerName) => new Method(vd.Name,
            vd.Returns,
            vd.FromBody,
            controllerName,
            vd.Parameters.Select(p => new Parameter(Globals.FixForbidden(p.Name.MakeIt().CamelCase()), p.Type, Parameter.ParameterTypes.Query, p.Optional)),
            new string[] { },
            true,
            vd.Verb,
            vd.Route,
            RouteTypes.MacsRoute);
        public static Method FromMethodInfo(MethodInfo method, string controllerName, RouteTypes rt, string routePrefix)
        {

            var rtExtractors = new List<Func<MethodInfo, string>>
            {
                mi => mi.CustomAttributes.FirstOrDefaultConstructorArg<string>("Returns"),
                mi =>
                {
                    var t = mi.ReturnType;
                    if ((bool) t?.Name.StartsWith("Task"))
                        t = t.IsGenericType ? t.GenericTypeArguments.First() : t;
                    if (t?.Name.StartsWithOic("IResult") ?? false)
                        t = t.GenericTypeArguments.First();
                    return t.CodeName(controllerName);
                },
                mi => mi.ReturnType.CodeName(controllerName)
            };

            var returnType = rtExtractors.Select(ex => ex(method)).FirstOrDefault(n => n != null);

            var verbs = new[] { "POST", "PUT", "DELETE" };
            var methodString = verbs.FirstOrDefault(v => method.HasAttribute($"Http{v}")) ??
                                  verbs.FirstOrDefault(v => method.Name.StartsWithOic(v))?.ToUpper();


            var methodRoute = method.Name;

            string routeAttribute = method.CustomAttributes.FirstOrDefault("RouteAttribute")?.ConstructorArguments?
                .GetFirstOrDefault<string>();
            if (routeAttribute.IsNotNullOrEmpty())
            {
                methodRoute = routeAttribute;
                routePrefix = routePrefix.SubstringBefore("/") + "/";
            }


            methodRoute = StringUtil.Combine('/', routePrefix, methodRoute);
            var parameters = method.GetParameters()
                .Select(pi => GetParameter(pi, controllerName, methodRoute));

            var headers = method.CustomAttributes.WhereTypeNameContains("RequiresHeader")
                .Select(a =>
                    a.ConstructorArguments.First().Value.ToString())
                .Where(n => n.IsNotNullOrEmpty());
            return new Method(method.Name, returnType, controllerName, parameters, headers,
                method.HasAttribute("AllowAnonymous"), methodString, methodRoute, rt);
        }

        private static Parameter.ParameterTypes GetParameterType(ParameterInfo pi, string route)
        {
            if (pi.HasAttribute("FromBody")) return Parameter.ParameterTypes.Body;
            if (pi.HasAttribute("FromHeader")) return Parameter.ParameterTypes.Header;
            if (route.ContainsOic($"{{{pi.Name}}}")) return Parameter.ParameterTypes.Route;
            return Parameter.ParameterTypes.Query;
        }

        private static Parameter GetParameter(ParameterInfo pi, string controllerName, string route)
        {
            var t = GetParameterType(pi, route);
            var name = pi.CustomAttributes
                           .WhereTypeNameContains("FromHeader")
                           .FirstOrDefault()
                           ?.NamedArguments?
                           .FirstOrDefault(a => a.MemberName.EqualsIc2("Name"))
                           .TypedValue.Value.ToString()
                       ?? pi.Name;
            return new Parameter(name.MakeIt().StartWithLowerCase, pi.ParameterType.CodeName(controllerName), t, pi.IsOptional).WithRealName(name);
        }

        public string Name { get; }

        public string Route { get; }

        public IEnumerable<string> Types
        {
            get
            {
                List<string> Deconstruct(string s)
                {
                    if (s.IsNullOrAny("", "void")) return new List<string>();
                    var lst = new List<string>() { s };
                    if (!s.Contains("<")) return lst;
                    lst.Add(s.SubstringBefore("<"));
                    lst.AddRange(Deconstruct(s.SubstringAfter("<").SubstringBeforeLast(">")));
                    return lst;
                }
                var ret = Returns.IsSet() ? Deconstruct(Returns.Name) : new List<string>();
                ret.AddRange(Parameters.SelectMany(p => Deconstruct(p.Type)));

                return ret;
            }
        }

        public IEnumerable<Parameter> Parameters { get; }


        public Method CloneAndRename(string name)
        {
            return new Method(name, Returns, Body, ControllerName, Parameters, Headers, IsAnonymous, HttpMethod, Route, _routeType);
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            var r = $"\"{Route}\"";
            var routeParameters = Parameters.Where(p => p.ParameterType == Parameter.ParameterTypes.Route).ToList();
            if (routeParameters.Any())
            {
                r = routeParameters.Aggregate("$" + r, (current, p) => current.EnsureCase("{" + p.Name + "}"));
            }
            if (!Parameters.Any() && (HttpMethod == null || HttpMethod.EqualsOic("GET")) && Route.EndsWithIc2("/GET/"))
            {
                r = r.ReplaceOic("/GET/", "/");
            }
            var routeType = Returns.IsSet() ?$"{_routeType}<{Returns.Name}>": _routeType.ToString() ;
            sb
                .AppendLine("[Pure]")
                .Append($"public static {routeType} {Name}(")
                .Append(ParameterDeclaration)
                .AppendLine(") => ")
                .Indent(x => x
                    .AppendLine($"new {routeType}({r})")
                    .Indent(y => y
                        .AppendObjects(Parameters.Where(p => p.ParameterType != Parameter.ParameterTypes.Route), false)
                        .AppendLines(Headers.Select(h => $".WithHeader(\"{h}\", {h})"))
                        .MaybeAppendLine(HttpMethod.IsNotNullOrEmpty() && !HttpMethod.EqualsIc2("GET"),
                            $".SetMethod(\"{HttpMethod}\")")
                        .MaybeAppendLine(IsAnonymous, ".Anonymous()")
                        .PrevLine()
                        .AppendLine(";")));
            return sb;
        }
        public string ParameterDeclaration =>
            Parameters.Select(p => $"{p.Type} {p.Name}" + (p.Optional ? " = null" : ""))
                .Union(Headers.Select(h => $"string {h}")).Join(", ");

        public string FormattedParameterNames => Parameters.Select(p => p.Name).Union(Headers).Join(", ");

    }
}