using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using UtilClasses.Extensions.Dictionaries;

namespace SupplyChain.Dto
{
    public class DtoInfo:ICloneable<DtoInfo>
    {
        public bool Properties { get; set; }
        public bool HasWriteId { get; set; }
        public bool Stateful { get; set; }
        public bool Cloneable { get; set; }
        public List<InterfaceDef> Implements { get; set; }
        public int? DecimalPlaces { get; set; }
        public bool MatchNormalizeLineBreak { get; set; }
        public bool MatchIgnoreCase { get; set; }
        public bool MatchIgnoreWhitespace { get; set; }

        public Dictionary<string, JsonField> JsonFields { get; set; }
        public DtoInfo() { }

        public DtoInfo(DtoInfo o)
        {
            Properties = o.Properties;
            HasWriteId = o.HasWriteId;
            Stateful = o.Stateful;
            Cloneable = o.Cloneable;
            Implements = o?.Implements?.ToList();
            JsonFields = o.JsonFields.Clone();
            DecimalPlaces = o.DecimalPlaces;
            MatchIgnoreCase = o.MatchIgnoreCase;
            MatchIgnoreWhitespace = o.MatchIgnoreWhitespace;
            MatchNormalizeLineBreak = o.MatchNormalizeLineBreak;
        }
        public virtual DtoInfo  Clone() => new DtoInfo(this);
    }

    public class PopulatedDtoInfo: DtoInfo
    {
        public string Namespace { get; set; }

        public PopulatedDtoInfo() : base(){}
        public PopulatedDtoInfo(DtoInfo o) : base(o) { }

        public PopulatedDtoInfo(PopulatedDtoInfo o) : base(o)
        {
            Namespace = o.Namespace;
        }

        public PopulatedDtoInfo Populate(DtoSettings s)
        {
            Namespace = s.Namespace;
            return this;
        }
    }


}