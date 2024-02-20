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
    public partial class Launcher : Form
    {
        private static Generator _generator;
        private static CallCompare _comparer;
        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {

        }

        private void Launcher_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)
                Close();
        }

        private void btnGenerator_Click(object sender, EventArgs e)
        {
            _generator = _generator ?? new Generator();
            _generator.Show();
        }

        private void btnCallCompare_Click(object sender, EventArgs e)
        {
            _comparer = _comparer ?? new CallCompare();
            _comparer.Show();
        }
    }
}
