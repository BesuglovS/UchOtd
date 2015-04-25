using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.Analysis
{
    partial class IncompatiblePairs
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
            this.AddPair = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.disc2 = new System.Windows.Forms.ComboBox();
            this.disc1 = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.PairsView = new System.Windows.Forms.DataGridView();
            this.removePair = new System.Windows.Forms.Button();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PairsView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.removePair);
            this.controlsPanel.Controls.Add(this.AddPair);
            this.controlsPanel.Controls.Add(this.label2);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Controls.Add(this.disc2);
            this.controlsPanel.Controls.Add(this.disc1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(849, 82);
            this.controlsPanel.TabIndex = 0;
            // 
            // AddPair
            // 
            this.AddPair.Location = new System.Drawing.Point(531, 12);
            this.AddPair.Name = "AddPair";
            this.AddPair.Size = new System.Drawing.Size(96, 48);
            this.AddPair.TabIndex = 4;
            this.AddPair.Text = "Добавить пару дисциплин";
            this.AddPair.UseVisualStyleBackColor = true;
            this.AddPair.Click += new System.EventHandler(this.AddPair_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Дисциплина 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дисциплина 1";
            // 
            // disc2
            // 
            this.disc2.FormattingEnabled = true;
            this.disc2.Location = new System.Drawing.Point(97, 39);
            this.disc2.Name = "disc2";
            this.disc2.Size = new System.Drawing.Size(428, 21);
            this.disc2.TabIndex = 1;
            // 
            // disc1
            // 
            this.disc1.FormattingEnabled = true;
            this.disc1.Location = new System.Drawing.Point(97, 12);
            this.disc1.Name = "disc1";
            this.disc1.Size = new System.Drawing.Size(428, 21);
            this.disc1.TabIndex = 0;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.PairsView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 82);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(849, 427);
            this.viewPanel.TabIndex = 1;
            // 
            // PairsView
            // 
            this.PairsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PairsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PairsView.Location = new System.Drawing.Point(0, 0);
            this.PairsView.Name = "PairsView";
            this.PairsView.Size = new System.Drawing.Size(849, 427);
            this.PairsView.TabIndex = 0;
            // 
            // removePair
            // 
            this.removePair.Location = new System.Drawing.Point(633, 12);
            this.removePair.Name = "removePair";
            this.removePair.Size = new System.Drawing.Size(96, 48);
            this.removePair.TabIndex = 5;
            this.removePair.Text = "Удалить пару дисциплин";
            this.removePair.UseVisualStyleBackColor = true;
            this.removePair.Click += new System.EventHandler(this.removePair_Click);
            // 
            // IncompatiblePairs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 509);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "IncompatiblePairs";
            this.Text = "Пары дисциплин, которые нельзя ставить в один день";
            this.Load += new System.EventHandler(this.IncompatiblePairs_Load);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PairsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel controlsPanel;
        private Button AddPair;
        private Label label2;
        private Label label1;
        private ComboBox disc2;
        private ComboBox disc1;
        private Panel viewPanel;
        private DataGridView PairsView;
        private Button removePair;
    }
}