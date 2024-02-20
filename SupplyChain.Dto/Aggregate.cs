using System;
using System.Collections.Generic;
using System.Text;
using JsonSubTypes;
using Newtonsoft.Json;
using UtilClasses.Extensions.Objects;

namespace SupplyChain.Dto
{
    [JsonConverter(typeof(JsonSubtypes), "Type" )]
    [JsonSubtypes.KnownSubType(typeof(Aggregate), AggregateTypes.Normal)]
    [JsonSubtypes.KnownSubType(typeof(LinkedAggregate), AggregateTypes.Linked)]
    [JsonSubtypes.KnownSubType(typeof(ReverseAggregate), AggregateTypes.Reverse)]
    public class Aggregate
    {
        public virtual AggregateTypes Type => AggregateTypes.Normal;
        public string Table { get; set; }
        public string Repo { get; set; }
        public string Alias { get; set; }
        public string TargetType { get; set; }
        public string ForeignKeyColumn { get; set; }
        public string PrimaryKeyColumn { get; set; }
        public bool ReadOnly { get; set; }
        public string ShortName { get; set; }
    }
}
