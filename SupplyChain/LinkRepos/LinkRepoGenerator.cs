using Dapper;
using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses;
using UtilClasses.CodeGeneration;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.LinkRepos
{
    class LinkRepoGenerator
    {
        private readonly LinkTable _def;

        public LinkRepoGenerator(LinkTable def)
        {
            _def = def;
        }
        public void Init(SqlConnection conn)
        {
            var cols = conn.Query<string>($@"SELECT
cu.COLUMN_NAME AS ReferencingColumn
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
where cu.TABLE_NAME = '{_def.Name}'").Select(c=>c.TrimEnd("Id")).ToList();
            if (cols.Count != 2)
                throw new NotImplementedException($"{cols.Count} is not 2.");

            
            var table = _def.Name;
            if (table.StartsWith("tbl")) table = table.Substring(3);
            var a = cols[0];
            var b = cols[1];
            var r = new KeywordReplacer("{", "}", StringComparison.OrdinalIgnoreCase)
                .Add("table", table)
                .Add("name", a)
                .Add("other", b);

            conn.Execute(r.Run(sqlTemplate));
            r.Add("name", b)
                .Add("other", a);
            conn.Execute(r.Run(sqlTemplate));
        }

        private static string sqlTemplate = @"DROP PROCEDURE IF EXISTS sp{table}_GetFor{name};
GO
CREATE PROCEDURE sp{table}_GetFor{name} (
  @id bigint
) AS
  SELECT
    {other}Id
  FROM {table}
  WHERE
    ({name}Id = @id)
GO";
    }
    class LinkRepoBuilder : SimpleClassBuilder
    {
        private readonly CodeEnvironment _env;
        private readonly IEnumerable<string> _cols;

        public LinkRepoBuilder(string name, CodeEnvironment env, IEnumerable<string> cols) : base(name)
        {
            _env = env;
            _cols = cols;
        }
        public override IEnumerable<string> Requires => new[] { _env.Dto.Namespace, "System.Collections.Generic", "UtilClasses.Db", $"def = Eco.Repo.DbDef.{Name.Substring(0, Name.Length-4)}"};
        public override IEnumerable<string> Implements => new[] { "RepoBase" };
        public override string BaseConstructorParameters => "creator";
        public override IEnumerable<string> ConstructorParameters => new []{"RepoCreator creator"};
        protected override void ClassBody(IndentingStringBuilder sb)
        {
            
        }
    }
}
/*using Eco.Dto;
using System.Collections.Generic;
using UtilClasses.Db;
using def = Eco.Repo.DbDef.HeatPumps;*/
