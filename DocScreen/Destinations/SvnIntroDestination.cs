using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
namespace DocScreen.Destinations
{
    class SvnIntroDestination : IDestination
    {
        public string Name => "SVN Intro";

        public DestinationUiConfig Config => DestinationUiConfig.Name.WithCheckbox("Lightbox");

        public void Handle(Screenshot s)
        {
            var path = Path.Combine(@"c:\Source\Nuggets\SvnIntro\img\", s.Name + ".png");
            s.Image.Save(path, ImageFormat.Png);
            if(s.Checkboxes["Lightbox"])
            {
                Clipboard.SetText($@"<a href=""img/{s.Name}.png"" data-lightbox=""img-{s.Name}"">
    <img src=""img/{s.Name}.png"" alt=""{s.Name}"" style ="" ></a> ");
            }
            else
            {
                Clipboard.SetText($@"<img src=""img/{s.Name}.png"" alt=""{s.Name}"" style = "" >");
            }
        }
    }
}
