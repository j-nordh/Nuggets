using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses;
using UtilClasses.CodeGeneration;

namespace SupplyChain.Injectors
{
    class CrudCallersInjector : IInjector
    {
        public List<string> Callers { get; set; }
        public IEnumerable<string> Using => new []{"System"};
        public CrudCallersInjector()
        {
            Callers = new List<string>();
        }

        public string Name => "Crud callers";
        public IEnumerable<string> ConstructorArgs => null;

        public IndentingStringBuilder Constructor(IndentingStringBuilder sb)
            => sb.AppendLine("_crudCallers = new Dictionary<Type, object>();").AppendObjects(Callers, c => $"Set({c});");
        public IEnumerable<string> Fields => new[] { "Dictionary<Type, object> _crudCallers;" };

        public string Inherits => null;

        public IEnumerable<string> Implements => null;

        public string BaseConstructorArguments => null;

        public IndentingStringBuilder Methods(IndentingStringBuilder sb) => sb.AppendLines(
            "public ICrudCaller<T> GetCaller<T>() => _crudCallers.TryGetValue(typeof(T), out var ret) ? (ICrudCaller<T>)ret : null;",
            "private void Set<T>(ICrudCaller<T> caller) => _crudCallers[typeof(T)] = caller;");
        public IndentingStringBuilder SubClasses(IndentingStringBuilder sb) =>sb;
    }
}
