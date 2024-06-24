namespace colorenhancementfuzzylogicpso
{
    partial class Form1
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
            this.browseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enhanceColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveResultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlSebelum = new System.Windows.Forms.Panel();
            this.pbsebelumnya = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSesudah = new System.Windows.Forms.Panel();
            this.pbsesudah = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.pnlSebelum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbsebelumnya)).BeginInit();
            this.pnlSesudah.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbsesudah)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseToolStripMenuItem,
            this.enhanceColorToolStripMenuItem,
            this.saveResultToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(869, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // browseToolStripMenuItem
            // 
            this.browseToolStripMenuItem.Name = "browseToolStripMenuItem";
            this.browseToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.browseToolStripMenuItem.Text = "Browse";
            this.browseToolStripMenuItem.Click += new System.EventHandler(this.browseToolStripMenuItem_Click);
            // 
            // enhanceColorToolStripMenuItem
            // 
            this.enhanceColorToolStripMenuItem.Name = "enhanceColorToolStripMenuItem";
            this.enhanceColorToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.enhanceColorToolStripMenuItem.Text = "Enhance Color";
            this.enhanceColorToolStripMenuItem.Click += new System.EventHandler(this.enhanceColorToolStripMenuItem_Click);
            // 
            // saveResultToolStripMenuItem
            // 
            this.saveResultToolStripMenuItem.Name = "saveResultToolStripMenuItem";
            this.saveResultToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.saveResultToolStripMenuItem.Text = "Save Result";
            this.saveResultToolStripMenuItem.Click += new System.EventHandler(this.saveResultToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pnlSebelum
            // 
            this.pnlSebelum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSebelum.Controls.Add(this.pbsebelumnya);
            this.pnlSebelum.Location = new System.Drawing.Point(31, 84);
            this.pnlSebelum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlSebelum.Name = "pnlSebelum";
            this.pnlSebelum.Size = new System.Drawing.Size(368, 419);
            this.pnlSebelum.TabIndex = 1;
            // 
            // pbsebelumnya
            // 
            this.pbsebelumnya.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbsebelumnya.Location = new System.Drawing.Point(0, 0);
            this.pbsebelumnya.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbsebelumnya.Name = "pbsebelumnya";
            this.pbsebelumnya.Size = new System.Drawing.Size(366, 417);
            this.pbsebelumnya.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbsebelumnya.TabIndex = 0;
            this.pbsebelumnya.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(296, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Before";
            // 
            // pnlSesudah
            // 
            this.pnlSesudah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSesudah.Controls.Add(this.pbsesudah);
            this.pnlSesudah.Location = new System.Drawing.Point(464, 84);
            this.pnlSesudah.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlSesudah.Name = "pnlSesudah";
            this.pnlSesudah.Size = new System.Drawing.Size(368, 419);
            this.pnlSesudah.TabIndex = 1;
            // 
            // pbsesudah
            // 
            this.pbsesudah.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbsesudah.Location = new System.Drawing.Point(0, 0);
            this.pbsesudah.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbsesudah.Name = "pbsesudah";
            this.pbsesudah.Size = new System.Drawing.Size(366, 417);
            this.pbsesudah.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbsesudah.TabIndex = 1;
            this.pbsesudah.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(730, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "After";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(869, 554);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlSesudah);
            this.Controls.Add(this.pnlSebelum);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Color Enhancement Fuzzy Logic PSO";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlSebelum.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbsebelumnya)).EndInit();
            this.pnlSesudah.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbsesudah)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem browseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enhanceColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel pnlSebelum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSesudah;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem saveResultToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pbsebelumnya;
        private System.Windows.Forms.PictureBox pbsesudah;
    }
}

