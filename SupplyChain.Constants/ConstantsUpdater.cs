using Newtonsoft.Json;
using SupplyChain.Dto;
using SupplyChain.Procs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Constants
{
    public class ConstantsUpdater
    {
        private readonly CodeEnvironment _env;

        public ConstantsUpdater(CodeEnvironment env)
        {
            _env = env;
        }

        private SqlConnection GetConn(string dbName)=> new SqlConnection($"Server=localhost;Database={dbName};Trusted_Connection=True;");

        public void Update(bool force)
        {
            if (_env.Db.DbDefinition.IsNotNullOrEmpty() && File.Exists(_env.Db.DbDefinition))
            {
                var def = JsonConvert.DeserializeObject<DbDef>(File.ReadAllText(_env.Db.DbDefinition));
                if (def?.Enumerations?.IsNullOrEmpty()??true) return;
                Console.WriteLine("Updating database constants");
                var conn = GetConn(_env.Db.Name);
                foreach (var e in def.Enumerations)
                {
                    Console.Write($"  {e.Name} ");
                    var res = new EnumDefQueryEnumBuilder(e, _env).UpdateIfNeeded(conn, force);
                    Console.WriteLine(res ? "Done." : "Unchanged.");

                }
               
            }
        }
    }
}
