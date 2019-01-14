namespace UchOtd.Forms
{
    partial class AuditoriumCollisionsLog
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
            this.label1 = new System.Windows.Forms.Label();
            this.BuildingsListbox = new System.Windows.Forms.ListBox();
            this.DowListbox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.WeeksTextbox = new System.Windows.Forms.TextBox();
            this.Run = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Корпуса";
            // 
            // BuildingsListbox
            // 
            this.BuildingsListbox.FormattingEnabled = true;
            this.BuildingsListbox.Location = new System.Drawing.Point(12, 22);
            this.BuildingsListbox.Name = "BuildingsListbox";
            this.BuildingsListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.BuildingsListbox.Size = new System.Drawing.Size(239, 186);
            this.BuildingsListbox.TabIndex = 1;
            // 
            // DowListbox
            // 
            this.DowListbox.FormattingEnabled = true;
            this.DowListbox.Location = new System.Drawing.Point(259, 22);
            this.DowListbox.Name = "DowListbox";
            this.DowListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.DowListbox.Size = new System.Drawing.Size(239, 186);
            this.DowListbox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дни недели";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Недели";
            // 
            // WeeksTextbox
            // 
            this.WeeksTextbox.Location = new System.Drawing.Point(12, 227);
            this.WeeksTextbox.Name = "WeeksTextbox";
            this.WeeksTextbox.Size = new System.Drawing.Size(486, 20);
            this.WeeksTextbox.TabIndex = 5;
            // 
            // Run
            // 
            this.Run.Font = new System.Drawing.Font("Microsoft Sans Serif", 52F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Run.Location = new System.Drawing.Point(12, 253);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(486, 109);
            this.Run.TabIndex = 6;
            this.Run.Text = "ПРОВЕРИТЬ";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 373);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(504, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(67, 17);
            this.StatusLabel.Text = "StatusLabel";
            // 
            // AuditoriumCollisionsLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 395);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.WeeksTextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DowListbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BuildingsListbox);
            this.Controls.Add(this.label1);
            this.Name = "AuditoriumCollisionsLog";
            this.Text = "Проверить коллизии аудиторий";
            this.Load += new System.EventHandler(this.AuditoriumCollisionsLog_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox BuildingsListbox;
        private System.Windows.Forms.ListBox DowListbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox WeeksTextbox;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    }
}