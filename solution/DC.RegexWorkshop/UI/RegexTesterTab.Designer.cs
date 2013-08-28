namespace DC.RegexWorkshop
{
    partial class RegexTesterTab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txt_regex = new FastColoredTextBoxNS.FastColoredTextBox();
            this.txt_text = new FastColoredTextBoxNS.FastColoredTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txt_regex);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txt_text);
            this.splitContainer1.Size = new System.Drawing.Size(682, 592);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 0;
            // 
            // txt_regex
            // 
            this.txt_regex.BackBrush = null;
            this.txt_regex.CommentPrefix = "#";
            this.txt_regex.CurrentLineColor = System.Drawing.Color.LightSteelBlue;
            this.txt_regex.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_regex.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txt_regex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_regex.LeftBracket = '(';
            this.txt_regex.Location = new System.Drawing.Point(0, 0);
            this.txt_regex.Name = "txt_regex";
            this.txt_regex.Paddings = new System.Windows.Forms.Padding(0);
            this.txt_regex.RightBracket = ')';
            this.txt_regex.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txt_regex.ShowScrollBars = false;
            this.txt_regex.Size = new System.Drawing.Size(682, 110);
            this.txt_regex.TabIndex = 2;
            this.txt_regex.WordWrap = true;
            this.txt_regex.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txt_regex_TextChangedDelayed);
            // 
            // txt_text
            // 
            this.txt_text.BackBrush = null;
            this.txt_text.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_text.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txt_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_text.Location = new System.Drawing.Point(0, 0);
            this.txt_text.Name = "txt_text";
            this.txt_text.Paddings = new System.Windows.Forms.Padding(0);
            this.txt_text.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txt_text.ShowScrollBars = false;
            this.txt_text.Size = new System.Drawing.Size(682, 478);
            this.txt_text.TabIndex = 3;
            this.txt_text.WordWrap = true;
            this.txt_text.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txt_text_TextChangedDelayed);
            // 
            // RegexTesterTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "RegexTesterTab";
            this.Size = new System.Drawing.Size(682, 592);
            this.Load += new System.EventHandler(this.RegexTesterTab_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox txt_regex;
        private FastColoredTextBoxNS.FastColoredTextBox txt_text;

    }
}
