using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Dto;
using UtilClasses;

namespace SupplyChain.Procs
{
    class AccessorGenerator : IAppendable
    {
        private readonly ClassDef _def;

        public AccessorGenerator(ClassDef def)
        {
            _def = def;
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            if (!_def.IsCrudDef()) return sb;
            var t = _def.CrudType();
            sb.AppendLines($"public class Accessor : ICrudAccessor<{t}>",
            "{",
            "private readonly DbWrapper _db;",
            "public Accessor (DbWrapper db)",
            "{",
            "_db = db;",
            "}",
            $"public {t} Create({t} obj) => _db.QueryOne({_def.ClassName}.Create(obj));",
            $"public void Delete(long id) => _db.ExecuteNonQuery({_def.ClassName}.Delete(id));",
            $"public {t} Get(long id) => _db.QueryOne({_def.ClassName}.Get(id));",
            $"public List<{t}> Get() => _db.Query({_def.ClassName}.Get());",
            $"public void Update({t} obj) => _db.ExecuteNonQuery({_def.ClassName}.Update(obj));",
            "}");

            return sb;
        }
    }
}
