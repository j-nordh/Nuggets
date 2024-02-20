using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Dictionaries;

namespace ScrabblePoints
{
    public partial class Form1 : Form
    {
        private static readonly Dictionary<char, int> _swedish = new Dictionary<char, int>() {
            { 'A', 1},{'B', 4},{'C', 8},{'D', 1},{'E', 1},{'F', 3},{'G', 2},{'H', 2},
            {'I', 1},{'J', 7},{'K', 2},{'L', 1},{'M', 2},{'N', 1},{'O', 2},{'P', 4},
            {'R', 1},{'S', 1},{'T', 1},{'U', 4},{'V', 3},{'X', 8},{'Y', 7},{'Z', 1},
            {'Å', 4},{'Ä', 3},{'Ö', 4}};
        private static readonly Dictionary<char, int> _english = new Dictionary<char, int>() {
            { 'A', 1},{'B', 3},{'C', 3},{'D', 2},{'E', 1},{'F', 4},{'G', 2},{'H', 4},
            {'I', 1},{'J', 8},{'K', 5},{'L', 1},{'M', 3},{'N', 1},{'O', 1},{'P', 3},
            {'Q', 10},{'R', 1},{'S', 1},{'T', 1},{'U', 1},{'V', 4},{'W', 4},{'X', 8},
            {'Y', 4}, {'Z', 10}
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void ctxtWord_TextChanged(object sender, EventArgs e)
        {
            Dictionary<char, int> dict = null;
            if (rbSwedish.Checked) dict = _swedish;
            else if (rbEnglish.Checked) dict = _english;
            if (null == dict)
            {
                MessageBox.Show("No language selected");
                lblPoints.Text = "Error";
                return;
            }
            try
            {
                var p = ctxtWord.Text.ToUpper().Select(dict).Sum().ToString();
                lblPoints.Text = $"{ctxtWord.Text.Count()} letters\r\n{p} points";
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong in the points calculation. Usually this means that there is an unknown character in the word.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblPoints.Text = "Error";
            }

        }
    }
}
