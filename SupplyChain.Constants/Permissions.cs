using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Dto;
using UtilClasses.CodeGeneration;

namespace SupplyChain.Constants
{
    class Permissions : QueryEnumBase<PermissionDTO>
    {
        public Permissions(CodeEnvironment env) : base(env)
        {
        }

        public override string Name => "Permissions";

        protected override string Query => "select * from tblPermissions";

        protected override string Namespace => "Recs.Dto";

        protected override string DbTable => "tblPermissions";

        protected override string LocalPath => @"Shared\Recs\Recs.Dto\Permissions.cs";

        protected override Func<PermissionDTO, EnumElement.Member> Format => p=> new EnumElement.Member() { Name = p.Tag, Id=(int)p.Id, Attributes =  new[] {$"[Description({p.Description})]" }.ToList() };
    }

    class PermissionDTO
    {
        public long Id { get; set; }
        public string Tag { get; set; }
        public string Description { get; }
    }
}
