using System;

namespace SupplyChain.Dto
{
    [Flags]
    public enum AggregateTypes
    {
        None=0,
        Normal=1,
        Linked=2,
        Reverse=4,
        All=7
    }
}