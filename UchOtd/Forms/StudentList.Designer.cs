namespace UchOtd.Forms
{
    partial class StudentList
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
            this.components = new System.ComponentModel.Container();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.searchBox = new System.Windows.Forms.ComboBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.viewGrid = new System.Windows.Forms.DataGridView();
            this.StudentListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewGrid)).BeginInit();
            this.StudentListContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.searchBox);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(654, 51);
            this.controlsPanel.TabIndex = 0;
            // 
            // searchBox
            // 
            this.searchBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.searchBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.searchBox.FormattingEnabled = true;
            this.searchBox.Location = new System.Drawing.Point(12, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(627, 21);
            this.searchBox.TabIndex = 0;
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchBoxKeyDown);
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.viewGrid);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 51);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(654, 0);
            this.contentPanel.TabIndex = 1;
            // 
            // viewGrid
            // 
            this.viewGrid.AllowUserToAddRows = false;
            this.viewGrid.AllowUserToDeleteRows = false;
            this.viewGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.viewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.viewGrid.ContextMenuStrip = this.StudentListContextMenu;
            this.viewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewGrid.Location = new System.Drawing.Point(0, 0);
            this.viewGrid.Name = "viewGrid";
            this.viewGrid.ReadOnly = true;
            this.viewGrid.RowHeadersVisible = false;
            this.viewGrid.Size = new System.Drawing.Size(654, 0);
            this.viewGrid.TabIndex = 0;
            this.viewGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.viewGrid_CellDoubleClick);
            // 
            // StudentListContextMenu
            // 
            this.StudentListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.редактироватьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.StudentListContextMenu.Name = "StudentListContextMenu";
            this.StudentListContextMenu.Size = new System.Drawing.Size(155, 70);
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // StudentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 49);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "StudentList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Контингент";
            this.Load += new System.EventHandler(this.StudentListLoad);
            this.Resize += new System.EventHandler(this.StudentListResize);
            this.controlsPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewGrid)).EndInit();
            this.StudentListContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.ComboBox searchBox;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.DataGridView viewGrid;
        private System.Windows.Forms.ContextMenuStrip StudentListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;

    }
}