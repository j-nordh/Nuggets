using System.Data;

namespace SupplyChain.Procs
{
    public class ParameterDef
    {
        public ParameterDef(string name, DbType dbType, int length, bool isOut)
        {
            Name = name;
            DbType = dbType;
            Length = length;
            IsOut = isOut;
        }

        public string Name { get; }
        public DbType DbType { get; }
        public int Length { get; }
        public bool IsOut { get; }

        public override string ToString()
        {
            var codeName = char.ToUpper(Name[1]) + Name.Substring(2);
            if (codeName.Equals("Name")) codeName = "NameParam";
            return $"public ParameterDef {codeName} => new ParameterDef(\"{Name}\", DbType.{DbType}, {Length});";
        }
    }
}