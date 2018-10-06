namespace UchOtd.Forms
{
    partial class ChooseRingAndAud
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.audsList = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ringsList = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 602);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 54);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(118, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(326, 54);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // OK
            // 
            this.OK.Dock = System.Windows.Forms.DockStyle.Left;
            this.OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OK.Location = new System.Drawing.Point(0, 0);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(118, 54);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 602);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.audsList);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(156, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(288, 602);
            this.panel4.TabIndex = 2;
            // 
            // audsList
            // 
            this.audsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.audsList.FormattingEnabled = true;
            this.audsList.ItemHeight = 25;
            this.audsList.Location = new System.Drawing.Point(0, 0);
            this.audsList.Name = "audsList";
            this.audsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.audsList.Size = new System.Drawing.Size(288, 602);
            this.audsList.TabIndex = 2;
            this.audsList.DoubleClick += new System.EventHandler(this.audsList_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ringsList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(156, 602);
            this.panel3.TabIndex = 1;
            // 
            // ringsList
            // 
            this.ringsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ringsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ringsList.FormattingEnabled = true;
            this.ringsList.ItemHeight = 25;
            this.ringsList.Location = new System.Drawing.Point(0, 0);
            this.ringsList.Name = "ringsList";
            this.ringsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ringsList.Size = new System.Drawing.Size(156, 602);
            this.ringsList.TabIndex = 1;
            this.ringsList.SelectedValueChanged += new System.EventHandler(this.ringsList_SelectedValueChanged);
            // 
            // ChooseRingAndAud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 656);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ChooseRingAndAud";
            this.Text = "Выберите время";
            this.Load += new System.EventHandler(this.ChooseRing_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox audsList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox ringsList;
    }
}