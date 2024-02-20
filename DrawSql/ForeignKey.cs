namespace DrawSql
{
    public class ForeignKey
    {
        public string Name { get; set; }
        public string ReferencingTable { get; set; }
        public string ReferencingColumn { get; set; }
        public string ReferencedTable { get; set; }
        public string ReferencedColumn { get; set; }

        public ForeignKey(string referencingTable, string referencingColumn, string referencedTable, string referencedColumn)
        {
            ReferencingTable = referencingTable;
            ReferencingColumn = referencingColumn;
            ReferencedTable = referencedTable;
            ReferencedColumn = referencedColumn;
        }

        public ForeignKey()
        {
        }

        public string Drop => $"ALTER TABLE {ReferencingTable} DROP CONSTRAINT {Name}";
    }
}
