using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Yaml;
using FarsiLibrary.Win;
using FastColoredTextBoxNS;

namespace DC.RegexWorkshop
{
    public partial class FrmMain : Form
    {

        #region Fields

        private int _nosaved = 1;

        Style invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);

        private int _matchesCount = 0;

        #endregion

        public FrmMain(string[] args)
        {
            InitializeComponent();
            
            if (args==null) return;

            foreach (var f in args)
            {
                CreateTab(f);
            }

        }

        public FrmMain()
            : this(null)
        {
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        private void CreateTab(string filename)
        {
            try
            {
                var tb = new RegexTesterTab();
                tb.Dock = DockStyle.Fill;
                tb.RegexText.Focus();

                var tab = new FATabStripItem(filename != null ? Path.GetFileName(filename) : string.Format("new {0}", _nosaved++), tb);
                tab.Tag = filename;
                tab.Click += TabOnClick;
                
                if (filename != null)
                {

                    var yaml = File.ReadAllText(filename);
                    var doc = YamlNode.FromYaml(yaml);
                    var root = (YamlMapping)doc[0];
                    var sregex = ((YamlScalar)root["regex"]).Value;
                    var stext = ((YamlScalar)root["text"]).Value;
                    tb.RegexText.Text = sregex;
                    tb.TesterText.Text = stext;
                }

                tabMain.AddTab(tab);
                tabMain.SelectedItem = tab;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("The file {0} is invalid.", filename), "ERROR");
            }

        }

        private void TabOnClick(object sender, EventArgs eventArgs)
        {
            var hola = eventArgs;
        }

        private void TabOnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var hola = mouseEventArgs.Button;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private bool Save(FATabStripItem tab)
        {
            var tb = (tab.Controls[0] as RegexTesterTab);

            YamlNode pnode =
                new YamlMapping(
                        "regex", new YamlScalar(tb.RegexText.Text),
                        "text", new YamlScalar(tb.TesterText.Text)
                    );
            
            if (tab.Tag == null)
            {
                if (sfdMain.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                tab.Title = Path.GetFileName(sfdMain.FileName);
                tab.Tag = sfdMain.FileName;

                if (!RecentFiles.Contains(sfdMain.FileName))
                    RecentFiles.Add(sfdMain.FileName);
            }

            try
            {
                File.WriteAllText(tab.Tag as string, pnode.ToYaml());
                tb.RegexText.IsChanged = false;
                tb.TesterText.IsChanged = false;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    return Save(tab);
                else
                    return false;
            }

            tb.Invalidate();

            return true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //CreateTab(null);
            LoadConfig();
            RefreshRecentFiles();
        }

        private void RefreshRecentFiles()
        {
            foreach (var filename in RecentFiles)
            {
                var name = Path.GetFileName(filename);
                var toolTipItem = recentFilesToolStripMenuItem.DropDown.Items.Add(name);
                toolTipItem.Tag = filename;
                toolTipItem.ToolTipText = filename;
                toolTipItem.Click += RecetFileToolTipItemOnClick;
            }
            
        }

        private void RecetFileToolTipItemOnClick(object sender, EventArgs eventArgs)
        {
            var file = ((ToolStripItem)sender).Tag.ToString();
            CreateTab(file);
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            if (ofdMain.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!RecentFiles.Contains(ofdMain.FileName))
                    RecentFiles.Add(ofdMain.FileName);

                CreateTab(ofdMain.FileName);
            }
                
        }

        private void SaveFile()
        {
            if (tabMain.SelectedItem != null)
            {
                Save(tabMain.SelectedItem);
            }
                
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            CurrentTB.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            CurrentTB.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            CurrentTB.Paste();
        }


