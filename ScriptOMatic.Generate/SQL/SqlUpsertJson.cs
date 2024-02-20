using System.Collections.Generic;
using System.Linq;
using ScriptOMatic.Pages;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Generate.SQL
{
    public class SqlUpsertJson : IAppendable
    {
        private readonly string _name;
        private readonly IEnumerable<ColumnProperties> _columns;
        private readonly ColumnRenderer _cr;

        public SqlUpsertJson(string name, IEnumerable<ColumnProperties> columns, ColumnRenderer cr)
        {
            _name = name;
            _columns = columns;
            _cr = cr;
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            string Escape(ColumnProperties c)
            {
                if (!c.GetDtoType().EqualsOic("string"))
                    return _cr.ConvertToNVarChar(c);
                return $"REPLACE('' + {c.Name} + '', '''''''', '''''''''''')";
            }

            string SetVar(ColumnProperties c)
            {
                var str = Escape(c);
                var quotes = c.IsString()
                    ? "+ '''' "
                    : "";
                return $"'set @{c.Name} =' {quotes}+ {str} {quotes}+CHAR(10)";
            }

            var cols = _columns.Select(c => $"{c.Name} {c.FullType}").Join(",\r\n\t");
            var setters = new IndentingStringBuilder("  ", 2)
                .AppendObjects(_columns, c => $"'@{c.Name} = ' +" + _cr.ConvertToNVarChar(c), "+ ', ' +").ToString();

            var kr = KeywordReplacer
                .Create("CommandLength", "max")
                .Add("SingleSP", _name)
                .Add("SetParams", _columns.Select(c => $"'@{c.Name} = ' + {_cr.ConvertToNVarChar(c)}").Join(" + ',' + CHAR(10) + \n")+ " + CHAR(10)")
                .Add("JsonTags", _columns, _cr.JsonDeclaration, ",\r\n\t\t")
                .Add("Columns", cols)
                .Add("Declare",
                    _columns.Select(c => $"'declare @{c.ParameterName} {c.FullType}'")
                        .Join(" + CHAR(10)\nset @sql = @sql +"))
                .Add("ColumnNames", _columns.Select(c => c.Name), x => x, ", ");
            var generate = kr.Run("drop table if exists #tmp\n" + GenerateTemplate + "\nprint @sql");
            return sb.AppendLine(kr.Run(GenerateTemplate + ReturnTemplate));
        }

        private const string GenerateTemplate = @"
DROP TABLE IF EXISTS #tmp
CREATE TABLE #tmp (cmd nvarchar(%CommandLength%))
insert into #tmp(cmd)
SELECT 'exec %SingleSP% ' + CHAR(10) +
%SetParams%

FROM OPENJSON(@json)
WITH(
    %JsonTags%
)

declare @sql nvarchar(max)
set @sql = (select cmd from #tmp FOR XML PATH(''))
set @sql = replace(@sql, '<cmd>', '')
set @sql = replace(@sql, '</cmd>', '')";

        private const string ReturnTemplate = @"

CREATE TABLE #res 
(
    %Columns%
)
INSERT INTO #res
    exec sp_executesql @sql

SELECT 
    %ColumnNames% 
FROM #res
DROP TABLE #tmp
DROP TABLE #res";
    }
}