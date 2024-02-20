using System;
using SupplyChain.Dto;

namespace SupplyChain.Constants
{
    class ObjectType: GenericQueryConstantsBase<IdName>
    {
        protected override string LocalPath => @"Shared\HelperClasses.Constants\SysObjectType.cs";
        public override string Name => "SysObjectType";
        protected override string Query => "select * from sysObjectType";
        protected override string Namespace => "Hogia.TW.HelperClasses.Constants";
        protected override string Declaration => "public enum SysObjectType";
        protected override string DbTable => "sysObjectType";

        public ObjectType(CodeEnvironment env) : base(env, null)
        {
            Extras.Add(new IdName { Id = 0, Name = "Unknown" });
        }
    }
}
