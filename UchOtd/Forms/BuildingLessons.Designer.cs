namespace UchOtd.Forms
{
    partial class BuildingLessons
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildingLessons));
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.lessonsDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.building = new System.Windows.Forms.ComboBox();
            this.label175 = new System.Windows.Forms.Label();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.auditoriumsView = new System.Windows.Forms.DataGridView();
            this.showProposed = new System.Windows.Forms.CheckBox();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.auditoriumsView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.showProposed);
            this.controlsPanel.Controls.Add(this.lessonsDate);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Controls.Add(this.building);
            this.controlsPanel.Controls.Add(this.label175);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(875, 50);
            this.controlsPanel.TabIndex = 0;
            // 
            // lessonsDate
            // 
            this.lessonsDate.Location = new System.Drawing.Point(290, 10);
            this.lessonsDate.Name = "lessonsDate";
            this.lessonsDate.Size = new System.Drawing.Size(200, 20);
            this.lessonsDate.TabIndex = 3;
            this.lessonsDate.Value = new System.DateTime(1985, 4, 4, 0, 0, 0, 0);
            this.lessonsDate.ValueChanged += new System.EventHandler(this.DateValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дата";
            // 
            // building
            // 
            this.building.FormattingEnabled = true;
            this.building.Location = new System.Drawing.Point(61, 9);
            this.building.Name = "building";
            this.building.Size = new System.Drawing.Size(184, 21);
            this.building.TabIndex = 1;
            this.building.SelectedIndexChanged += new System.EventHandler(this.BuildingSelectedIndexChanged);
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Location = new System.Drawing.Point(12, 12);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(43, 13);
            this.label175.TabIndex = 0;
            this.label175.Text = "Корпус";
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.loadingLabel);
            this.viewPanel.Controls.Add(this.auditoriumsView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 50);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(875, 494);
            this.viewPanel.TabIndex = 1;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadingLabel.Location = new System.Drawing.Point(12, 3);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(376, 74);
            this.loadingLabel.TabIndex = 3;
            this.loadingLabel.Text = "Загрузка ...";
            this.loadingLabel.Visible = false;
            // 
            // auditoriumsView
            // 
            this.auditoriumsView.AllowUserToAddRows = false;
            this.auditoriumsView.AllowUserToDeleteRows = false;
            this.auditoriumsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.auditoriumsView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.auditoriumsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.auditoriumsView.ColumnHeadersVisible = false;
            this.auditoriumsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.auditoriumsView.Location = new System.Drawing.Point(0, 0);
            this.auditoriumsView.Name = "auditoriumsView";
            this.auditoriumsView.ReadOnly = true;
            this.auditoriumsView.RowHeadersVisible = false;
            this.auditoriumsView.Size = new System.Drawing.Size(875, 494);
            this.auditoriumsView.TabIndex = 0;
            // 
            // showProposed
            // 
            this.showProposed.AutoSize = true;
            this.showProposed.Location = new System.Drawing.Point(496, 11);
            this.showProposed.Name = "showProposed";
            this.showProposed.Size = new System.Drawing.Size(217, 17);
            this.showProposed.TabIndex = 4;
            this.showProposed.Text = "Показывать преполагаемые занятия";
            this.showProposed.UseVisualStyleBackColor = true;
            // 
            // BuildingLessons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 544);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BuildingLessons";
            this.Text = "Занятость аудиторий";
            this.Load += new System.EventHandler(this.BuildingLessonsLoad);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            this.viewPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.auditoriumsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.DateTimePicker lessonsDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox building;
        private System.Windows.Forms.Label label175;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView auditoriumsView;
        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.CheckBox showProposed;
    }
}