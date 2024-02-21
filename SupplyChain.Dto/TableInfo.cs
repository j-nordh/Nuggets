using UtilClasses.Interfaces;

namespace SupplyChain.Dto
{

    public class TableInfo : ICloneable<TableInfo>

    {
        public string Name { get; set; }
        public string Plural { get; set; }
        public string Singular { get; set; }

        public TableInfo()
        {
        }

        public TableInfo(TableInfo x)
        {
            Name = x.Name;
            Plural = x.Plural;
            Singular = x.Singular;
        }

        public TableInfo Clone()
        {
            return new TableInfo(this);
        }
    }
}
