using UtilClasses.Extensions.Strings;

namespace DrawSql
{
    class FromDbColumn : Column
    {
        public FromDbColumn()
        {
        }

        public int TableId { get; set; }
        public int MaxLength { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public override string DataType
        {
            get
            {
                var ret = base.DataType;
                if (ret.EqualsOic("nvarchar")) return ret + (MaxLength == -1 ? "(MAX)" : $"({MaxLength / 2})");
                if (ret.EqualsOic("decimal")) return ret + $"({Precision},{Scale})";
                return ret;
            }
            set => base.DataType = value;
        }

        public override string DefaultValue
        {
            get => base.DefaultValue?.Trim('(', ')');
            set => base.DefaultValue = value;
        }
    }
}
