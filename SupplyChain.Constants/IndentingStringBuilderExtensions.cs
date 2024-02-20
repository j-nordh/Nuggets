using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using UtilClasses;
using UtilClasses.Db.Extensions;
using UtilClasses.Extensions.Dictionaries;

namespace SupplyChain.Constants
{
    internal static class IndentingStringBuilderExtensions
    {
        public static IndentingStringBuilder Query<T>(this IndentingStringBuilder sb, SqlConnection conn,
            string query, Func<T, string> func, List<T> extras,string separator="")
        {
            return sb.AppendLines(conn.QueryDirect<T>(query).Union(extras).Select(func), separator);
        }
        public static IndentingStringBuilder QueryDict(this IndentingStringBuilder sb, SqlConnection conn,
            string query, Func<IDictionary<string,object>, string> func, List<IDictionary<string,object>> extras,string separator="")
        {
            return sb.AppendLines(conn.QueryDirect(query).Union(extras).Select(func), separator);
        }
    }
}