using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses.Extensions.Enumerables;

namespace SupplyChain.Dto.Extensions
{
    public static class TableNodeExtensions
    {
        public static ColumnProperties IdCol(this TableNode tn) => 
            tn?.Columns.IsNullOrEmpty()??true
                ? null 
                : tn.Columns.Count(c=>c.IsKey)>1
                    ? null
                    : tn.Columns.SingleOrDefault(c => c.IsKey);
    }
}
