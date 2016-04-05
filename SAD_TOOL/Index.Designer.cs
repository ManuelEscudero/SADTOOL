namespace SAD_TOOL
{
    partial class Index
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProtectedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.challengesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importNewChallengeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteChallengeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gonfiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showXMLHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.challengesToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateToolStripMenuItem,
            this.newProjectToolStripMenuItem,
            this.closeProtectedFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.generateToolStripMenuItem.Text = "Generate protected file";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateProtectedFileToolStripMenuItem_Click);
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.newProjectToolStripMenuItem.Text = "Open protected file";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // closeProtectedFileToolStripMenuItem
            // 
            this.closeProtectedFileToolStripMenuItem.Name = "closeProtectedFileToolStripMenuItem";
            this.closeProtectedFileToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.closeProtectedFileToolStripMenuItem.Text = "Close protected file";
            this.closeProtectedFileToolStripMenuItem.Click += new System.EventHandler(this.closeProtectedFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // challengesToolStripMenuItem
            // 
            this.challengesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importNewChallengeToolStripMenuItem,
            this.deleteChallengeToolStripMenuItem});
            this.challengesToolStripMenuItem.Name = "challengesToolStripMenuItem";
            this.challengesToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.challengesToolStripMenuItem.Text = "Challenges";
            // 
            // importNewChallengeToolStripMenuItem
            // 
            this.importNewChallengeToolStripMenuItem.Name = "importNewChallengeToolStripMenuItem";
            this.importNewChallengeToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.importNewChallengeToolStripMenuItem.Text = "Import new challenge";
            this.importNewChallengeToolStripMenuItem.Click += new System.EventHandler(this.importNewChallengeToolStripMenuItem_Click);
            // 
            // deleteChallengeToolStripMenuItem
            // 
            this.deleteChallengeToolStripMenuItem.Name = "deleteChallengeToolStripMenuItem";
            this.deleteChallengeToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.deleteChallengeToolStripMenuItem.Text = "Delete challenge";
            this.deleteChallengeToolStripMenuItem.Click += new System.EventHandler(this.deleteChallengeToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gonfiToolStripMenuItem,
            this.showXMLHeaderToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // gonfiToolStripMenuItem
            // 
            this.gonfiToolStripMenuItem.Name = "gonfiToolStripMenuItem";
            this.gonfiToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.gonfiToolStripMenuItem.Text = "Help";
            // 
            // showXMLHeaderToolStripMenuItem
            // 
            this.showXMLHeaderToolStripMenuItem.Name = "showXMLHeaderToolStripMenuItem";
            this.showXMLHeaderToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.showXMLHeaderToolStripMenuItem.Text = "About";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 27);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(760, 247);
            this.textBox1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::SAD_TOOL.Properties.Resources.GF0EVI71_300x212;
            this.pictureBox1.Location = new System.Drawing.Point(480, 280);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(292, 170);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Index";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UTOPIA Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Index_FormClosing);
            this.Load += new System.EventHandler(this.Index_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem challengesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importNewChallengeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteChallengeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gonfiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showXMLHeaderToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProtectedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
    }
}

