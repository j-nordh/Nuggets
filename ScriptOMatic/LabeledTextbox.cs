using System;
using System.Windows.Forms;

namespace ScriptOMatic
{
    public partial class LabeledTextbox : UserControl
    {
        private string _caption;
        public LabeledTextbox()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                lbl.Text = _caption;
            }
        }

        private void LabeledTextbox_Load(object sender, EventArgs e)
        {

        }

        public override string Text
        {
            get { return txt.Text; }
            set
            {
                txt.Text = value;
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }
    }
}
