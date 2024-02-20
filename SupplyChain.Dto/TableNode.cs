using System.Collections.Generic;

namespace SupplyChain.Dto
{
    public class TableNode
    {
        public string Name { get; }
        public List<TableNode> Children;
        public List<ColumnProperties> Columns;
        public List<Aggregate> AggregateCandidates;
        public string FieldName;
        public string Alias;
        public int Depth { get; set; }
        public bool Hidden { get; set; }
        public bool Select { get; set; }

        public TableNode(string name)
        {
            Name = name;
            Children = new List<TableNode>();
            Columns = new List<ColumnProperties>();
            AggregateCandidates = new List<Aggregate>();
        }

        public TableNode CloneWithoutChildren()
        {
            var ret = new TableNode(Name)
            {
                FieldName = FieldName,
                Alias = Alias
            };
            ret.Columns.AddRange(Columns);
            return ret;
        }

        public IEnumerable<TableNode> Flatten()
        {
            yield return this;
            foreach(var child in Children)
            {
                foreach (var x in child.Flatten())
                    yield return x;
            }
        }
        public override string ToString() => Name;
    }

    

    public class JsonField
    {
        public string Column { get; set; }
        public string Type { get; set; }
        public string Alias { get; set; }

    }
}