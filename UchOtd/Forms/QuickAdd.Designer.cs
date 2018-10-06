namespace UchOtd.Forms
{
    partial class QuickAdd
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
            this.studentGroup = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.discipline = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Auditorium = new System.Windows.Forms.ComboBox();
            this.AddLesson = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ringsBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dowBox = new System.Windows.Forms.ComboBox();
            this.weekNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.weekNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Группа";
            // 
            // studentGroup
            // 
            this.studentGroup.FormattingEnabled = true;
            this.studentGroup.Location = new System.Drawing.Point(14, 72);
            this.studentGroup.Name = "studentGroup";
            this.studentGroup.Size = new System.Drawing.Size(286, 21);
            this.studentGroup.TabIndex = 3;
            this.studentGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.studentGroup_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дисциплина";
            // 
            // discipline
            // 
            this.discipline.FormattingEnabled = true;
            this.discipline.Location = new System.Drawing.Point(14, 112);
            this.discipline.Name = "discipline";
            this.discipline.Size = new System.Drawing.Size(286, 21);
            this.discipline.TabIndex = 4;
            this.discipline.TextChanged += new System.EventHandler(this.discipline_TextChanged);
            this.discipline.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.discipline_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Аудитория";
            // 
            // Auditorium
            // 
            this.Auditorium.FormattingEnabled = true;
            this.Auditorium.Location = new System.Drawing.Point(14, 152);
            this.Auditorium.Name = "Auditorium";
            this.Auditorium.Size = new System.Drawing.Size(286, 21);
            this.Auditorium.TabIndex = 5;
            this.Auditorium.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Auditorium_KeyPress);
            // 
            // AddLesson
            // 
            this.AddLesson.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddLesson.Location = new System.Drawing.Point(15, 222);
            this.AddLesson.Name = "AddLesson";
            this.AddLesson.Size = new System.Drawing.Size(285, 61);
            this.AddLesson.TabIndex = 7;
            this.AddLesson.Text = "Добавить";
            this.AddLesson.UseVisualStyleBackColor = true;
            this.AddLesson.Click += new System.EventHandler(this.AddLesson_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Неделя";
            // 
            // ringsBox
            // 
            this.ringsBox.FormattingEnabled = true;
            this.ringsBox.Location = new System.Drawing.Point(15, 194);
            this.ringsBox.Name = "ringsBox";
            this.ringsBox.Size = new System.Drawing.Size(286, 21);
            this.ringsBox.TabIndex = 66;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Время";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "День недели";
            // 
            // dowBox
            // 
            this.dowBox.FormattingEnabled = true;
            this.dowBox.Location = new System.Drawing.Point(91, 32);
            this.dowBox.Name = "dowBox";
            this.dowBox.Size = new System.Drawing.Size(209, 21);
            this.dowBox.TabIndex = 2;
            // 
            // weekNum
            // 
            this.weekNum.Location = new System.Drawing.Point(91, 7);
            this.weekNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.weekNum.Name = "weekNum";
            this.weekNum.Size = new System.Drawing.Size(209, 20);
            this.weekNum.TabIndex = 1;
            this.weekNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // QuickAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 294);
            this.Controls.Add(this.weekNum);
            this.Controls.Add(this.dowBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ringsBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AddLesson);
            this.Controls.Add(this.Auditorium);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.discipline);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.studentGroup);
            this.Controls.Add(this.label1);
            this.Name = "QuickAdd";
            this.Text = "Добавление занятий";
            this.Load += new System.EventHandler(this.QuickAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.weekNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox studentGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox discipline;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Auditorium;
        private System.Windows.Forms.Button AddLesson;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ringsBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox dowBox;
        private System.Windows.Forms.NumericUpDown weekNum;
    }
}