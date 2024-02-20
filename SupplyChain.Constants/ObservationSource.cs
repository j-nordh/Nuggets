using System;
using SupplyChain.Dto;

namespace SupplyChain.Constants
{
    class ObservationSource : GenericQueryConstantsBase<IdName>
    {
        public ObservationSource(CodeEnvironment env) : base(env, null)
        {
        }

        protected override string LocalPath => @"Shared\Hogia.TW.Monitoring.Shared\ObservationSource.cs";
        public override string Name => "ObservationSource";
        protected override string Query => "select Id, Name from tblSources where Name <>'RESERVED'";
        protected override string Namespace => "Hogia.TW.Monitoring.Shared";
        protected override string Declaration => "public enum ObservationSource";
        protected override string DbTable => "tblSources";
    }
}
