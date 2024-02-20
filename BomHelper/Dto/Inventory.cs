using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomHelper.Dto
{
    public class InventoryItem
    {
        public int Amount { get; set; }
        public string Note { get; set; }
        public DateTime? Date { get; set; }
    }
}
