using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace DC.RegexWorkshop
{
    public partial class RegexTesterTab : UserControl
    {
        
        public RegexTesterTab()
        {
            InitializeComponent();
        }

        public FastColoredTextBox RegexText
        {
            get { return this.txt_regex; }
        }

        public FastColoredTextBox TesterText
        {
            get { return this.txt_text; }
        }

        private void RegexTesterTab_Load(object sender, EventArgs e)
        {
            txt_regex.AddStyle(new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray))));//same words style
        }

        private void txt_regex_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
        }

        private void txt_text_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
        }

    }
}
