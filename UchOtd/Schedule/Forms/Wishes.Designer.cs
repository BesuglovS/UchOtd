namespace UchOtd.Schedule.Forms
{
    partial class Wishes
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
            this.mainSplit = new System.Windows.Forms.SplitContainer();
            this.wishesView = new System.Windows.Forms.DataGridView();
            this.teacherList = new System.Windows.Forms.ComboBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.Yes = new System.Windows.Forms.Button();
            this.No = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wishesView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainSplit
            // 
            this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplit.Location = new System.Drawing.Point(0, 0);
            this.mainSplit.Name = "mainSplit";
            this.mainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplit.Panel1
            // 
            this.mainSplit.Panel1.Controls.Add(this.Clear);
            this.mainSplit.Panel1.Controls.Add(this.No);
            this.mainSplit.Panel1.Controls.Add(this.Yes);
            this.mainSplit.Panel1.Controls.Add(this.refreshButton);
            this.mainSplit.Panel1.Controls.Add(this.teacherList);
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.Controls.Add(this.wishesView);
            this.mainSplit.Size = new System.Drawing.Size(969, 654);
            this.mainSplit.SplitterDistance = 51;
            this.mainSplit.TabIndex = 0;
            // 
            // wishesView
            // 
            this.wishesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wishesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wishesView.Location = new System.Drawing.Point(0, 0);
            this.wishesView.Name = "wishesView";
            this.wishesView.Size = new System.Drawing.Size(969, 599);
            this.wishesView.TabIndex = 0;
            // 
            // teacherList
            // 
            this.teacherList.FormattingEnabled = true;
            this.teacherList.Location = new System.Drawing.Point(12, 15);
            this.teacherList.Name = "teacherList";
            this.teacherList.Size = new System.Drawing.Size(435, 21);
            this.teacherList.TabIndex = 0;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(453, 13);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Обновить";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // Yes
            // 
            this.Yes.Location = new System.Drawing.Point(554, 13);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(115, 23);
            this.Yes.TabIndex = 2;
            this.Yes.Text = "Всегда говори ДА";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // No
            // 
            this.No.Location = new System.Drawing.Point(675, 12);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(76, 23);
            this.No.TabIndex = 3;
            this.No.Text = "Ни за что";
            this.No.UseVisualStyleBackColor = true;
            this.No.Click += new System.EventHandler(this.No_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(757, 12);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 4;
            this.Clear.Text = "Очистить";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Wishes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 654);
            this.Controls.Add(this.mainSplit);
            this.Name = "Wishes";
            this.Text = "Пожелания";
            this.Load += new System.EventHandler(this.Wishes_Load);
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wishesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplit;
        private System.Windows.Forms.ComboBox teacherList;
        private System.Windows.Forms.DataGridView wishesView;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.Button No;
        private System.Windows.Forms.Button Clear;
    }
}