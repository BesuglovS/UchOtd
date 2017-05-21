namespace UchOtd.Schedule.Forms.Merge
{
    partial class MergeStudents
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
            this.studentList1 = new System.Windows.Forms.ComboBox();
            this.studentList2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Go = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Запись в которую происходит объединение";
            // 
            // studentList1
            // 
            this.studentList1.FormattingEnabled = true;
            this.studentList1.Location = new System.Drawing.Point(311, 20);
            this.studentList1.Name = "studentList1";
            this.studentList1.Size = new System.Drawing.Size(353, 21);
            this.studentList1.TabIndex = 1;
            // 
            // studentList2
            // 
            this.studentList2.FormattingEnabled = true;
            this.studentList2.Location = new System.Drawing.Point(311, 57);
            this.studentList2.Name = "studentList2";
            this.studentList2.Size = new System.Drawing.Size(353, 21);
            this.studentList2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(293, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Запись которая объединяется с первой и уничтожается";
            // 
            // Go
            // 
            this.Go.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Go.Location = new System.Drawing.Point(16, 93);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(648, 92);
            this.Go.TabIndex = 4;
            this.Go.Text = "Merge";
            this.Go.UseVisualStyleBackColor = true;
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // MergeStudents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 196);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.studentList2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.studentList1);
            this.Controls.Add(this.label1);
            this.Name = "MergeStudents";
            this.Text = "Объединить 2 записи студента";
            this.Load += new System.EventHandler(this.MergeStudents_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox studentList1;
        private System.Windows.Forms.ComboBox studentList2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Go;
    }
}