using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;

namespace SupplyChain.Procs
{
    class AccessorFactoryGenerator : GeneratorBase
    {
        private const string _header = @"//                                                           ____            
// This file has been generated using SupplyChain.          /\' .\    _____  
// Please do not modify it directly, instead:              /: \___\  / .  /\ 
// * Make your changes to DbDefinition.js                  \' / . / /____/..\
// * Add that script to TFS!!!!                             \/___/  \'  '\  /
// * Run SupplyChain again.                                          \'__'\/ ";
        private readonly AccessorFactorySettings _s;
        private readonly IEnumerable<(string t, string cls)> _accessorTypes;
        private readonly IEnumerable<string> _using;

        public AccessorFactoryGenerator(CodeEnvironment env, bool force, IEnumerable<string> @using = null, IEnumerable<(string t, string cls)> accessorTypes=null) : base(env, force)
        {
            _s = env.Route.Accessor.Factory;
            _using = new[] { "System", "System.Collections.Generic", "UtilClasses.Db" }
                .Union(@using ?? new string[] { })
                .Union((accessorTypes??new (string, string)[] { }).Select(at=>Globals.Map.GetNamespace(at.t)))
                .NotNullOrWhitespace()
                .Distinct();
            _accessorTypes = accessorTypes;
        }

        protected override string NewContent(string name)=>        
            IndentingStringBuilder
                .SourceFileBuilder($"public static class {_s.Name}", _s.Namespace, _header, _using)
                .AppendLines("static Dictionary<Type, Func<DbWrapper, object>> _dict;",
                    $"static {_s.Name}()",
                    "{",
                    "_dict = new Dictionary<Type, Func<DbWrapper, object>>();")
                .AppendObjects(_accessorTypes, at => $"_dict[typeof({at.t})] = db => new {at.cls}.Accessor(db);")
                .AppendLines("}",
                    "public static ICrudAccessor<T> Get<T>(DbWrapper db) => (ICrudAccessor<T>)_dict[typeof(T)](db);")
                .ToString();
    }
}
