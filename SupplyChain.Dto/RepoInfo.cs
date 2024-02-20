namespace SupplyChain.Dto
{
    public class RepoInfo
    {
        public bool ImplementUpsertRepo { get; set; }
        public bool ImplementMultiUpsertRepo { get; set; }
        public bool WithFiltering{get; set; }

        public RepoInfo()
        {
        }

        public RepoInfo(RepoInfo o)
        {
            ImplementUpsertRepo = o.ImplementUpsertRepo;
            ImplementMultiUpsertRepo = o.ImplementMultiUpsertRepo;
            WithFiltering = o.WithFiltering;
        }
    }
    public class PopulatedRepoInfo : RepoInfo
    {
        public string DefNamespace { get; set; }
        public string Base { get; set; }
        public bool BaseIsGeneric { get; set; }

        public PopulatedRepoInfo(PopulatedRepoInfo o) : base(o)
        {
            DefNamespace = o.DefNamespace;
            BaseIsGeneric = o.BaseIsGeneric;
            Base = o.Base;
        }
        public PopulatedRepoInfo() { }
        public PopulatedRepoInfo(RepoInfo o) : base(o) { }

        public PopulatedRepoInfo Populate(RepoSettings rs)
        {
            DefNamespace = rs.Def.Namespace;
            Base = rs.Base;
            BaseIsGeneric = rs.BaseIsGeneric;
            return this;
        }

    }
}