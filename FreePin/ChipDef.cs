using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreePin
{
    class ChipDef
    {
        public Dictionary<string, GpioPin> Pins { get; set; }
        public List<Function> Functions { get; set; }
        public List<List<string>> Header { get; set; }
    }
}
