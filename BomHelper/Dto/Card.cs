using UtilClasses;

namespace BomHelper.Dto;

public class Card
{
    public Dictionary<string, List<string>> References { get; set; } = new DictionaryOic<List<string>>();
    public string Name { get; set; }
    public int Count { get; set; }
    public string BomPath { get; set; }

}