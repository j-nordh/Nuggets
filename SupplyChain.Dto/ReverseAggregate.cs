namespace SupplyChain.Dto
{
    public class ReverseAggregate : Aggregate
    {
        public override AggregateTypes Type => AggregateTypes.Reverse;
        public string Column { get; set; }
    }
}