namespace UchOtd.Schedule.Forms.Analysis
{
    partial class FreeScheduleSpot
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
            this.groupList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.spotDatePicker = new System.Windows.Forms.DateTimePicker();
            this.ringsList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.output = new System.Windows.Forms.RichTextBox();
            this.Go = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Группа";
            // 
            // groupList
            // 
            this.groupList.FormattingEnabled = true;
            this.groupList.Location = new System.Drawing.Point(60, 6);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(130, 21);
            this.groupList.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата";
            // 
            // spotDatePicker
            // 
            this.spotDatePicker.Location = new System.Drawing.Point(60, 38);
            this.spotDatePicker.Name = "spotDatePicker";
            this.spotDatePicker.Size = new System.Drawing.Size(130, 20);
            this.spotDatePicker.TabIndex = 3;
            // 
            // ringsList
            // 
            this.ringsList.FormattingEnabled = true;
            this.ringsList.Location = new System.Drawing.Point(60, 73);
            this.ringsList.Name = "ringsList";
            this.ringsList.Size = new System.Drawing.Size(130, 21);
            this.ringsList.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Время";
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(196, 6);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(275, 159);
            this.output.TabIndex = 6;
            this.output.Text = "";
            // 
            // Go
            // 
            this.Go.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Go.Location = new System.Drawing.Point(15, 100);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(175, 65);
            this.Go.TabIndex = 7;
            this.Go.Text = "GO";
            this.Go.UseVisualStyleBackColor = true;
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // FreeScheduleSpot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 175);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.output);
            this.Controls.Add(this.ringsList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.spotDatePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupList);
            this.Controls.Add(this.label1);
            this.Name = "FreeScheduleSpot";
            this.Text = "Освободить место в расписании";
            this.Load += new System.EventHandler(this.FreeScheduleSpot_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox groupList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker spotDatePicker;
        private System.Windows.Forms.ComboBox ringsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.Button Go;
    }
}