using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using UtilClasses;
using UtilClasses.CodeGeneration;
using UtilClasses.Db.Extensions;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.Files;

namespace SupplyChain.Constants
{
    public abstract class ConstantsBase
    {
        protected readonly CodeEnvironment _env;

        protected abstract string LocalPath { get; }

        public abstract string Name { get; }
        protected abstract string Run(SqlConnection conn);

        public bool UpdateIfNeeded(SqlConnection conn, bool force) =>
            new FileSaver(Path.Combine(_env.Dto.Dir, LocalPath))
                .WithContent(Run(conn))
                .Forced(force)
                .SaveIfChanged();
        public string GetContent(SqlConnection conn) => Run(conn);

        public ConstantsBase(CodeEnvironment env)
        {
            _env = env;
        }
    }

    public abstract class GenericQueryConstantsBase<T> : QueryConstantsBase<T> where T : IdName
    {
        protected override Func<T, string> Format { get; }

        protected GenericQueryConstantsBase(CodeEnvironment env, Func<T, string> f = null) : base(env)
        {
            if (null == f) f = n => $"{n.Name.MakeIt().PascalCase()} = {n.Id}";
            Format = f;
        }
    }
    public abstract class QueryConstantsBase<T> : ConstantsBase
    {
        protected abstract string Query { get; }
        protected abstract string Namespace { get; }
        protected abstract string Declaration { get; }
        protected abstract Func<T, string> Format { get; }
        protected abstract string DbTable { get; }
        protected string Separator { get; set; }

        protected List<T> Extras { get; }

        protected List<ICodeElement> ExtraElements { get; }
        protected QueryConstantsBase(CodeEnvironment env) : base(env)
        {
            Separator = ",";
            Extras = new List<T>();
            ExtraElements = new List<ICodeElement>();
        }

        protected override string Run(SqlConnection conn)
        {
            var tablename = $"{conn.Database}.{DbTable}";
            return IndentingStringBuilder
                .SourceFileBuilder(Declaration, Namespace,
$@"//                                                           ____            
// This file has been generated using SupplyChain.          /\' .\    _____  
// Please do not modify it directly, instead:              /: \___\  / .  /\ 
// * Make your changes in {$"{tablename,-33}"}\' / . / /____/..\
// * Add that script to TFS!!!!                             \/___/  \'  '\  /
// * Run SupplyChain again.                                          \'__'\/ ", ExtraElements.SelectMany(ee => ee.Requires).NotNull().Distinct())
                .Query(conn, Query, Format, Extras, Separator)
                .Close()
                .AppendObjects(ExtraElements)
                .ToString();
        }
    }
    public abstract class QueryEnumBuilder : ConstantsBase
    {

        protected abstract string Query { get; }
        protected abstract string Namespace { get; }
        protected virtual string Modifier => "public";
        protected abstract Func<IDictionary<string, object>, EnumElement.Member> Format { get; }
        protected abstract string DbTable { get; }
        protected string Separator { get; set; }

        protected List<Dictionary<string, object>> Extras { get; }

        protected List<ICodeElement> ExtraElements { get; }
        protected QueryEnumBuilder(CodeEnvironment env) : base(env)
        {
            Separator = ",";
            Extras = new List<Dictionary<string, object>>();
            ExtraElements = new List<ICodeElement>();
        }

        protected override string Run(SqlConnection conn)
        {
            var tablename = $"{conn.Database}.{DbTable}";

            var members = conn.QueryDirect(Query).Union(Extras).Select(Format).ToList();


            var f = new FileBuilder { HeaderSteps = new[] { $"Make your changes in \"{tablename}\"", "Add that script to source control!!!!", "Run SupplyChain again." }, Namespace = Namespace }
            .Add(new EnumElement() { Modifier = Modifier, Name = Name, Requires = ExtraElements.SelectMany(ee => ee.Requires).NotNull().Distinct().ToList(), Members = members })
            .Add(ExtraElements);

            return f.ToString();
            //            return IndentingStringBuilder
            //                .SourceFileBuilder(Declaration, Namespace,
            //$@"//                                                           ____            
            //// This file has been generated using SupplyChain.          /\' .\    _____  
            //// Please do not modify it directly, instead:              /: \___\  / .  /\ 
            //// * Make your changes in {$"{tablename,-33}"}\' / . / /____/..\
            //// * Add that script to TFS!!!!                             \/___/  \'  '\  /
            //// * Run SupplyChain again.                                          \'__'\/ ", ExtraElements.SelectMany(ee => ee.Requires).NotNull().Distinct())
            //                .QueryDict(conn, Query, Format, Extras.Select(d=>(IDictionary<string, object>)d).ToList(), Separator)
            //                .Close()
            //                .AppendObjects(ExtraElements)
            //                .ToString();
            //        }
        }
    }
    public abstract class QueryEnumBase<T> : ConstantsBase
    {
        protected abstract string Query { get; }
        protected abstract string Namespace { get; }
        protected abstract Func<T, EnumElement.Member> Format { get; }
        protected abstract string DbTable { get; }
        protected List<ICodeElement> ExtraElements { get; }
        protected QueryEnumBase(CodeEnvironment env) : base(env)
        {
            ExtraElements = new List<ICodeElement>();
        }

        protected override string Run(SqlConnection conn)
        {
            var tablename = $"{conn.Database}.{DbTable}";
            var fb = new FileBuilder()
            {
                HeaderSteps = new[] {
                    $"Make your changes in {tablename}",
                    "Add that script to TFS!!!!",
                    "Run SupplyChain again." },
                Namespace = Namespace
            }.Add(new EnumElement() { Name = Name, Members = conn.QueryDirect<T>(Query).Select(Format).ToList() })
            .Add(ExtraElements);
            return fb.ToString();
        }
    }


    public class IdName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class IdNameAttr<T> : IdName
    {
        public T Attribute { get; set; }
    }
    public class IdNameParentAttribute<T> : IdNameAttr<T>
    {
        long ParentId { get; set; }
    }
}
