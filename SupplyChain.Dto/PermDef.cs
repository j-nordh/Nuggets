namespace SupplyChain.Dto
{
    public class PermDef
    {
        public string TagBase { get; set; }
        public string DescBase { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public override string ToString() => TagBase + " (" + (Create ? "C" : "") + (Read ? "R" : "") + (Update ? "U" : "") + (Delete ? "D" : "") + ")";
    }
    
}
