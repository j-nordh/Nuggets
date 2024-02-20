using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;

namespace FreePin
{
    class GpioPin
    {
        public string In { get; set; }
        public string Out { get; set; }
        public string Comment { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? HeaderLocation { get; set; }
    }


    class DrawPin
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Color Color { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public DrawPin With(Color c) => this.Do(() => Color = c);
    }

     static class DrawPinExtensions
     {
         public static DrawPin Get(this List<DrawPin> lst, string name) =>
             lst.FirstOrDefault(p => p.Name.EqualsOic(name));
     }
}
