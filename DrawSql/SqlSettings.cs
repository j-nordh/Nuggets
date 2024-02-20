using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses.Extensions.Strings;

namespace DrawSql
{
    class SqlSettings
    {
        string _idType = "bigint";
        string _stringType = "nvarchar";
        int _nameLength = 40;
        public string GetType(Column c)
        {
            var dt = c.DataType;
            if (dt.Equals(Column.IDENTIFIER)) return _idType + " IDENTITY(1,1) NOT NULL";
            if (dt.Equals(Column.FOREIGNKEY)) dt = _idType;
            if (dt.Equals(Column.NAME)) dt = $"{_stringType}({_nameLength})";
            if (dt.StartsWithOic("str")) dt = dt.ReplaceOic("str", _stringType);
            if (!c.Nullable) dt += " NOT NULL";
            if (c.DefaultValue.IsNotNullOrEmpty()) dt += $" DEFAULT {c.DefaultValue}";
            return dt;
        }
    }
}
