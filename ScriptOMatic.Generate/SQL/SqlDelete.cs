using System.Collections.Generic;
using UtilClasses;

namespace ScriptOMatic.Generate.SQL
{
    public class SqlDelete : IAppendable
    {
        public string _name;
        private readonly IEnumerable<string> _keyColumns;
        private readonly bool _inputJson;

        public SqlDelete(string name, IEnumerable<string> keyColumns, bool inputJson)
        {
            _name = name;
            _keyColumns = keyColumns;
            _inputJson = inputJson;
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            if (_inputJson)
                return sb.AppendLines(
                    "declare @sql nvarchar(max)  + CHAR(10)",
                    "SET @sql = 'declare @count bigint' + CHAR(10)",
                    "SET @sql = @sql + (SELECT 'set @count = @count + exec spWeatherForecastValues_DelForWeatherForecasts @id ' + ");
            return sb.AppendObject(DeleteStatement(true));
        }
        private IAppendable DeleteStatement(bool withParameter) => new FuncAppendable(sb=> sb
            .AppendLine($"DELETE FROM {_name} WHERE")
            .Indent(() => sb.AppendObjects(_keyColumns, c => $"{c} = {(withParameter?"@":"")}{c}", " AND")));
        /*declare @sql nvarchar(max)
  SET @sql = 'declare @count bigint' + CHAR(10)
  SET @sql = @sql + (SELECT 'set @count = @count + exec spWeatherForecastValues_DelForWeatherForecasts @id ' + 
    '@Id = ' +convert(nvarchar(15),Id)+ CHAR(10)
  FROM OPENJSON(@json)
    WITH (
      Id bigint '$.Id'
    ) FOR XML PATH('')
  ) + char(10)
  set @sql =  @sql + 'select @count as COUNT'
  exec sp_executesql @sql*/
    }
}