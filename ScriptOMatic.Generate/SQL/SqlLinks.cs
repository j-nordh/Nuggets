using System.Collections.Generic;
using System.Linq;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using UtilClasses;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Generate.SQL
{
    public class SqlLinks : IAppendable
    {
        private readonly List<LinkTable> _tables;

        public SqlLinks(IEnumerable<LinkTable> links)
        {
            _tables = links.ToList();
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb) => sb.AppendObjects(
            _tables.SelectMany(GetAppendables), "\r\nGO\r\n");

        private IEnumerable<IAppendable> GetAppendables(LinkTable lt) =>
            new[]
            {
                new LinkAppendable(lt, lt.A),
                new LinkAppendable(lt, lt.B)
            };

        private class LinkAppendable : IAppendable
        {
            private readonly LinkTable _l;
            private readonly Link _parent;
            private readonly Link _child;

            public LinkAppendable(LinkTable l, Link parent)
            {
                _l = l;
                _parent = parent;
                _child =_l.A.Table.EqualsOic(parent.Table)?l.B:l.A;
            }
            public IndentingStringBuilder AppendObject(IndentingStringBuilder sb) =>
                sb.AppendLines(
                    $"create or alter procedure spLink{_l.Name(_parent)}(@parentId bigint, @children nvarchar(max))",
                    "AS",
                    $"delete from {_l.Name} where {_parent.Field} = @parentId",
                    $"insert into {_l.Name}({_parent.Field}, {_child.Field})",
                    $"select @parentId {_parent.Field}, value {_child.Field}",
                    "from string_split(@children, ',')",
                    "WHERE RTRIM(value) <> ''");
        }
    }
}
