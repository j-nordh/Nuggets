using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace DocScreen
{
    interface IDestination
    {
        string Name { get; }
        DestinationUiConfig Config { get; }
        void Handle(Screenshot s);
    }

    public class DestinationUiConfig
    {
        public bool NeedsConfig { get; }
        public bool ShowPath { get; }
        public bool ShowName { get; }
        public List<string> Checkboxes { get; }
        public DestinationUiConfig(bool needsConfig, bool showPath, bool showName)
        {
            NeedsConfig = needsConfig;
            ShowPath = showPath;
            ShowName = ShowName;
            Checkboxes =  new List<string>();
        }
        public DestinationUiConfig WithCheckbox(string chk)
        {
            Checkboxes.Add(chk);
            return this;
        }
        public static DestinationUiConfig None => new DestinationUiConfig(false, false,false);
        public static DestinationUiConfig Name => new DestinationUiConfig(true, false, true);
    }
    public class Screenshot
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Image Image { get; set; }
        public Dictionary<string, bool> Checkboxes { get; }
        public Screenshot()
        {
            Checkboxes = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
