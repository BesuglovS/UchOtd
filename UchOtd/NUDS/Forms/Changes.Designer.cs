using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.NUDS.Forms
{
    partial class Changes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.allChanges = new System.Windows.Forms.Button();
            this.todaysChanges = new System.Windows.Forms.Button();
            this.tomorrowsChanges = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupList = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.changesView = new System.Windows.Forms.DataGridView();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.changesView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.datePicker);
            this.controlsPanel.Controls.Add(this.allChanges);
            this.controlsPanel.Controls.Add(this.todaysChanges);
            this.controlsPanel.Controls.Add(this.tomorrowsChanges);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Controls.Add(this.groupList);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(441, 72);
            this.controlsPanel.TabIndex = 2;
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(216, 12);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(159, 20);
            this.datePicker.TabIndex = 8;
            this.datePicker.Value = new System.DateTime(2013, 10, 1, 0, 0, 0, 0);
            this.datePicker.ValueChanged += new System.EventHandler(this.DatePickerValueChanged);
            // 
            // allChanges
            // 
            this.allChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.allChanges.Location = new System.Drawing.Point(381, 12);
            this.allChanges.Name = "allChanges";
            this.allChanges.Size = new System.Drawing.Size(51, 49);
            this.allChanges.TabIndex = 7;
            this.allChanges.Text = "все";
            this.allChanges.UseVisualStyleBackColor = true;
            this.allChanges.Click += new System.EventHandler(this.AllChangesClick);
            // 
            // todaysChanges
            // 
            this.todaysChanges.Location = new System.Drawing.Point(216, 38);
            this.todaysChanges.Name = "todaysChanges";
            this.todaysChanges.Size = new System.Drawing.Size(80, 23);
            this.todaysChanges.TabIndex = 6;
            this.todaysChanges.Text = "на сегодня";
            this.todaysChanges.UseVisualStyleBackColor = true;
            this.todaysChanges.Click += new System.EventHandler(this.TodaysChangesClick);
            // 
            // tomorrowsChanges
            // 
            this.tomorrowsChanges.Location = new System.Drawing.Point(302, 38);
            this.tomorrowsChanges.Name = "tomorrowsChanges";
            this.tomorrowsChanges.Size = new System.Drawing.Size(73, 23);
            this.tomorrowsChanges.TabIndex = 5;
            this.tomorrowsChanges.Text = "на завтра";
            this.tomorrowsChanges.UseVisualStyleBackColor = true;
            this.tomorrowsChanges.Click += new System.EventHandler(this.TomorrowsChangesClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Группа";
            // 
            // groupList
            // 
            this.groupList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupList.FormattingEnabled = true;
            this.groupList.Location = new System.Drawing.Point(60, 23);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(150, 21);
            this.groupList.TabIndex = 3;
            this.groupList.SelectedValueChanged += new System.EventHandler(this.GroupListSelectedValueChanged);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.loadingLabel);
            this.viewPanel.Controls.Add(this.changesView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 72);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(441, 343);
            this.viewPanel.TabIndex = 4;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadingLabel.Location = new System.Drawing.Point(31, 19);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(376, 74);
            this.loadingLabel.TabIndex = 2;
            this.loadingLabel.Text = "Загрузка ...";
            this.loadingLabel.Visible = false;
            // 
            // changesView
            // 
            this.changesView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.changesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.changesView.ColumnHeadersVisible = false;
            this.changesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changesView.Location = new System.Drawing.Point(0, 0);
            this.changesView.Name = "changesView";
            this.changesView.ReadOnly = true;
            this.changesView.RowHeadersVisible = false;
            this.changesView.Size = new System.Drawing.Size(441, 343);
            this.changesView.TabIndex = 0;
            this.changesView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ChangesViewCellFormatting);
            this.changesView.SelectionChanged += new System.EventHandler(this.ChangesViewSelectionChanged);
            // 
            // Changes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 415);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "Changes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменения расписания";
            this.Load += new System.EventHandler(this.ChangesLoad);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            this.viewPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.changesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel controlsPanel;
        private DateTimePicker datePicker;
        private Button allChanges;
        private Button todaysChanges;
        private Button tomorrowsChanges;
        private Label label1;
        private ComboBox groupList;
        private Panel viewPanel;
        private DataGridView changesView;
        private Label loadingLabel;
    }
}