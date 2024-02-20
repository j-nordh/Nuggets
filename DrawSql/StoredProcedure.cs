using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses.Extensions.Strings;

namespace DrawSql
{
    class StoredProcedure
    {
        public string Name { get; set; }
        public string Definition { get; set; }
        public override string ToString() => "CREATE OR ALTER PROCEDURE" + Definition.SubstringAfter("PROCEDURE");

    }
}
