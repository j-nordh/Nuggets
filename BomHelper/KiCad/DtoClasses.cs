using System.Xml.Serialization;

namespace BomHelper.KiCad;
/* 
Licensed under the Apache License, Version 2.0

http://www.apache.org/licenses/LICENSE-2.0
*/


[XmlRoot(ElementName = "comment")]
public class Comment
{
    [XmlAttribute(AttributeName = "number")]
    public string Number { get; set; }
    [XmlAttribute(AttributeName = "value")]
    public string Value { get; set; }
}

[XmlRoot(ElementName = "title_block")]
public class Title_block
{
    [XmlElement(ElementName = "title")]
    public string Title { get; set; }
    [XmlElement(ElementName = "company")]
    public string Company { get; set; }
    [XmlElement(ElementName = "rev")]
    public string Rev { get; set; }
    [XmlElement(ElementName = "date")]
    public string Date { get; set; }
    [XmlElement(ElementName = "source")]
    public string Source { get; set; }
    [XmlElement(ElementName = "comment")]
    public List<Comment> Comment { get; set; }
}

[XmlRoot(ElementName = "sheet")]
public class Sheet
{
    [XmlElement(ElementName = "title_block")]
    public Title_block Title_block { get; set; }
    [XmlAttribute(AttributeName = "number")]
    public string Number { get; set; }
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    [XmlAttribute(AttributeName = "tstamps")]
    public string Tstamps { get; set; }
}

[XmlRoot(ElementName = "design")]
public class Design
{
    [XmlElement(ElementName = "source")]
    public string Source { get; set; }
    [XmlElement(ElementName = "date")]
    public string Date { get; set; }
    [XmlElement(ElementName = "tool")]
    public string Tool { get; set; }
    [XmlElement(ElementName = "sheet")]
    public List<Sheet> Sheet { get; set; }
}

[XmlRoot(ElementName = "libsource")]
public class Libsource
{
    [XmlAttribute(AttributeName = "lib")]
    public string Lib { get; set; }
    [XmlAttribute(AttributeName = "part")]
    public string Part { get; set; }
    [XmlAttribute(AttributeName = "description")]
    public string Description { get; set; }
}

[XmlRoot(ElementName = "property")]
public class Property
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    [XmlAttribute(AttributeName = "value")]
    public string Value { get; set; }
}

[XmlRoot(ElementName = "sheetpath")]
public class Sheetpath
{
    [XmlAttribute(AttributeName = "names")]
    public string Names { get; set; }
    [XmlAttribute(AttributeName = "tstamps")]
    public string Tstamps { get; set; }
}

[XmlRoot(ElementName = "comp")]
public class Comp
{
    [XmlElement(ElementName = "value")]
    public string Value { get; set; }
    [XmlElement(ElementName = "footprint")]
    public string Footprint { get; set; }
    [XmlElement(ElementName = "libsource")]
    public Libsource Libsource { get; set; }
    [XmlElement(ElementName = "property")]
    public List<Property> Property { get; set; }
    [XmlElement(ElementName = "sheetpath")]
    public Sheetpath Sheetpath { get; set; }
    [XmlElement(ElementName = "tstamps")]
    public string Tstamps { get; set; }
    [XmlAttribute(AttributeName = "ref")]
    public string Ref { get; set; }
    [XmlElement(ElementName = "datasheet")]
    public string Datasheet { get; set; }
}

[XmlRoot(ElementName = "components")]
public class Components
{
    [XmlElement(ElementName = "comp")]
    public List<Comp> Comp { get; set; }
}

[XmlRoot(ElementName = "field")]
public class Field
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    [XmlText]
    public string Text { get; set; }
}

[XmlRoot(ElementName = "fields")]
public class Fields
{
    [XmlElement(ElementName = "field")]
    public List<Field> Field { get; set; }
}

[XmlRoot(ElementName = "pin")]
public class Pin
{
    [XmlAttribute(AttributeName = "num")]
    public string Num { get; set; }
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }
}

[XmlRoot(ElementName = "pins")]
public class Pins
{
    [XmlElement(ElementName = "pin")]
    public List<Pin> Pin { get; set; }
}

[XmlRoot(ElementName = "libpart")]
public class Libpart
{
    [XmlElement(ElementName = "fields")]
    public Fields Fields { get; set; }
    [XmlElement(ElementName = "pins")]
    public Pins Pins { get; set; }
    [XmlAttribute(AttributeName = "lib")]
    public string Lib { get; set; }
    [XmlAttribute(AttributeName = "part")]
    public string Part { get; set; }
    [XmlElement(ElementName = "description")]
    public string Description { get; set; }
    [XmlElement(ElementName = "docs")]
    public string Docs { get; set; }
    [XmlElement(ElementName = "footprints")]
    public Footprints Footprints { get; set; }
}

[XmlRoot(ElementName = "footprints")]
public class Footprints
{
    [XmlElement(ElementName = "fp")]
    public List<string> Fp { get; set; }
}

[XmlRoot(ElementName = "libparts")]
public class Libparts
{
    [XmlElement(ElementName = "libpart")]
    public List<Libpart> Libpart { get; set; }
}

[XmlRoot(ElementName = "library")]
public class Library
{
    [XmlElement(ElementName = "uri")]
    public string Uri { get; set; }
    [XmlAttribute(AttributeName = "logical")]
    public string Logical { get; set; }
}

[XmlRoot(ElementName = "libraries")]
public class Libraries
{
    [XmlElement(ElementName = "library")]
    public List<Library> Library { get; set; }
}

[XmlRoot(ElementName = "node")]
public class Node
{
    [XmlAttribute(AttributeName = "ref")]
    public string Ref { get; set; }
    [XmlAttribute(AttributeName = "pin")]
    public string Pin { get; set; }
    [XmlAttribute(AttributeName = "pintype")]
    public string Pintype { get; set; }
    [XmlAttribute(AttributeName = "pinfunction")]
    public string Pinfunction { get; set; }
}

[XmlRoot(ElementName = "net")]
public class Net
{
    [XmlElement(ElementName = "node")]
    public List<Node> Node { get; set; }
    [XmlAttribute(AttributeName = "code")]
    public string Code { get; set; }
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "nets")]
public class Nets
{
    [XmlElement(ElementName = "net")]
    public List<Net> Net { get; set; }
}

[XmlRoot(ElementName = "export")]
public class Export
{
    [XmlElement(ElementName = "design")]
    public Design Design { get; set; }
    [XmlElement(ElementName = "components")]
    public Components Components { get; set; }
    [XmlElement(ElementName = "libparts")]
    public Libparts Libparts { get; set; }
    [XmlElement(ElementName = "libraries")]
    public Libraries Libraries { get; set; }
    [XmlElement(ElementName = "nets")]
    public Nets Nets { get; set; }
    [XmlAttribute(AttributeName = "version")]
    public string Version { get; set; }
}
