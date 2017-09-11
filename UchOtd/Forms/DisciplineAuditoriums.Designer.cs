namespace UchOtd.Forms
{
    partial class DisciplineAuditoriums
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
            this.semesters = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.faculties = new System.Windows.Forms.ListBox();
            this.Go = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // semesters
            // 
            this.semesters.Location = new System.Drawing.Point(14, 34);
            this.semesters.Multiline = true;
            this.semesters.Name = "semesters";
            this.semesters.Size = new System.Drawing.Size(172, 265);
            this.semesters.TabIndex = 0;
            this.semesters.Text = "13141\r\n13142\r\n14151\r\n14152\r\n15161\r\n15162\r\n16171\r\n16172\r\n17181\r\n17182";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Список семестров";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Список факультетов";
            // 
            // faculties
            // 
            this.faculties.FormattingEnabled = true;
            this.faculties.Location = new System.Drawing.Point(222, 34);
            this.faculties.Name = "faculties";
            this.faculties.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.faculties.Size = new System.Drawing.Size(263, 264);
            this.faculties.TabIndex = 3;
            // 
            // Go
            // 
            this.Go.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Go.Location = new System.Drawing.Point(14, 309);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(470, 81);
            this.Go.TabIndex = 4;
            this.Go.Text = "ПОЕХАЛИ";
            this.Go.UseVisualStyleBackColor = true;
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // DisciplineAuditoriums
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 402);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.faculties);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.semesters);
            this.Name = "DisciplineAuditoriums";
            this.Text = "DisciplineAuditoriums";
            this.Load += new System.EventHandler(this.DisciplineAuditoriums_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox semesters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox faculties;
        private System.Windows.Forms.Button Go;
    }
}