using System.Collections.Generic;
using System.Linq;
using SupplyChain.Procs;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto
{
    //public class CrudSpsInfo
    //{
    //    public string NameBase;
    //    public string TableName;
    //    public List<ColumnProperties> Columns;
    //    public SpInfo Create;
    //    public SpInfo Read;
    //    public SpInfo ReadIdIn;
    //    public List<ReadWithCol> ReadBetweens;
    //    public List<ReadWithCol> ReadFors;
    //    public List<ReadWithCol> ReadForMaxs;
    //    public List<ReadWithCol> DeleteFor;
    //    public SpInfo Update;
    //    public SpInfo Delete;
    //    public SpInfoWith<UpsertMode> Upsert;

    //    public TableNode RootNode;
    //    public string Typename;
    //    public List<SubQuery> SubQueries;
    //    public List<Aggregate> Aggregates;
    //    public bool DropUnused;
    //    public RepoInfo RepoInfo;
    //    public List<ProcDef> HandCoded;
    //    public Dictionary<string, string> EnumFields { get; set; }
    //    public bool IsCrud => Create.ShouldCreate() && Read.ShouldCreate() && Update.ShouldCreate() && Delete.ShouldCreate() && Columns.Any(c=>c.Name.EqualsOic("Id"));
    //}
}
