using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UtilClasses;
using UtilClasses.Extensions.Reflections;
using UtilClasses.Interfaces;

namespace SupplyChain.Dto
{
    public class CodeEnvironment : ICloneable<CodeEnvironment>
    {
        public DbSettings Db { get; set; }
        public DtoSettings Dto { get; set; }
        public ProjectSettings Controller { get; set; }
        public RepoSettings Repo { get; set; }
        public RoutesSettings Route { get; set; }
        public string ServerSolution { get; set; }
        public List<InterfaceDef> Interfaces { get; set; }
        public string ChainFile { get; set; }
        public string BasePath { get; set; }
        

        public CodeEnvironment()
        {
            Db = new DbSettings();
            Dto = new DtoSettings();
            Controller = new ProjectSettings();
            Repo = new RepoSettings();
        }

        public CodeEnvironment(CodeEnvironment p)
        {
            Db = p.Db.Clone();
            Dto = p.Dto.Clone();
            Repo = p.Repo?.Clone();
            Controller = p.Controller?.Clone();
            Route = p.Route?.Clone();
            ServerSolution = p.ServerSolution;
            Name = p.Name;
            ChainFile = p.ChainFile;
        }

        public string Name { get; set; }

        public CodeEnvironment Clone() => new CodeEnvironment(this);
    }

    public class InterfaceDef
    {
        public string FieldName { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Type { get; set; }
    }
    [Path]
    public class RoutesSettings : ICloneable<RoutesSettings>
    {
        public RoutesSettings()
        {
            Accessor = new AccessorSettings();
        }
        public RoutesSettings(RoutesSettings p) : this()
        {
            DllPath = p.DllPath;
            Enabled = p.Enabled;
            Accessor = p.Accessor.Clone();
            Path = p.Path;
            VerbDescriptorPath = p.VerbDescriptorPath;
        }
        [Path]
        public string DllPath { get; set; }
        [Path]
        public string VerbDescriptorPath { get; set; }
        public bool Enabled { get; set; }
        [Path]
        public string Path { get; set; }
        public AccessorSettings Accessor { get; set; }
        public RoutesSettings Clone() => new RoutesSettings(this);
    }

    public class PathAttribute : Attribute
    {

    }

    public static class CodeEnvironmentExtensions
    {
        public static void SetPathsAbsolute(this object obj , string basepath)
        {
            RecursePaths(obj, p=>PathUtil.GetAbsolutePath(p, basepath), p=>!Path.IsPathRooted(p));
        }

        public static void SetPathsRelative(this object obj, string basepath)
        {
            RecursePaths(obj, p=>PathUtil.GetRelativePath(p, basepath), Path.IsPathRooted);
        }

        private static void RecursePaths(this object obj, Func<string, string> f, params Func<string, bool>[] predicates)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.HasCustomAttribute<PathAttribute>())
                {
                    var val = prop.GetValue(obj)?.ToString();
                    if(null==val)continue;
                    if(predicates.Any(p=>!p(val))) continue;
                    prop.SetValue(obj, f(val));
                    continue;
                }

                if (!prop.PropertyType.IsClass || !prop.PropertyType.HasCustomAttribute<PathAttribute>(true)) continue;
                var child = prop.GetValue(obj);
                child?.RecursePaths(f);
            }
        }
        public static ProjectSettings Clone(this ProjectSettings ps) => new ProjectSettings(ps);
    }
    [Path]
    public abstract class AccessorBaseSettings
    {
        protected AccessorBaseSettings()
        {}

        protected AccessorBaseSettings(AccessorBaseSettings p)
        {
            Enabled = p.Enabled;
            Path = p.Path;
            Namespace = p.Namespace;
        }


        public bool Enabled { get; set; }
        [Path]
        public string Path { get; set; }
        public string Namespace { get; set; }
    }
    public class AccessorFactorySettings : AccessorBaseSettings, ICloneable<AccessorFactorySettings>
    {
        public AccessorFactorySettings() { }
        public AccessorFactorySettings(AccessorFactorySettings p) :base(p)
        {
            Name = p.Name;
        }
        public string Name { get; set; }
        public AccessorFactorySettings Clone() => new AccessorFactorySettings(this);
    }
    public class AccessorSettings : AccessorBaseSettings, ICloneable<AccessorSettings>
    {
        public AccessorSettings() {
            Factory = new AccessorFactorySettings();
        }
        public AccessorSettings(AccessorSettings p): base(p)
        {
            Prefix = p.Prefix;
            Factory = Factory.Clone();
        }
        public string Prefix { get; set; }
        public AccessorFactorySettings Factory {get;set;}

        public AccessorSettings Clone() => new AccessorSettings(this);
    }

    public class DtoSettings : ProjectSettings, ICloneable<DtoSettings>
    {
        [Path]
        public string NamespaceMap { get; set; }
        [Path]
        public string ExtensionsDir { get; set; }
        public string ExtensionNs { get; set; }

        public DtoSettings()
        { }
        public DtoSettings(DtoSettings p) :base(p)
        {
            NamespaceMap = p.NamespaceMap;
            ExtensionsDir = p.ExtensionsDir;
            ExtensionNs = p.ExtensionNs;
        }
        public DtoSettings Clone() => new DtoSettings(this);
    }
    [Path]
    public class DbSettings : ICloneable<DbSettings>
    {
        public string Server { get; set; }
        public string Name { get; set; }
        [Path]
        public string DbDefinition { get; set; }
        [Path]
        public string RestoreDir { get; set; }
        [Path]
        public string IncomingDiagram { get; set; }
        [Path]
        public string Diagram { get; set; }
        [Path]
        public string CombinedSqlFile { get; set; }
        [Path]
        public string TableSqlFile { get; set; }
        [Path]
        public string ProcedureSqlFile { get; set; }
        public DbSettings() { }
        public DbSettings(DbSettings p)
        {
            Server = p.Server;
            Name = p.Name;
            DbDefinition = p.DbDefinition;
            RestoreDir = p.RestoreDir;
            IncomingDiagram = p.IncomingDiagram;
            Diagram = p.Diagram;
        }
        public DbSettings Clone() => new DbSettings(this);
    }

    public class RepoSettings : ProjectSettings, ICloneable<RepoSettings>
    {
        public string RepoCreator { get; set; }
        public DefSettings Def { get; set; }
        public string Base { get; set; }
        public bool BaseIsGeneric { get; set; }
        [Path]
        public string Creator { get; set; }
        public RepoSettings() { }
        public RepoSettings(RepoSettings s) :base(s)
        {
            RepoCreator = s.RepoCreator;
            Def = s.Def.Clone();
        }

        public RepoSettings Clone() => new RepoSettings(this);
        
    }
    [Path]
    public class DefSettings : ICloneable<DefSettings>
    {
        public string Namespace { get; set; }
        [Path]
        public string Dir { get; set; }

        public DefSettings(DefSettings o)
        {
            if (null == o) return;
            Namespace = o.Namespace;
            Dir = o.Dir;
        }

        public DefSettings Clone() => new DefSettings(this);
    }
    [Path]
    public class ProjectSettings
    {
        [Path]
        public string Project { get; set; }
        [Path]
        public string Dir { get; set; }
        public string Namespace { get; set; }
        public ProjectSettings() { }
        public ProjectSettings(ProjectSettings p)
        {
            Project = p.Project;
            Dir = p.Dir;
            Namespace = p.Namespace;
        }
    }
}
