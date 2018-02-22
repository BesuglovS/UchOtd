using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.DBLists
{
    partial class ConfigOptionsList
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
            this.ListPanel = new System.Windows.Forms.Panel();
            this.OptionsListView = new System.Windows.Forms.DataGridView();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.optionKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.optionValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.showInternalOptions = new System.Windows.Forms.CheckBox();
            this.ListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsListView)).BeginInit();
            this.SuspendLayout();
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.OptionsListView);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ListPanel.Location = new System.Drawing.Point(288, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(274, 451);
            this.ListPanel.TabIndex = 9;
            // 
            // OptionsListView
            // 
            this.OptionsListView.AllowUserToAddRows = false;
            this.OptionsListView.AllowUserToDeleteRows = false;
            this.OptionsListView.AllowUserToResizeColumns = false;
            this.OptionsListView.AllowUserToResizeRows = false;
            this.OptionsListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OptionsListView.ColumnHeadersVisible = false;
            this.OptionsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionsListView.Location = new System.Drawing.Point(0, 0);
            this.OptionsListView.Name = "OptionsListView";
            this.OptionsListView.ReadOnly = true;
            this.OptionsListView.RowHeadersVisible = false;
            this.OptionsListView.Size = new System.Drawing.Size(274, 451);
            this.OptionsListView.TabIndex = 0;
            this.OptionsListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OptionsListView_CellClick);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(174, 130);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 14;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(93, 130);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 13;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(12, 130);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 12;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // optionKey
            // 
            this.optionKey.Location = new System.Drawing.Point(12, 34);
            this.optionKey.Name = "optionKey";
            this.optionKey.Size = new System.Drawing.Size(237, 20);
            this.optionKey.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Key";
            // 
            // optionValue
            // 
            this.optionValue.Location = new System.Drawing.Point(12, 91);
            this.optionValue.Name = "optionValue";
            this.optionValue.Size = new System.Drawing.Size(237, 20);
            this.optionValue.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Value";
            // 
            // showInternalOptions
            // 
            this.showInternalOptions.AutoSize = true;
            this.showInternalOptions.Location = new System.Drawing.Point(14, 166);
            this.showInternalOptions.Name = "showInternalOptions";
            this.showInternalOptions.Size = new System.Drawing.Size(232, 17);
            this.showInternalOptions.TabIndex = 17;
            this.showInternalOptions.Text = "Показывать скрытые/системные опции";
            this.showInternalOptions.UseVisualStyleBackColor = true;
            this.showInternalOptions.CheckedChanged += new System.EventHandler(this.showInternalOptions_CheckedChanged);
            // 
            // ConfigOptionsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 451);
            this.Controls.Add(this.showInternalOptions);
            this.Controls.Add(this.optionValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.update);
            this.Controls.Add(this.add);
            this.Controls.Add(this.optionKey);
            this.Controls.Add(this.label1);
            this.Name = "ConfigOptionsList";
            this.Text = "Опции чёрт побери";
            this.Load += new System.EventHandler(this.ConfigOptionsLoad);
            this.ListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptionsListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel ListPanel;
        private DataGridView OptionsListView;
        private Button remove;
        private Button update;
        private Button add;
        private TextBox optionKey;
        private Label label1;
        private TextBox optionValue;
        private Label label2;
        private CheckBox showInternalOptions;
    }
}