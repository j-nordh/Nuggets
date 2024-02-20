using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocScreen.Destinations
{
    class ClipboardDestination : IDestination
    {
        public string Name => "Clipboard";
        public DestinationUiConfig Config => DestinationUiConfig.None;
        public void Handle(Screenshot s) => Clipboard.SetImage(s.Image);
    }
}
