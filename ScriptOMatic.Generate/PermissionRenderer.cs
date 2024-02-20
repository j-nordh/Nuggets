using System;
using System.Collections.Generic;
using SupplyChain.Dto;
using UtilClasses;

namespace ScriptOMatic.Generate
{
    class PermissionRenderer:Renderer<PermDef>
    {
        
        readonly string _table;
        readonly Dictionary<string, string> _templates;

        public PermissionRenderer(string table, Dictionary<string,string> templates) 
        {
            _templates = templates;
            _table = table;
        }

        protected override IEnumerable<string> SimpleRender(PermDef p)
        {
            if (p.Create) yield return RenderUpsert(p, "C");
            if (p.Read) yield return RenderUpsert(p, "R");
            if (p.Update) yield return RenderUpsert(p, "U");
            if (p.Delete) yield return RenderUpsert(p, "D");
        }

        protected override void CombineTo(List<IAppendable> parts, IndentingStringBuilder sb)
        {
            sb.AppendObjects(parts, "\r\nGO\r\n");
        }

        protected override string SimpleSetup() => $@"CREATE PROCEDURE UpsertPermission(@tag nvarchar(20), @desc nvarchar(200))
AS
  IF EXISTS (SELECT tag from tblPermissions WHERE tag=@tag)
    UPDATE {_table}
    SET Description = @desc
    WHERE Tag = @Tag
  ELSE
    INSERT INTO {_table} (Tag, Description)
    VALUES (@tag, @desc);
GO";

        protected override string SimpleTeardown() =>"DROP PROCEDURE UpsertPermission";

        private string RenderUpsert(PermDef p, string suffix)=>$"exec UpsertPermission @tag='{p.TagBase}{suffix}', @desc='{string.Format(_templates[suffix], p.DescBase)}'";
    }
}
