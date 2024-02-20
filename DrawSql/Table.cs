using System;
using System.Collections.Generic;
using System.Linq;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Strings;

namespace DrawSql
{
    public class Table
    {
        
        public string Name { get; set; }
        public Dictionary<string, Column> Columns { get; private set; }

        public Table(string name)
        {
            Name = name;
            Columns = new Dictionary<string, Column>();
        }

        public bool IsChanged(Table o)
        {
            if (!Name.EqualsOic(o.Name)) return true;
            var namedCols = Columns.Values.ToDictionary(c => c.Name, StringComparer.Ordinal);
            foreach (var col in o.Columns.Values)
            {
                var myC = namedCols.Maybe(col.Name);
                if (null == myC) return true;
                var diff = myC.Differences(col).ToList();
                if (diff.Any()){ return true;}
                namedCols.Remove(col.Name);
            }

            return namedCols.Any();
        }
    }

}
