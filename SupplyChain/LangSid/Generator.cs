//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Xml;
//using Newtonsoft.Json;
//using UtilClasses.Extensions.Enumerables;
//using UtilClasses.Extensions.Strings;

//namespace SupplyChain.LangSid
//{
//    internal class Generator : GeneratorBase
//    {
//        public Generator(string basePath, bool force) : base(basePath, force)
//        {
//        }

//        public void Run()
//        {
//            Section.Init(BasePath);
//            var doc = new XmlDocument();
//            doc.Load(Path.Combine(BasePath, @"Shared\HelperClasses\Language\Localization.xml"));
//            var xmlSections = doc["Localization"]?["Sections"];
//            var resourceCount = 0;
//            var sectionCount = 0;
//            var sections = (xmlSections?.ChildNodes.Cast<XmlNode>() ?? new XmlNode[] { }).Select(Section.FromNode)
//                .NotNull();
//            foreach (var section in sections)
//            {
//                var filename = Path.Combine(BasePath, @"Shared\HelperClasses.Constants\LangSID",
//                    $"{section.ClassName}.cs");

//                var res = section.ToString();
//                Console.Write($"{section.ClassName}: ");
//                if (Force || !res.RemoveAllWhitespace().Equals(File.ReadAllText(filename).RemoveAllWhitespace()))
//                {
//                    File.WriteAllText(filename, section.ToString());
//                    Console.WriteLine($"Wrote {section.Resources.Count()} resources.");
//                    resourceCount += section.Resources.Count();
//                    sectionCount += 1;
//                }
//                else
//                    Console.WriteLine("Unchanged.");
//            }

//            Console.WriteLine($"Done generating LangSids. {sectionCount} sections and {resourceCount} resources updated.");
//        }

//        protected override string NewContent()
//        {
            
//        }
//    }
//}
