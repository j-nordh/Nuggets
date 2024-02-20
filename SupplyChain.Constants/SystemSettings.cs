using System;
using SupplyChain.Dto;

namespace SupplyChain.Constants
{
    class SystemSettings:QueryConstantsBase<string>
    {
        public SystemSettings(CodeEnvironment env) : base(env)
        {
        }

        protected override string LocalPath => @"Shared\HelperClasses.Constants\SystemSettings.cs";
        public override string Name => "SystemSettings";
        protected override string Query => "select Name from tblSystemSetting";
        protected override string Namespace => "Hogia.TW.HelperClasses.Constants";
        protected override string Declaration => "public enum SystemSettings";
        protected override Func<string, string> Format => x => x;
        protected override string DbTable => "tblSystemSetting";
    }
}
