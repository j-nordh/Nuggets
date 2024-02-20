using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses;
using UtilClasses.CodeGeneration;

namespace ScriptOMatic.Generate
{
    public class ControllerRenderer
    {
        private readonly IndentingStringBuilder _sb;
        readonly CodeEnvironment _env;

        public ControllerRenderer(IndentingStringBuilder sb, CodeEnvironment env)
        {
            _env = env;
            _sb = sb;
        }

        public void Append(string name, string typeName)
        {
            _sb.AppendObject(new FileBuilder() { Namespace = _env.Controller.Namespace }.Add(new ControllerClass(name, typeName)));
        }

        private class ControllerClass : SimpleClassBuilder
        {
            readonly string _typeName;
            public ControllerClass(string name, string typeName) : base(name+"Controller")
            {
                _typeName = typeName;
            }
            public override IEnumerable<string> Implements => new[] { $"CrudController<{_typeName}>" };
            public override IEnumerable<string> ConstructorParameters => new string[0];
            public override IEnumerable<string> Requires => new[] { "Recs.Dto"};

        }
    }
}

/*
 using Recs.Dto;
using System;
using System.Linq;

namespace Recs.Server.Controllers
{
    public class UnitsController : CrudController<Unit>
    {
    }
}
     */
