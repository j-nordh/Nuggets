using SupplyChain.Procs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyChain.Dto
{
    public class DbDef
    {
        //public List<ClassDef> Procedures { get; set; }
        public List<Bundle> Bundles { get; set; }
        public List<EnumDef> Enumerations { get; set; }
        public List<LinkTable> LinkTables { get; set; }

        public DbDef()
        {
            Bundles=new List<Bundle>();
            Enumerations= new List<EnumDef>();
            LinkTables = new List<LinkTable>();
        }
    }
}
