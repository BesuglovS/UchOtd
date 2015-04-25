using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.Analysis
{
    partial class Shifts
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
            this.shiftsPanel = new System.Windows.Forms.Panel();
            this.shiftsViewPanel = new System.Windows.Forms.Panel();
            this.shiftsListBox = new System.Windows.Forms.ListBox();
            this.shiftsControlsPanel = new System.Windows.Forms.Panel();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.shiftName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.shiftRingsPanel = new System.Windows.Forms.Panel();
            this.shiftRingsControlsPanel = new System.Windows.Forms.Panel();
            this.AddShiftRing = new System.Windows.Forms.Button();
            this.RemoveShiftRing = new System.Windows.Forms.Button();
            this.allShiftRingsPanel = new System.Windows.Forms.Panel();
            this.allShiftRingsViewPanel = new System.Windows.Forms.Panel();
            this.AllRingsListBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.thisShiftRingsPanel = new System.Windows.Forms.Panel();
            this.thisShiftRingsViewPanel = new System.Windows.Forms.Panel();
            this.ShiftRingsListBox = new System.Windows.Forms.ListBox();
            this.thisShiftRingsLabelPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.shiftsPanel.SuspendLayout();
            this.shiftsViewPanel.SuspendLayout();
            this.shiftsControlsPanel.SuspendLayout();
            this.shiftRingsPanel.SuspendLayout();
            this.shiftRingsControlsPanel.SuspendLayout();
            this.allShiftRingsPanel.SuspendLayout();
            this.allShiftRingsViewPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.thisShiftRingsPanel.SuspendLayout();
            this.thisShiftRingsViewPanel.SuspendLayout();
            this.thisShiftRingsLabelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // shiftsPanel
            // 
            this.shiftsPanel.Controls.Add(this.shiftsViewPanel);
            this.shiftsPanel.Controls.Add(this.shiftsControlsPanel);
            this.shiftsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.shiftsPanel.Location = new System.Drawing.Point(0, 0);
            this.shiftsPanel.Name = "shiftsPanel";
            this.shiftsPanel.Size = new System.Drawing.Size(250, 394);
            this.shiftsPanel.TabIndex = 0;
            // 
            // shiftsViewPanel
            // 
            this.shiftsViewPanel.Controls.Add(this.shiftsListBox);
            this.shiftsViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shiftsViewPanel.Location = new System.Drawing.Point(0, 123);
            this.shiftsViewPanel.Name = "shiftsViewPanel";
            this.shiftsViewPanel.Size = new System.Drawing.Size(250, 271);
            this.shiftsViewPanel.TabIndex = 1;
            // 
            // shiftsListBox
            // 
            this.shiftsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shiftsListBox.FormattingEnabled = true;
            this.shiftsListBox.Location = new System.Drawing.Point(0, 0);
            this.shiftsListBox.Name = "shiftsListBox";
            this.shiftsListBox.Size = new System.Drawing.Size(250, 271);
            this.shiftsListBox.TabIndex = 0;
            this.shiftsListBox.SelectedIndexChanged += new System.EventHandler(this.shiftsListBox_SelectedIndexChanged);
            // 
            // shiftsControlsPanel
            // 
            this.shiftsControlsPanel.Controls.Add(this.remove);
            this.shiftsControlsPanel.Controls.Add(this.update);
            this.shiftsControlsPanel.Controls.Add(this.add);
            this.shiftsControlsPanel.Controls.Add(this.shiftName);
            this.shiftsControlsPanel.Controls.Add(this.label1);
            this.shiftsControlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.shiftsControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.shiftsControlsPanel.Name = "shiftsControlsPanel";
            this.shiftsControlsPanel.Size = new System.Drawing.Size(250, 123);
            this.shiftsControlsPanel.TabIndex = 0;
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(169, 70);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(68, 23);
            this.remove.TabIndex = 44;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(92, 70);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(71, 23);
            this.update.TabIndex = 43;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(15, 70);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(71, 23);
            this.add.TabIndex = 42;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // shiftName
            // 
            this.shiftName.Location = new System.Drawing.Point(15, 35);
            this.shiftName.Name = "shiftName";
            this.shiftName.Size = new System.Drawing.Size(222, 20);
            this.shiftName.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Название смены";
            // 
            // shiftRingsPanel
            // 
            this.shiftRingsPanel.Controls.Add(this.shiftRingsControlsPanel);
            this.shiftRingsPanel.Controls.Add(this.allShiftRingsPanel);
            this.shiftRingsPanel.Controls.Add(this.thisShiftRingsPanel);
            this.shiftRingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shiftRingsPanel.Location = new System.Drawing.Point(250, 0);
            this.shiftRingsPanel.Name = "shiftRingsPanel";
            this.shiftRingsPanel.Size = new System.Drawing.Size(272, 394);
            this.shiftRingsPanel.TabIndex = 1;
            // 
            // shiftRingsControlsPanel
            // 
            this.shiftRingsControlsPanel.Controls.Add(this.AddShiftRing);
            this.shiftRingsControlsPanel.Controls.Add(this.RemoveShiftRing);
            this.shiftRingsControlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shiftRingsControlsPanel.Location = new System.Drawing.Point(100, 0);
            this.shiftRingsControlsPanel.Name = "shiftRingsControlsPanel";
            this.shiftRingsControlsPanel.Size = new System.Drawing.Size(72, 394);
            this.shiftRingsControlsPanel.TabIndex = 2;
            // 
            // AddShiftRing
            // 
            this.AddShiftRing.Location = new System.Drawing.Point(16, 156);
            this.AddShiftRing.Name = "AddShiftRing";
            this.AddShiftRing.Size = new System.Drawing.Size(40, 36);
            this.AddShiftRing.TabIndex = 3;
            this.AddShiftRing.Text = "<<";
            this.AddShiftRing.UseVisualStyleBackColor = true;
            this.AddShiftRing.Click += new System.EventHandler(this.AddShiftRing_Click);
            // 
            // RemoveShiftRing
            // 
            this.RemoveShiftRing.Location = new System.Drawing.Point(16, 198);
            this.RemoveShiftRing.Name = "RemoveShiftRing";
            this.RemoveShiftRing.Size = new System.Drawing.Size(40, 36);
            this.RemoveShiftRing.TabIndex = 2;
            this.RemoveShiftRing.Text = ">>";
            this.RemoveShiftRing.UseVisualStyleBackColor = true;
            this.RemoveShiftRing.Click += new System.EventHandler(this.RemoveShiftRing_Click);
            // 
            // allShiftRingsPanel
            // 
            this.allShiftRingsPanel.Controls.Add(this.allShiftRingsViewPanel);
            this.allShiftRingsPanel.Controls.Add(this.panel1);
            this.allShiftRingsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.allShiftRingsPanel.Location = new System.Drawing.Point(172, 0);
            this.allShiftRingsPanel.Name = "allShiftRingsPanel";
            this.allShiftRingsPanel.Size = new System.Drawing.Size(100, 394);
            this.allShiftRingsPanel.TabIndex = 1;
            // 
            // allShiftRingsViewPanel
            // 
            this.allShiftRingsViewPanel.Controls.Add(this.AllRingsListBox);
            this.allShiftRingsViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allShiftRingsViewPanel.Location = new System.Drawing.Point(0, 69);
            this.allShiftRingsViewPanel.Name = "allShiftRingsViewPanel";
            this.allShiftRingsViewPanel.Size = new System.Drawing.Size(100, 325);
            this.allShiftRingsViewPanel.TabIndex = 2;
            // 
            // AllRingsListBox
            // 
            this.AllRingsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllRingsListBox.FormattingEnabled = true;
            this.AllRingsListBox.Location = new System.Drawing.Point(0, 0);
            this.AllRingsListBox.Name = "AllRingsListBox";
            this.AllRingsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.AllRingsListBox.Size = new System.Drawing.Size(100, 325);
            this.AllRingsListBox.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 69);
            this.panel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(2, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Остальные";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(19, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "звонки";
            // 
            // thisShiftRingsPanel
            // 
            this.thisShiftRingsPanel.Controls.Add(this.thisShiftRingsViewPanel);
            this.thisShiftRingsPanel.Controls.Add(this.thisShiftRingsLabelPanel);
            this.thisShiftRingsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.thisShiftRingsPanel.Location = new System.Drawing.Point(0, 0);
            this.thisShiftRingsPanel.Name = "thisShiftRingsPanel";
            this.thisShiftRingsPanel.Size = new System.Drawing.Size(100, 394);
            this.thisShiftRingsPanel.TabIndex = 0;
            // 
            // thisShiftRingsViewPanel
            // 
            this.thisShiftRingsViewPanel.Controls.Add(this.ShiftRingsListBox);
            this.thisShiftRingsViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thisShiftRingsViewPanel.Location = new System.Drawing.Point(0, 69);
            this.thisShiftRingsViewPanel.Name = "thisShiftRingsViewPanel";
            this.thisShiftRingsViewPanel.Size = new System.Drawing.Size(100, 325);
            this.thisShiftRingsViewPanel.TabIndex = 1;
            // 
            // ShiftRingsListBox
            // 
            this.ShiftRingsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShiftRingsListBox.FormattingEnabled = true;
            this.ShiftRingsListBox.Location = new System.Drawing.Point(0, 0);
            this.ShiftRingsListBox.Name = "ShiftRingsListBox";
            this.ShiftRingsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ShiftRingsListBox.Size = new System.Drawing.Size(100, 325);
            this.ShiftRingsListBox.TabIndex = 0;
            // 
            // thisShiftRingsLabelPanel
            // 
            this.thisShiftRingsLabelPanel.Controls.Add(this.label3);
            this.thisShiftRingsLabelPanel.Controls.Add(this.label2);
            this.thisShiftRingsLabelPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.thisShiftRingsLabelPanel.Location = new System.Drawing.Point(0, 0);
            this.thisShiftRingsLabelPanel.Name = "thisShiftRingsLabelPanel";
            this.thisShiftRingsLabelPanel.Size = new System.Drawing.Size(100, 69);
            this.thisShiftRingsLabelPanel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(11, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 26);
            this.label3.TabIndex = 1;
            this.label3.Text = "смены";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(8, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "Звонки";
            // 
            // Shifts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 394);
            this.Controls.Add(this.shiftRingsPanel);
            this.Controls.Add(this.shiftsPanel);
            this.Name = "Shifts";
            this.Text = "Смены";
            this.Load += new System.EventHandler(this.Shifts_Load);
            this.shiftsPanel.ResumeLayout(false);
            this.shiftsViewPanel.ResumeLayout(false);
            this.shiftsControlsPanel.ResumeLayout(false);
            this.shiftsControlsPanel.PerformLayout();
            this.shiftRingsPanel.ResumeLayout(false);
            this.shiftRingsControlsPanel.ResumeLayout(false);
            this.allShiftRingsPanel.ResumeLayout(false);
            this.allShiftRingsViewPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.thisShiftRingsPanel.ResumeLayout(false);
            this.thisShiftRingsViewPanel.ResumeLayout(false);
            this.thisShiftRingsLabelPanel.ResumeLayout(false);
            this.thisShiftRingsLabelPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel shiftsPanel;
        private Panel shiftsViewPanel;
        private ListBox shiftsListBox;
        private Panel shiftsControlsPanel;
        private Button remove;
        private Button update;
        private Button add;
        private TextBox shiftName;
        private Label label1;
        private Panel shiftRingsPanel;
        private Panel shiftRingsControlsPanel;
        private Panel allShiftRingsPanel;
        private Panel thisShiftRingsPanel;
        private Panel panel1;
        private Label label4;
        private Label label5;
        private Panel thisShiftRingsLabelPanel;
        private Label label3;
        private Label label2;
        private Button AddShiftRing;
        private Button RemoveShiftRing;
        private Panel allShiftRingsViewPanel;
        private Panel thisShiftRingsViewPanel;
        private ListBox AllRingsListBox;
        private ListBox ShiftRingsListBox;
    }
}