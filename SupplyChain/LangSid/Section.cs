using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.LangSid
{
    internal class Section
    {
        public readonly string Name;
        public readonly List<Section> SubSections;
        private readonly List<Resource> _resources;
        private readonly bool _generateSubSections;
        private readonly Section _parent;
        private static Dictionary<string, string> _supportedSections;
        public string ClassName;
        public Section(string name, bool generateSubSections, Section parent=null)
        {
            _generateSubSections = generateSubSections;
            Name = name.TrimStart('*');
            SubSections = new List<Section>();
            _resources = new List<Resource>();
            _parent = parent;
            ClassName = _parent==null? Name.Replace("_", ""): Name.Substring(_parent.FullName.Length).Replace("_","");
        }

        public static void Init(string basePath)
        {
            _supportedSections = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var s in JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(
                Path.Combine(basePath,
                    @"Shared\HelperClasses\Language\LangSidSections.json"))))
            {
                _supportedSections[s.TrimStart('*')] = s;
            }
            
        }
        public static Section FromNode(XmlNode node)
        {
            var name = node.Attributes?["Name"].Value;
            if (name.IsNullOrEmpty()) return null;
            name = _supportedSections.TryGetValue(name);
            if (name.IsNullOrEmpty()) return null;
            if (name.IndexOf(" ", StringComparison.CurrentCultureIgnoreCase) >= 0)
                throw new ArgumentException();
            var section = new Section(name, name.StartsWith("*"));
            (node["Resources"]?.ChildNodes.Cast<XmlNode>() ?? new XmlNode[] { }).ForEach(section.AddResource);
            return section;
        }

        public void AddResource(XmlNode r)
        {
            var res = new Resource(
                r?.Attributes?["Id"]?.Value,
                r?.Attributes?["Description"]?.Value,
                r?["Translations"]?.ChildNodes.Cast<XmlNode>()
                    .Select(
                        n =>
                            new KeyValuePair<string, string>(n?.Attributes?["Language"].Value,
                                n?.Attributes?["Text"].Value))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            if (res.Id.IsNullOrEmpty()) return;
            AddResource(res);
        }

        private string FullName
        {
            get
            {
                if(null==_parent) return "";
                var ret = _parent.FullName;
                if (ret.Length > 0) ret += "_";
                ret += ClassName;
                return ret;
            }
                
        }

        public IEnumerable<Resource> Resources => SubSections.SelectMany(s => s.Resources).Concat(_resources);

        public bool AddResource(Resource res)
        {
            if (!_generateSubSections)
            {
                _resources.Add(res);
                return true;
            }

            var fullName = FullName;
            if (!res.Id.StartsWith(fullName) && _parent!= null)
                return false;
            if (SubSections.AsSorted((a,b)=>b.ClassName.Length.CompareTo(a.ClassName.Length)).Any(s=>s.AddResource(res))) return true;

            var i = res.Id.Length > fullName.Length + 1 ? res.Id.IndexOf('_', fullName.Length+1) : -1;
            if (i < 0)
            {
                _resources.Add(res);
                return true;
            }
            var start = res.Id.Substring(0, i);
            var siblings = _resources.Where(r => r.Id.StartsWith(start)).ToList();
            if (siblings.Count > 1)
            {
                var s = new Section(start, true, this);
                SubSections.Add(s);
                siblings.Add(res);
                s._resources.AddRange(siblings);
                siblings.ForEach(sib=>_resources.Remove(sib));
                return true;
            }
            _resources.Add(res);
            return true;
        }

        public override string ToString()
        {
            var sb = new IndentingStringBuilder("\t")
                .AppendLine("using Hogia.TW.HelperClasses.Constants.LangSID;")
                .AppendLine("namespace Hogia.TW.HelperClasses.Constants.LangSID")
                .AppendLine("{")
                .AppendLine("//         ::::::::  :::::::::: ::::    ::: :::::::::: :::::::::      ::: ::::::::::: :::::::::: :::::::::")
                .AppendLine("//       :+:    :+: :+:        :+:+:   :+: :+:        :+:    :+:   :+: :+:   :+:     :+:        :+:    :+:")
                .AppendLine("//      +:+        +:+        :+:+:+  +:+ +:+        +:+    +:+  +:+   +:+  +:+     +:+        +:+    +:+ ")
                .AppendLine("//     :#:        +#++:++#   +#+ +:+ +#+ +#++:++#   +#++:++#:  +#++:++#++: +#+     +#++:++#   +#+    +:+  ")
                .AppendLine("//    +#+   +#+# +#+        +#+  +#+#+# +#+        +#+    +#+ +#+     +#+ +#+     +#+        +#+    +#+   ")
                .AppendLine("//   #+#    #+# #+#        #+#   #+#+# #+#        #+#    #+# #+#     #+# #+#     #+#        #+#    #+#    ")
                .AppendLine("//   ########  ########## ###    #### ########## ###    ### ###     ### ###     ########## #########      ")
                .AppendLine("//The content of this file is generated from Localization.xml using the SupplyChain Tool. DO NOT MODIFY")
                .AppendLine("//this file directly, make your changes to Localization.xml, run SupplyChain and watch the magic happen :-)")
                .AppendLine().Indent();
            Append(sb);
            sb.Outdent().AppendLine("}");
            return sb.ToString();
        }

        private void Append(IndentingStringBuilder sb)
        {
            sb.AppendLine($"public class {ClassName}: LangSidSection<{ClassName}>")
                .AppendLine("{").Indent();
            if (null == _parent) sb.AppendLine($"private const string SECTION = \"{Name}\";");
            SubSections.ForEach(s=>s.Append(sb));
            var name = FullName;
            if (name.IsNullOrEmpty()) name = ClassName;
            foreach (var res in _resources)
            {
                res.Init(name);
                sb.AppendLine(res.Render());
            }
            sb.Outdent()
                .AppendLine("}");

        }
    }
}