using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace BomHelper.Dto;

public class Deck
{
    public List<Component> Components { get; set; } = new();
    public List<Card> Cards { get; set; } = new();
    public DictionaryOic< decimal> ExchangeRates { get; set; }
    public HashSet<string> Ignore { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public string Name { get; set; }
    [JsonIgnore]
    public DictionaryOic<Component> ComponentDict => Components.ToDictionaryOic(c => c.Name);

    public Dictionary<string, List<string>> FindOrphans()
    {
        var ret = new DictionaryOic<List<string>>();
        foreach (var card in Cards)
        {
            var lst = card.References.Keys.Except(ComponentDict).ToList();
            if (lst.Any())
                ret[card.Name] = lst;
        }
        return ret;
    }

    public List<string> Unused() =>
        ComponentDict.Keys.Where(r => this.CountComponents(r) == 0).ToList();

    public int CountComponents(string reference)
    {
        int i = 0;
        foreach (var card in Cards)
        {
            i += (card.References.Maybe(reference)?.Count ?? 0) * card.Count;
        }

        return i;
    }

    public List<KeyValuePair<string, IEnumerable<string>>> Missing()
    {
        var comps = ComponentDict;
        return Cards.SelectMany(c => c.References.Keys).Distinct().Where(c => !comps.ContainsKey(c))
            .ToDictionary(c => c,
                c => Cards.Where(card => card.References.ContainsKey(c)).Select(card => card.Name)).ToList();
    }
}

public static class DeckExtensions
{
    public static Card AddReference(this Card c, string reference, params string[] components)
    {
        c.References.GetOrAdd(reference).AddRange(components);
        return c;
    }

    public static Deck AddComponent(this Deck d, string reference, string url)
    {
        d.Components.Add(new() { Name = reference, Urls = new (){ url } });
        return d;
    }

    public static int CountComponents(this Deck d, string reference)
    {
        int i = 0;
        foreach (var card in d.Cards)
        {
            i += (card.References.Maybe(reference)?.Count ?? 0) * card.Count;
        }

        return i;
    }

    public static IPriceFetcher GetFetcher(this Deck d, string uri, string productId, DictionaryOic<string> partNumbers)
    {
        var key = uri.StartsWithAnyOic("http://", "https://")
            ? new Uri(uri).Host.ToLower().Replace("www.", "")
            : uri.SubstringBefore(":").ToLower();
        return key switch
        {
            "digikey.com" or "digikey.se" => new DigiKey.PriceFetcher(productId?? uri, partNumbers),
            "eur" => new CurrencyPriceFetcher(uri, d.ExchangeRates["EUR"]),
            "olimex.com" => new OlimexPriceFetcher(uri, d.ExchangeRates["EUR"]),
            "se.farnell.com" => new Farnell.PriceFetcher(uri, partNumbers),
            _ => throw new NotImplementedException($"The provider {key} is not implemented")
        };
    }
}