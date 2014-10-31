namespace UchOtd.Schedule.Forms
{
    partial class DisciplinesAuditoriums
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
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.All = new System.Windows.Forms.Button();
            this.None = new System.Windows.Forms.Button();
            this.splitPanel = new System.Windows.Forms.Panel();
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.discList = new System.Windows.Forms.ListBox();
            this.audList = new System.Windows.Forms.ListBox();
            this.controlsPanel.SuspendLayout();
            this.splitPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.Panel2.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.None);
            this.controlsPanel.Controls.Add(this.All);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(689, 50);
            this.controlsPanel.TabIndex = 1;
            // 
            // All
            // 
            this.All.Location = new System.Drawing.Point(12, 12);
            this.All.Name = "All";
            this.All.Size = new System.Drawing.Size(75, 23);
            this.All.TabIndex = 0;
            this.All.Text = "Всё";
            this.All.UseVisualStyleBackColor = true;
            this.All.Click += new System.EventHandler(this.All_Click);
            // 
            // None
            // 
            this.None.Location = new System.Drawing.Point(93, 12);
            this.None.Name = "None";
            this.None.Size = new System.Drawing.Size(75, 23);
            this.None.TabIndex = 1;
            this.None.Text = "Ничего";
            this.None.UseVisualStyleBackColor = true;
            this.None.Click += new System.EventHandler(this.None_Click);
            // 
            // splitPanel
            // 
            this.splitPanel.Controls.Add(this.MainSplit);
            this.splitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPanel.Location = new System.Drawing.Point(0, 50);
            this.splitPanel.Name = "splitPanel";
            this.splitPanel.Size = new System.Drawing.Size(689, 799);
            this.splitPanel.TabIndex = 2;
            // 
            // MainSplit
            // 
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.Location = new System.Drawing.Point(0, 0);
            this.MainSplit.Name = "MainSplit";
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.discList);
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.Controls.Add(this.audList);
            this.MainSplit.Size = new System.Drawing.Size(689, 799);
            this.MainSplit.SplitterDistance = 530;
            this.MainSplit.TabIndex = 1;
            // 
            // discList
            // 
            this.discList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discList.FormattingEnabled = true;
            this.discList.Location = new System.Drawing.Point(0, 0);
            this.discList.Name = "discList";
            this.discList.Size = new System.Drawing.Size(530, 799);
            this.discList.TabIndex = 0;
            this.discList.SelectedIndexChanged += new System.EventHandler(this.tfdList_SelectedIndexChanged);
            // 
            // audList
            // 
            this.audList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audList.FormattingEnabled = true;
            this.audList.Location = new System.Drawing.Point(0, 0);
            this.audList.Name = "audList";
            this.audList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.audList.Size = new System.Drawing.Size(155, 799);
            this.audList.TabIndex = 0;
            this.audList.SelectedIndexChanged += new System.EventHandler(this.audList_SelectedIndexChanged);
            // 
            // DisciplinesAuditoriums
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 849);
            this.Controls.Add(this.splitPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "DisciplinesAuditoriums";
            this.Text = "Аудитории дисциплин";
            this.Load += new System.EventHandler(this.DisciplinesAuditoriums_Load);
            this.controlsPanel.ResumeLayout(false);
            this.splitPanel.ResumeLayout(false);
            this.MainSplit.Panel1.ResumeLayout(false);
            this.MainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
            this.MainSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button None;
        private System.Windows.Forms.Button All;
        private System.Windows.Forms.Panel splitPanel;
        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.ListBox discList;
        private System.Windows.Forms.ListBox audList;


    }
}