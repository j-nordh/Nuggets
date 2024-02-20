using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Common.Dto;
using Newtonsoft.Json;
using SupplyChain.Dto.Extensions;
using UtilClasses;
using UtilClasses.CodeGen;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.Files;

namespace SupplyChain.Routes
{
    class Generator
    {
        private static string _assemblyLoadPath;
        private readonly CodeEnvironment _env;
        public bool Force { get; }

        public Generator(CodeEnvironment env, bool force)
        {
            _env = env;
            Force = force;
        }

        public void GenerateRoutes()
        {
            if (!(_env.Route?.Enabled ?? false)) return;
            if (_env.Route.DllPath.IsNullOrEmpty())
            {
                if (!File.Exists(_env.Route.VerbDescriptorPath))
                {
                    Console.Error.WriteLine("Could not find the VerbDescriptor file"); 
                    return;
                }
                GenerateRoutes(JsonConvert.DeserializeObject<Dictionary<string, List<VerbDescriptor>>>(File.ReadAllText(_env.Route.VerbDescriptorPath)));
            }
            else if (!File.Exists(_env.Route.DllPath))
            {
                Console.Error.WriteLine("Could not find the dll at: " + _env.Route.DllPath);
                return;
            }
            AccessorGenerator accGenerator = null;

            if (_env.Route.Accessor?.Enabled ?? false) accGenerator
            = new AccessorGenerator
            {
                Namespace = _env.Route.Accessor.Namespace,
                Prefix = _env.Route.Accessor.Prefix,
                FilePath = _env.Route.Accessor.Path
            }
           .Requiring("Recs.Dto", "Recs.Routes", "Recs.Dto.Requests");
            //Console.WriteLine("Generating RECS routes...");

            GenerateRoutes(_env.Route.DllPath, @"Clients\Recs\Recs.Routes\", accGenerator);
        }

        public void GenerateRoutes(Dictionary<string, List<VerbDescriptor>> vds) =>
            GenerateRoutes(vds.Select(Controller.FromVerbDescriptors).ToList(), _ => _env.Route.Path);

        public void GenerateRoutes(string dllPath, string dir, AccessorGenerator accGen) => GenerateRoutes(dllPath, _ => _env.Route.Path, null, accGen);
        private void GenerateRoutes(string dllPath, Func<Controller, string> f, Action<Controller> extraAction = null, AccessorGenerator accGen = null)
        {
            if (File.Exists(dllPath))
            {
                _assemblyLoadPath = Path.GetDirectoryName(dllPath);
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ReflectionAssemblyResolve;
                var ass = Assembly.ReflectionOnlyLoadFrom(dllPath);
                var controllers = ass.DefinedTypes
                    .Where(c => c.Name.ContainsOic("Controller"))
                    .Where(c => !c.IsAbstract)
                    .Select(Controller.FromType).NotNull().ToList();
                GenerateRoutes(controllers, f, extraAction, accGen);
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= ReflectionAssemblyResolve;
            }
            else if (File.Exists(_env.Route.VerbDescriptorPath))
            {
                var vds = _env.Route.GetVerbDescriptors();
                GenerateRoutes(vds.Select(Controller.FromVerbDescriptors).ToList(), f, extraAction, accGen);
            }
        }
        public void GenerateRoutes(List<Controller> controllers, Func<Controller, string> f, Action<Controller> extraAction = null, AccessorGenerator accGen = null)
        {
            var updatedFiles = new List<string>();
            foreach (var controller in controllers)
            {
                Console.Write($"  {controller.Name}: ");
                var filePath = Path.Combine(f(controller), controller.Name + ".cs");
                var current = (File.Exists(filePath) ? File.ReadAllText(filePath, Encoding.UTF8) : "")
                    .RemoveAllWhitespace();
                var generated = controller.ToString();

                if (current.Equals(generated.RemoveAllWhitespace()) && !Force)
                {
                    Console.WriteLine("Unchanged.");
                    extraAction?.Invoke(controller);
                    continue;
                }
                File.WriteAllText(filePath, generated, Encoding.UTF8);
                Console.WriteLine($"Updated. Wrote {generated.LineCount()} lines.");
                extraAction?.Invoke(controller);
                updatedFiles.Add(controller.Name + ".cs");
            }
            if (null != accGen)
            {
                Console.Write("Accessor: ");
                accGen.Controllers = controllers;

                var generated = accGen.ToString();
                Console.WriteLine(new FileSaver(accGen.FilePath, generated).Forced(Force).SaveIfChanged()
                    ? $"Updated. Wrote {generated.LineCount()} lines."
                    : "Unchanged.");

            }
            Console.WriteLine($"Route generation complete, checked {controllers.Count} controllers and updated {updatedFiles.Count} of them.");
            var dir = f(null);
            var projFile = Directory.GetFiles(dir, "*.csproj").FirstOrDefault();
            if (projFile.IsNotNullOrEmpty())
            {
                new CsProject(projFile)
                    .Add_Compile(updatedFiles.ToArray())
                    .Save();
            }
        }

        public static Assembly ReflectionAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var p = Path.Combine(_assemblyLoadPath, args.Name.SubstringBefore(",") + ".dll");
            if (File.Exists(p)) return Assembly.ReflectionOnlyLoadFrom(p);
            var ass = Assembly.ReflectionOnlyLoad(args.Name);
            return ass;
        }

        private class DirSelector
        {
            private readonly string _basePath;
            private readonly Dictionary<Requirements, string> _dirs;

            public DirSelector(string basePath)
            {
                _basePath = basePath;
                _dirs = new Dictionary<Requirements, string>();
            }

            public string this[Requirements r]
            {
                get => Path.Combine(_dirs[r]);
                set
                {
                    if (!Path.IsPathRooted(value)) value = Path.Combine(_basePath, value);
                    if (!Directory.Exists(value))
                    {
                        Console.WriteLine($"Could not find the {r} output dir at: {value}");
                        return;
                    }
                    _dirs[r] = value;
                }
            }

            public bool Ok => Enum.GetValues(typeof(Requirements)).Cast<Requirements>().All(r => _dirs.ContainsKey(r));

        }
    }
}
