using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptOMatic
{
    public partial class Formatter : Form
    {
        public Formatter()
        {
            InitializeComponent();
        }

        private void Formatter_Load(object sender, EventArgs e)
        {
            
            var sb = new StringBuilder();
            var wd = new WordDispenser(txt1.Text);
            var keywords = new Dictionary<string, int> {["select"] = 0, ["distinct"] = 0, ["top"] = 1};
            while (wd.HasNext)
            {
                var word = wd.Next();
                switch (word.ToLower())
                {
                    case "select":
                    case "distinct":
                        sb.Append(word.ToUpper()).Append(" ");
                        break;
                    case "top":
                        break;
                }
            }
        }

        private class WordDispenser
        {
            private int _index;
            private readonly string[] _words;

            public WordDispenser(string input)
            {
                _index = -1;
                _words = input.Trim().Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }

            public bool HasNext => _index < _words.Length-1;

            public string Next()
            {
                _index += 1;
                if(_index>=_words.Length) throw new IndexOutOfRangeException();
                return _words[_index];
            }


        }
    }
}
