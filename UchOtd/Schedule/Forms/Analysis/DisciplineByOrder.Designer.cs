namespace UchOtd.Schedule.Forms.Analysis
{
    partial class DisciplineByOrder
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
            this.discsView = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // discsView
            // 
            this.discsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discsView.FormattingEnabled = true;
            this.discsView.Location = new System.Drawing.Point(0, 0);
            this.discsView.Name = "discsView";
            this.discsView.Size = new System.Drawing.Size(542, 825);
            this.discsView.TabIndex = 0;
            this.discsView.DragDrop += new System.Windows.Forms.DragEventHandler(this.discsView_DragDrop);
            this.discsView.DragOver += new System.Windows.Forms.DragEventHandler(this.discsView_DragOver);
            this.discsView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.discsView_MouseDown);
            // 
            // DisciplineByOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 825);
            this.Controls.Add(this.discsView);
            this.Name = "DisciplineByOrder";
            this.Text = "Порядок постановки дисциплин";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DisciplineByOrder_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox discsView;
    }
}