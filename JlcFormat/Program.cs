// See https://aka.ms/new-console-template for more information

using System.Runtime.Intrinsics.Arm;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Integers;
using UtilClasses.Extensions.Strings;

var lines = File.ReadLines(args[0]).Skip(1);
var sb = new IndentingStringBuilder("");

if (args[0].Contains("-pos"))
{
    sb.AppendLine("Designator; Mid X; Mid Y; Layer; Rotation;");
    foreach (var l in lines)
    {
        var parts = l.Split(",");
        if (parts.Length != 7)
            continue;
        var rotation = parts[5].AsInt();
        if (rotation < 0)
            rotation += 360;
        var newLine = new List<string> { parts[0], parts[3], parts[4], parts[6], rotation.ToString() };
        sb.AppendLine(newLine.Join(";"));
    }
}
else
{
    sb.AppendLine("Comment;Designator;Footprint;JLCPCB Part#(optional)");
    foreach (var l in lines)
    {
        
        var parts = l.Split(";");
        if (parts.Length != 9)
            continue;
        var newLine = new List<string>()
        {
            parts[2],
            parts[1],
            parts[3],
        };
        sb.AppendLine(newLine.Join(";"));
    }
}
var path = args[0].SubstringBefore(".csv") + "_jlc.csv";
File.WriteAllText(path, sb.ToString());