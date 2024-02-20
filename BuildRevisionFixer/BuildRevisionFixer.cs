using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BuildRevisionFixer
{
    public class BuildRevisionFixer
    {
        public static void Run(string path, int majorVer = 1, int minorVer = 0, int buildVer = 0)
        {
            AddBuildEvent(path);
            CreateTmplFile(Directory.GetParent(path).FullName, majorVer, minorVer, buildVer);
        }

        public static string AddBuildEvent(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            var proj = doc.SelectSingleNode("Project");

            //Check if PreBuild already exists
            var node = doc.SelectSingleNode("Project/Target[@Name='PreBuild']");
            if (node!=null)
            {
                //Node exists
                var exec = node.SelectSingleNode("Exec");

                if (exec == null)
                {
                    exec = doc.CreateElement("Exec");
                    node.AppendChild(exec);
                }

                var cmd = exec.Attributes["Command"];
                if (cmd != null)
                {
                    //Command exists
                    if (!cmd.Value.Contains(EventString()))
                    {
                        //Add Name to string
                        cmd.Value = cmd.Value + "\r\n" + EventString();

                    }//Otherwise do nothing
                }
                else
                {
                    var newAttr = doc.CreateAttribute("Command");
                    newAttr.Value = EventString();
                    exec.Attributes.Append(newAttr);
                }
            }
            else
            {
                var newNode = BuildEventNode(doc);
                proj.AppendChild(newNode);

            }

            doc.Save(path);
            return doc.OuterXml;
            
        }

        public static void CreateTmplFile(string path, int majorVer = 1, int minorVer=0, int buildVer=0)
        {
            path = Path.Combine(path, "Directory.Build.props.tmpl");
            File.WriteAllText(path, GetFileContents(majorVer, minorVer, buildVer));
        }

        public static XmlNode BuildEventNode(XmlDocument doc)
        {
            var target = doc.CreateElement("Target");
            target.SetAttribute("Name", "PreBuild");
            target.SetAttribute("BeforeTargets", "PreBuildEvent");

            var exec = doc.CreateElement("Exec");
            exec.SetAttribute("Command", EventString());

            target.AppendChild(exec);

            return target;
        }

        public static string EventString()
        {
            //return @"subwcrev &quot;$(ProjectDir).\..&quot; &quot;$(ProjectDir)\Directory.Build.props.tmpl&quot; &quot;$(ProjectDir)\Directory.Build.props&quot;";
            return
                @"subwcrev ""$(ProjectDir).\.."" ""$(ProjectDir)\Directory.Build.props.tmpl"" ""$(ProjectDir)\Directory.Build.props""";
        }

        public static string GetFileContents(int majorVer = 1, int minorVer = 0, int buildVer = 0)
        {
            return 
                $@"<Project>
    <PropertyGroup>
        <Version>{majorVer}.{minorVer}.{buildVer}.$WCREV$</Version>
    </PropertyGroup>
</Project>";
        }
    }
}