        private void tmUpdateInterface_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentTB != null && tabMain.Items.Count > 0)
                {
                    var tb = CurrentTB;
                    undoStripButton.Enabled = undoStripButton.Enabled = tb.UndoEnabled;
                    redoStripButton.Enabled = redoStripButton.Enabled = tb.RedoEnabled;
                    saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = tb.IsChanged;
                    saveAsToolStripMenuItem.Enabled = true;
                    pasteToolStripButton.Enabled = pasteToolStripButton.Enabled = true;
                    cutToolStripButton.Enabled = cutToolStripButton.Enabled =
                    copyToolStripButton.Enabled = copyToolStripButton.Enabled = tb.Selection.Start != tb.Selection.End;
                    //printToolStripButton.Enabled = true;
                }
                else
                {
                    saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = false;
                    saveAsToolStripMenuItem.Enabled = false;
                    cutToolStripButton.Enabled = cutToolStripButton.Enabled =
                    copyToolStripButton.Enabled = copyToolStripButton.Enabled = false;
                    pasteToolStripButton.Enabled = pasteToolStripButton.Enabled = false;
                    //printToolStripButton.Enabled = false;
                    undoStripButton.Enabled = undoStripButton.Enabled = false;
                    redoStripButton.Enabled = redoStripButton.Enabled = false;
                    
                }

                lbl_matches.Text = string.Format(" Matches: {0}", _matchesCount);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void redoStripButton_Click(object sender, EventArgs e)
        {
            
            if (CurrentTB.RedoEnabled)
                CurrentTB.Redo();
        }

        private void undoStripButton_Click(object sender, EventArgs e)
        {
            if (CurrentTB.UndoEnabled)
                CurrentTB.Undo();
        }

        FastColoredTextBox CurrentTB
        {
            get
            {
                if (tabMain.SelectedItem == null)
                    return null;

                var tb = (tabMain.SelectedItem.Controls[0] as RegexTesterTab);

                if (tb.RegexText.Focused)
                    return tb.RegexText;

                return tb.TesterText;
            }
            /*
            set
            {
                tabMain.SelectedItem = (value.Parent as FATabStripItem);
                value.Focus();
            }
            */
        }

        FastColoredTextBox CurrentRegex
        {
            get
            {
                if (tabMain.SelectedItem == null)
                    return null;

                var tb = (tabMain.SelectedItem.Controls[0] as RegexTesterTab);
                
                return tb.RegexText;
            }
        }

        FastColoredTextBox CurrentTester
        {
            get
            {
                if (tabMain.SelectedItem == null)
                    return null;

                var tb = (tabMain.SelectedItem.Controls[0] as RegexTesterTab);

                return tb.TesterText;
            }
        }

        Style regextMatchStyle = new TextStyle(Brushes.Black, Brushes.Yellow, FontStyle.Regular);

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentTester.Range.ClearStyle(regextMatchStyle);

                var pattern = string.IsNullOrEmpty(CurrentRegex.SelectedText) ? CurrentRegex.Text : CurrentRegex.SelectedText;

                var opts = RegexOptions.None;

                if (ingnoreCase.Checked)
                {
                    opts = RegexOptions.IgnoreCase;
                }


                var ranges = CurrentTester.GetRanges(pattern, opts);
                _matchesCount = ranges.Count();

                foreach (Range range in ranges)
                {
                    range.SetStyle(regextMatchStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
            

        }

        private void btInvisibleChars_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tabMain.Items)
            {
                
                var ctl = (tab.Controls[0] as RegexTesterTab);
                HighlightInvisibleChars(ctl.RegexText.Range);
                HighlightInvisibleChars(ctl.TesterText.Range);
                
            }
                
            if (CurrentTB != null)
                CurrentTB.Invalidate();
        }

        private void HighlightInvisibleChars(Range range)
        {
            range.ClearStyle(invisibleCharsStyle);
            if (btInvisibleChars.Checked)
                range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
        }

        private List<string> _recentFiles;
        private List<string> RecentFiles
        {
            get
            {
                if (_recentFiles == null)
                {
                    _recentFiles = new List<string>();
                }
                return _recentFiles;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        private void SaveConfig()
        {
            var sec = new YamlSequence();
            
            foreach (var rf in RecentFiles)
            {
                sec.Add(new YamlScalar(rf));
            }

            YamlNode pnode =
                new YamlMapping(
                        "recentfiles", sec
                    );

            var path = Path.Combine(GetExecPath(), "cache.yml");

            pnode.ToYamlFile(path);

        }

        private void LoadConfig()
        {
            var path = Path.Combine(GetExecPath(), "cache.yml");

            if (!File.Exists(path)) return;

            var doc = YamlNode.FromYamlFile(path);
            var root = (YamlMapping)doc[0];
            var rfiles = (YamlSequence)root["recentfiles"];

            foreach (YamlScalar rfile in rfiles)
            {
                if (!RecentFiles.Contains(rfile.Value))
                    RecentFiles.Add(rfile.Value);
            }

        }

        private string GetExecPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void copyStringFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pattern = string.IsNullOrEmpty(CurrentRegex.SelectedText) ? CurrentRegex.Text : CurrentRegex.SelectedText;

            Clipboard.SetData(DataFormats.Text, (Object)ConvertToStringText(pattern));
        }

        private static string ConvertToStringText(string pattern)
        {
            return pattern.Replace(@"\", @"\\").Replace("\"", "\\\"");
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTester.Range.ClearStyle(regextMatchStyle);
        }

        private void extractMatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentTester.Range.ClearStyle(regextMatchStyle);

                var pattern = string.IsNullOrEmpty(CurrentRegex.SelectedText) ? CurrentRegex.Text : CurrentRegex.SelectedText;

                var opts = RegexOptions.None;

                if (ingnoreCase.Checked)
                {
                    opts = RegexOptions.IgnoreCase;
                }

                var ranges = CurrentTester.GetRanges(pattern, opts);
                _matchesCount = ranges.Count();

                var matches = new List<string>();

                foreach (Range range in ranges)
                {
                    if (matches.Contains(range.Text)) continue;

                    matches.Add(range.Text);
                }

                Clipboard.SetText(
                    string.Join("\n", matches.ToArray()),
                TextDataFormat.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

    }
    

}
