using UtilClasses;

namespace SourceFuLib
{
    public class SourceFuSettings
    {
        public string BasePath { get; set; }
        public string MsBuildPath { get; set; }
        public string SupplyChainPath { get; set; }
        public string DrawSqlPath { get; set; }
        public DictionaryOic<string> Environments { get; set; }
    }
}
