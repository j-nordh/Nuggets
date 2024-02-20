using System;
using System.Collections.Generic;
using System.Linq;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto
{
    //public class Aggregates
    //{
    //    public List<Aggregate> All { get; set; }
    //    public IEnumerable<Aggregate> Normal => All.Where(a => a.Type == AggregateTypes.Normal);
    //    
    //    public IEnumerable<ReverseAggregate> Reversed => All.Where(a => a.Type == AggregateTypes.Reverse).Cast<ReverseAggregate>().NotNull();
    //    public Aggregates()
    //    {
    //        All= new List<Aggregate>();
    //    }

    //    public Aggregates(IEnumerable<Aggregate> aggs)
    //    {
    //        All = new List<Aggregate>(aggs);
    //    }
    //    public bool Any() => All.Any();
    //    public bool Any(Func<Aggregate, bool> predicate) => All.Any(predicate);
    //    public bool Any(ColumnProperties col) => Reversed.Any(a=>a.Column.EqualsOic(col.Name));
    //    public bool Any(AggregateTypes t) => All.Any(a => a.Type == t);
    //    public bool Any(AggregateTypes t, Func<Aggregate, bool> predicate) => All.Any(a => a.Type == t && predicate(a));
    //    public Aggregates Add(Aggregate linkedAggregate)
    //    {
    //        All.Add(linkedAggregate);
    //        return this;
    //    }

    //    public Aggregates AddRange(IEnumerable<Aggregate> items)
    //    {
    //        All.AddRange(items);
    //        return this;
    //    }
    //}

    public static class AggregateListExtensions
    {
        public static IEnumerable<Aggregate> Normal(this List<Aggregate> lst) => lst.Matching(AggregateTypes.Normal); 
        public static IEnumerable<ReverseAggregate> Reversed(this List<Aggregate> lst) => lst.Matching(AggregateTypes.Reverse).Cast<ReverseAggregate>().NotNull();
        public static bool Any(this List<Aggregate> lst, ColumnProperties col) => lst.Reversed().Any(a => a.ForeignKeyColumn.EqualsOic(col.Name));
        public static IEnumerable<LinkedAggregate> Linked(this List<Aggregate> lst) => lst.Matching(AggregateTypes.Linked).Cast<LinkedAggregate>().NotNull();
        public static IEnumerable<Aggregate> Matching(this List<Aggregate> lst, params AggregateTypes[] ts) =>
            lst.Where(a => ts.Any(t=>(t & a.Type)>0));
    }
}