using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto
{
    public class LinkTable
    {
        public string Name;
        public Link A { get; set; }
        public Link B { get; set; }

        public LinkTable()
        {
            A = new Link();
            B = new Link();
        }

        public LinkTable(LinkTable lt)
        {
            A = new Link(lt.A);
            B = new Link(lt.B);
            Name = lt.Name;
        }

        public PopulatedLinkTable Populate(TableNode n) =>
            new PopulatedLinkTable(this)
            {
                A = A.Populate(n),
                B = B.Populate(n), 
               
                Columns = n.Columns
            };
    }

    public class Link
    {
        public string Table { get; set; }
        public string Field { get; set; }

        public Link(){}

        public Link(Link l)
        {
            Table = l.Table;
            Field = l.Field;
        }

        public PopulatedLink Populate(TableNode parent)
        {
            var n = parent.Children.First(c => c.Name.EqualsOic(Table));
            return new PopulatedLink(this) {Columns = n.Columns};
        }
    }

    public class PopulatedLink : Link
    {
        public List<ColumnProperties> Columns { get; set; }
        public PopulatedLink() { }

        public PopulatedLink(Link l): base(l)
        {
            Columns = new List<ColumnProperties>();
        }
    }

    public class PopulatedLinkTable : LinkTable
    {
        public PopulatedLink PopA =>A as PopulatedLink;
        public PopulatedLink PopB => B as PopulatedLink;

        public List<ColumnProperties> Columns { get; set; }

        public PopulatedLinkTable()
        {
        }

        public PopulatedLinkTable(LinkTable lt):base(lt)
        {
        }

    }
}
