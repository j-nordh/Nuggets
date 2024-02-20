using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.Files;

namespace DrawSql
{
    public class DrawingParser
    {
        public Dictionary<string, Table> Tables { get; }

        readonly string _path;
        private readonly string _outputPath;

        public DrawingParser(string path, string outputPath)
        {
            _path = path;
            _outputPath = outputPath;
            Tables = new Dictionary<string, Table>();
        }

        private class Label
        {
            public string Target { get; set; }
            public string Name { get; set; }
            public List<string> Sources { get; set; }
            public Label() { Sources = new List<string>(); }
        }
        public void Parse()
        {
            Stream readStream;
            var content = File.ReadAllText(_path);
            if (!content.ContainsOic("mxCell"))
            {
                var start = content.IndexOf(">", content.IndexOf("<diagram")) + 1;
                var end = content.IndexOf("</diagram", start);
                var compressed = content.Substring(start, end - start);
                using (var sIn = new PipeStream())
                using (var wr = new BinaryWriter(sIn))
                {
                    wr.Write(Convert.FromBase64String(compressed));
                    readStream = new PipeStream();
                    using (var dStream = new DeflateStream(sIn, CompressionMode.Decompress))
                    using (var r = new StreamReader(dStream))
                    {
                        content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" + Uri.UnescapeDataString(r.ReadToEnd());
                    }
                }
            }

            readStream = new PipeStream(content);
            if (null != _outputPath)
            {
                var doc = XDocument.Parse(content);
                FileSaver.SaveIfChanged(_outputPath, doc.ToString());
            }


            var rdr = XmlReader.Create(readStream);
            string name = "";
            string id = "";
            string parentId = "";
            string target = "";
            string source = "";
            bool isFK = false;
            bool isTable = false;
            bool isLabel = false;
            var fks = new List<(string source, string target)>();
            var lbls = new Dictionary<string, Label>();
            var constraint = "";
            while (rdr.Read())
            {
                if (rdr.NodeType == XmlNodeType.EndElement)
                {
                    if (!rdr.Name.EqualsOic("mxCell")) continue;
                    if (id.IsNullOrEmpty()) continue;
                    if (isFK)
                    {
                        if (source.IsNotNullOrEmpty() && target.IsNotNullOrEmpty())
                        {
                            fks.Add((source, target));
                        }
                    }
                    else
                    {
                        if (isTable && parentId.EqualsOic("1"))
                        {
                            var t = new Table(name);
                            Tables.Add(id, t);
                        }
                        else if (isLabel)
                        {
                            lbls.Add(id, new Label { Name = name });
                        }
                        else
                        {
                            if (!parentId.EqualsOic("1"))
                                Tables[parentId].Columns.Add(id, new Column(name));
                        }
                    }
                }
                if (rdr.NodeType == XmlNodeType.Element && rdr.Name.EqualsOic("mxCell"))
                {
                    id = rdr.GetAttribute("id") ?? "0";
                    name = rdr.GetAttribute("value") ?? "";
                    parentId = rdr.GetAttribute("parent") ?? "";
                    source = rdr.GetAttribute("source") ?? "";
                    target = rdr.GetAttribute("target") ?? "";
                    isFK = (rdr.GetAttribute("style") ?? "").StartsWith("edgeStyle");
                    isTable = (rdr.GetAttribute("style") ?? "").StartsWith("swimlane");
                    isLabel = (rdr.GetAttribute("style") ?? "").StartsWith("ellipse;");
                }

                if (rdr.NodeType == XmlNodeType.Element && rdr.Name.EqualsOic("object"))
                {
                    constraint = rdr.GetAttribute("Constraint");
                }
            }

            foreach (var lbl in lbls)
            {
                var fk = fks.Where((s, t) => s.source.Equals(lbl.Key)).FirstOrNull();
                if(null == fk)
                    continue;
                lbl.Value.Target = fk.Value.target;
                fks.Remove(fk.Value);
            }
            foreach (var fk in fks)
            {
                var t = lbls.Maybe(fk.target)?.Target ?? fk.target;
                FindColumn(fk.source).ForeignKey = t;
            }
        }
        public List<ForeignKey> GetForeignKeys(Table t)
        {
            var ret = new List<ForeignKey>();
            foreach (var c in t.Columns.Values)
            {
                if (c.ForeignKey.IsNullOrEmpty()) continue;
                var fk = GetFullName(c.ForeignKey);
                ret.Add(new ForeignKey(t.Name, c.Name, fk.table, fk.column));
            }
            return ret;
        }


        public Column FindColumn(string id)
        {
            foreach (var t in Tables.Values)
            {
                foreach (var c in t.Columns)
                {
                    if (c.Key == id)
                        return c.Value;
                }
            }
            return null;
        }

        private (string table, string column) GetFullName(string id)
        {
            foreach (var t in Tables.Values)
            {
                foreach (var c in t.Columns)
                {
                    if (c.Key.EqualsOic(id))
                        return (t.Name, c.Value.Name);
                }
            }
            return (null, null);
        }
    }
}
