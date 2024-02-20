using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreePin
{
    class Function
    {
        public string Name { get; set; }
        public List<string> Pins { get; set; }
        public string Color { get; set; }
        public override string ToString() =>Name;
    }
}
