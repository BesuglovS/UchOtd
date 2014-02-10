namespace UchOtd.Forms.Notes
{
    partial class Notes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notes));
            this.contrulsPanel = new System.Windows.Forms.Panel();
            this.currentDateTime = new System.Windows.Forms.CheckBox();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.TargetComputer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.noteMoment = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.noteText = new System.Windows.Forms.TextBox();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.showAll = new System.Windows.Forms.Button();
            this.filter = new System.Windows.Forms.TextBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.view = new System.Windows.Forms.DataGridView();
            this.contrulsPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // contrulsPanel
            // 
            this.contrulsPanel.Controls.Add(this.currentDateTime);
            this.contrulsPanel.Controls.Add(this.remove);
            this.contrulsPanel.Controls.Add(this.update);
            this.contrulsPanel.Controls.Add(this.add);
            this.contrulsPanel.Controls.Add(this.TargetComputer);
            this.contrulsPanel.Controls.Add(this.label3);
            this.contrulsPanel.Controls.Add(this.noteMoment);
            this.contrulsPanel.Controls.Add(this.label2);
            this.contrulsPanel.Controls.Add(this.label1);
            this.contrulsPanel.Controls.Add(this.noteText);
            this.contrulsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.contrulsPanel.Location = new System.Drawing.Point(0, 0);
            this.contrulsPanel.Name = "contrulsPanel";
            this.contrulsPanel.Size = new System.Drawing.Size(261, 448);
            this.contrulsPanel.TabIndex = 0;
            // 
            // currentDateTime
            // 
            this.currentDateTime.AutoSize = true;
            this.currentDateTime.Location = new System.Drawing.Point(96, 194);
            this.currentDateTime.Name = "currentDateTime";
            this.currentDateTime.Size = new System.Drawing.Size(141, 17);
            this.currentDateTime.TabIndex = 9;
            this.currentDateTime.Text = "Текущие дата + время";
            this.currentDateTime.UseVisualStyleBackColor = true;
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(15, 248);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 8;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(15, 219);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 7;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 190);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 6;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // TargetComputer
            // 
            this.TargetComputer.Location = new System.Drawing.Point(15, 414);
            this.TargetComputer.Name = "TargetComputer";
            this.TargetComputer.Size = new System.Drawing.Size(231, 20);
            this.TargetComputer.TabIndex = 5;
            this.TargetComputer.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 398);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Целевой компьютер";
            this.label3.Visible = false;
            // 
            // noteMoment
            // 
            this.noteMoment.CustomFormat = "dd.MM.yyyy H:mm";
            this.noteMoment.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.noteMoment.Location = new System.Drawing.Point(12, 162);
            this.noteMoment.Name = "noteMoment";
            this.noteMoment.Size = new System.Drawing.Size(234, 20);
            this.noteMoment.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата + время";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Текст";
            // 
            // noteText
            // 
            this.noteText.Location = new System.Drawing.Point(12, 25);
            this.noteText.Multiline = true;
            this.noteText.Name = "noteText";
            this.noteText.Size = new System.Drawing.Size(234, 109);
            this.noteText.TabIndex = 0;
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.label4);
            this.searchPanel.Controls.Add(this.showAll);
            this.searchPanel.Controls.Add(this.filter);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(261, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(554, 46);
            this.searchPanel.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Фильтр";
            // 
            // showAll
            // 
            this.showAll.Location = new System.Drawing.Point(455, 10);
            this.showAll.Name = "showAll";
            this.showAll.Size = new System.Drawing.Size(87, 23);
            this.showAll.TabIndex = 2;
            this.showAll.Text = "Показать все";
            this.showAll.UseVisualStyleBackColor = true;
            this.showAll.Click += new System.EventHandler(this.showAll_Click);
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(59, 12);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(390, 20);
            this.filter.TabIndex = 0;
            this.filter.TextChanged += new System.EventHandler(this.filter_TextChanged);
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.view);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(261, 46);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(554, 402);
            this.viewPanel.TabIndex = 2;
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            this.view.AllowUserToResizeColumns = false;
            this.view.AllowUserToResizeRows = false;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(0, 0);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.RowHeadersVisible = false;
            this.view.Size = new System.Drawing.Size(554, 402);
            this.view.TabIndex = 0;
            this.view.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.view_CellClick);
            // 
            // Notes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 448);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.contrulsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Notes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заметки";
            this.Load += new System.EventHandler(this.Notes_Load);
            this.contrulsPanel.ResumeLayout(false);
            this.contrulsPanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel contrulsPanel;
        private System.Windows.Forms.DateTimePicker noteMoment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox noteText;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TargetComputer;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.CheckBox currentDateTime;
        private System.Windows.Forms.Button showAll;
        private System.Windows.Forms.TextBox filter;
        private System.Windows.Forms.Label label4;


    }
}