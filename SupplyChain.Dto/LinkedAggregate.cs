namespace SupplyChain.Dto
{
    public class LinkedAggregate : Aggregate
    {
        public override AggregateTypes Type => AggregateTypes.Linked;
        public LinkTable Link { get; set; }

    }
}