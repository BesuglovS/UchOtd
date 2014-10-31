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
            this.chooseRings = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.FitAllLessonsDaysCount = new System.Windows.Forms.TextBox();
            this.FitAllLessonsInXDays = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LessonsLimitPerDay = new System.Windows.Forms.TextBox();
            this.LessonsLimitedPerDay = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.windowsPossibleSize = new System.Windows.Forms.TextBox();
            this.windowsPossible = new System.Windows.Forms.CheckBox();
            this.OneValue = new System.Windows.Forms.Button();
            this.wishToSetValue = new System.Windows.Forms.TextBox();
            this.ValueSelected = new System.Windows.Forms.Button();
            this.MinSelected = new System.Windows.Forms.Button();
            this.MaxSelected = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.No = new System.Windows.Forms.Button();
            this.Yes = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.teacherList = new System.Windows.Forms.ComboBox();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.wishesView = new System.Windows.Forms.DataGridView();
            this.ringsPanel = new System.Windows.Forms.Panel();
            this.RingsList = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wishesView)).BeginInit();
            this.ringsPanel.SuspendLayout();
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
            this.mainSplit.Panel1.Controls.Add(this.chooseRings);
            this.mainSplit.Panel1.Controls.Add(this.label3);
            this.mainSplit.Panel1.Controls.Add(this.FitAllLessonsDaysCount);
            this.mainSplit.Panel1.Controls.Add(this.FitAllLessonsInXDays);
            this.mainSplit.Panel1.Controls.Add(this.label2);
            this.mainSplit.Panel1.Controls.Add(this.LessonsLimitPerDay);
            this.mainSplit.Panel1.Controls.Add(this.LessonsLimitedPerDay);
            this.mainSplit.Panel1.Controls.Add(this.label1);
            this.mainSplit.Panel1.Controls.Add(this.windowsPossibleSize);
            this.mainSplit.Panel1.Controls.Add(this.windowsPossible);
            this.mainSplit.Panel1.Controls.Add(this.OneValue);
            this.mainSplit.Panel1.Controls.Add(this.wishToSetValue);
            this.mainSplit.Panel1.Controls.Add(this.ValueSelected);
            this.mainSplit.Panel1.Controls.Add(this.MinSelected);
            this.mainSplit.Panel1.Controls.Add(this.MaxSelected);
            this.mainSplit.Panel1.Controls.Add(this.Clear);
            this.mainSplit.Panel1.Controls.Add(this.No);
            this.mainSplit.Panel1.Controls.Add(this.Yes);
            this.mainSplit.Panel1.Controls.Add(this.refreshButton);
            this.mainSplit.Panel1.Controls.Add(this.teacherList);
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.Controls.Add(this.viewPanel);
            this.mainSplit.Panel2.Controls.Add(this.ringsPanel);
            this.mainSplit.Size = new System.Drawing.Size(976, 654);
            this.mainSplit.SplitterDistance = 75;
            this.mainSplit.TabIndex = 0;
            // 
            // chooseRings
            // 
            this.chooseRings.Location = new System.Drawing.Point(577, 42);
            this.chooseRings.Name = "chooseRings";
            this.chooseRings.Size = new System.Drawing.Size(174, 23);
            this.chooseRings.TabIndex = 20;
            this.chooseRings.Text = "Выбрать звонки";
            this.chooseRings.UseVisualStyleBackColor = true;
            this.chooseRings.Click += new System.EventHandler(this.chooseRings_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(540, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "дней";
            // 
            // FitAllLessonsDaysCount
            // 
            this.FitAllLessonsDaysCount.Location = new System.Drawing.Point(504, 45);
            this.FitAllLessonsDaysCount.Name = "FitAllLessonsDaysCount";
            this.FitAllLessonsDaysCount.Size = new System.Drawing.Size(34, 20);
            this.FitAllLessonsDaysCount.TabIndex = 18;
            this.FitAllLessonsDaysCount.Text = "0";
            // 
            // FitAllLessonsInXDays
            // 
            this.FitAllLessonsInXDays.AutoSize = true;
            this.FitAllLessonsInXDays.Location = new System.Drawing.Point(407, 47);
            this.FitAllLessonsInXDays.Name = "FitAllLessonsInXDays";
            this.FitAllLessonsInXDays.Size = new System.Drawing.Size(98, 17);
            this.FitAllLessonsInXDays.TabIndex = 17;
            this.FitAllLessonsInXDays.Text = "Все занятия в";
            this.FitAllLessonsInXDays.UseVisualStyleBackColor = true;
            this.FitAllLessonsInXDays.CheckStateChanged += new System.EventHandler(this.FitAllLessonsInXDays_CheckStateChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "пар в день";
            // 
            // LessonsLimitPerDay
            // 
            this.LessonsLimitPerDay.Location = new System.Drawing.Point(276, 45);
            this.LessonsLimitPerDay.Name = "LessonsLimitPerDay";
            this.LessonsLimitPerDay.Size = new System.Drawing.Size(34, 20);
            this.LessonsLimitPerDay.TabIndex = 15;
            this.LessonsLimitPerDay.Text = "0";
            // 
            // LessonsLimitedPerDay
            // 
            this.LessonsLimitedPerDay.AutoSize = true;
            this.LessonsLimitedPerDay.Location = new System.Drawing.Point(208, 47);
            this.LessonsLimitedPerDay.Name = "LessonsLimitedPerDay";
            this.LessonsLimitedPerDay.Size = new System.Drawing.Size(71, 17);
            this.LessonsLimitedPerDay.TabIndex = 14;
            this.LessonsLimitedPerDay.Text = "не более";
            this.LessonsLimitedPerDay.UseVisualStyleBackColor = true;
            this.LessonsLimitedPerDay.CheckStateChanged += new System.EventHandler(this.LessonsLimitedPerDay_CheckStateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "пар";
            // 
            // windowsPossibleSize
            // 
            this.windowsPossibleSize.Location = new System.Drawing.Point(118, 44);
            this.windowsPossibleSize.Name = "windowsPossibleSize";
            this.windowsPossibleSize.Size = new System.Drawing.Size(34, 20);
            this.windowsPossibleSize.TabIndex = 12;
            this.windowsPossibleSize.Text = "0";
            // 
            // windowsPossible
            // 
            this.windowsPossible.AutoSize = true;
            this.windowsPossible.Location = new System.Drawing.Point(12, 46);
            this.windowsPossible.Name = "windowsPossible";
            this.windowsPossible.Size = new System.Drawing.Size(107, 17);
            this.windowsPossible.TabIndex = 11;
            this.windowsPossible.Text = "возможны окна";
            this.windowsPossible.UseVisualStyleBackColor = true;
            this.windowsPossible.CheckStateChanged += new System.EventHandler(this.windowsPossible_CheckStateChanged);
            // 
            // OneValue
            // 
            this.OneValue.Location = new System.Drawing.Point(924, 12);
            this.OneValue.Name = "OneValue";
            this.OneValue.Size = new System.Drawing.Size(40, 23);
            this.OneValue.TabIndex = 10;
            this.OneValue.Text = "все";
            this.OneValue.UseVisualStyleBackColor = true;
            this.OneValue.Click += new System.EventHandler(this.OneValue_Click);
            // 
            // wishToSetValue
            // 
            this.wishToSetValue.Location = new System.Drawing.Point(840, 13);
            this.wishToSetValue.Name = "wishToSetValue";
            this.wishToSetValue.Size = new System.Drawing.Size(45, 20);
            this.wishToSetValue.TabIndex = 9;
            this.wishToSetValue.Text = "50";
            // 
            // ValueSelected
            // 
            this.ValueSelected.Location = new System.Drawing.Point(891, 13);
            this.ValueSelected.Name = "ValueSelected";
            this.ValueSelected.Size = new System.Drawing.Size(27, 23);
            this.ValueSelected.TabIndex = 8;
            this.ValueSelected.Text = "=";
            this.ValueSelected.UseVisualStyleBackColor = true;
            this.ValueSelected.Click += new System.EventHandler(this.ValueSelected_Click);
            // 
            // MinSelected
            // 
            this.MinSelected.Location = new System.Drawing.Point(807, 12);
            this.MinSelected.Name = "MinSelected";
            this.MinSelected.Size = new System.Drawing.Size(27, 23);
            this.MinSelected.TabIndex = 7;
            this.MinSelected.Text = "-";
            this.MinSelected.UseVisualStyleBackColor = true;
            this.MinSelected.Click += new System.EventHandler(this.MinSelected_Click);
            // 
            // MaxSelected
            // 
            this.MaxSelected.Location = new System.Drawing.Point(774, 12);
            this.MaxSelected.Name = "MaxSelected";
            this.MaxSelected.Size = new System.Drawing.Size(27, 23);
            this.MaxSelected.TabIndex = 6;
            this.MaxSelected.Text = "+";
            this.MaxSelected.UseVisualStyleBackColor = true;
            this.MaxSelected.Click += new System.EventHandler(this.MaxSelected_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(682, 13);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(69, 23);
            this.Clear.TabIndex = 4;
            this.Clear.Text = "Очистить";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // No
            // 
            this.No.Location = new System.Drawing.Point(620, 13);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(56, 23);
            this.No.TabIndex = 3;
            this.No.Text = "Все - 0";
            this.No.UseVisualStyleBackColor = true;
            this.No.Click += new System.EventHandler(this.No_Click);
            // 
            // Yes
            // 
            this.Yes.Location = new System.Drawing.Point(547, 13);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(67, 23);
            this.Yes.TabIndex = 2;
            this.Yes.Text = "Все - 100";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
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
            // teacherList
            // 
            this.teacherList.FormattingEnabled = true;
            this.teacherList.Location = new System.Drawing.Point(12, 15);
            this.teacherList.Name = "teacherList";
            this.teacherList.Size = new System.Drawing.Size(435, 21);
            this.teacherList.TabIndex = 0;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.wishesView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(152, 0);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(824, 575);
            this.viewPanel.TabIndex = 1;
            // 
            // wishesView
            // 
            this.wishesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wishesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wishesView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.wishesView.Location = new System.Drawing.Point(0, 0);
            this.wishesView.Name = "wishesView";
            this.wishesView.Size = new System.Drawing.Size(824, 575);
            this.wishesView.TabIndex = 1;
            this.wishesView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.wishesView_CellValueChanged);
            this.wishesView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.wishesView_KeyDown);
            // 
            // ringsPanel
            // 
            this.ringsPanel.Controls.Add(this.RingsList);
            this.ringsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ringsPanel.Location = new System.Drawing.Point(0, 0);
            this.ringsPanel.Name = "ringsPanel";
            this.ringsPanel.Size = new System.Drawing.Size(152, 575);
            this.ringsPanel.TabIndex = 0;
            // 
            // RingsList
            // 
            this.RingsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RingsList.FormattingEnabled = true;
            this.RingsList.Location = new System.Drawing.Point(0, 0);
            this.RingsList.Name = "RingsList";
            this.RingsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.RingsList.Size = new System.Drawing.Size(152, 575);
            this.RingsList.TabIndex = 0;
            this.RingsList.SelectedIndexChanged += new System.EventHandler(this.RingsList_SelectedIndexChanged);
            // 
            // Wishes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 654);
            this.Controls.Add(this.mainSplit);
            this.Name = "Wishes";
            this.Text = "Пожелания";
            this.Load += new System.EventHandler(this.Wishes_Load);
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel1.PerformLayout();
            this.mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wishesView)).EndInit();
            this.ringsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplit;
        private System.Windows.Forms.ComboBox teacherList;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.Button No;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button MinSelected;
        private System.Windows.Forms.Button MaxSelected;
        private System.Windows.Forms.TextBox wishToSetValue;
        private System.Windows.Forms.Button ValueSelected;
        private System.Windows.Forms.Button OneValue;
        private System.Windows.Forms.CheckBox windowsPossible;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox windowsPossibleSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FitAllLessonsDaysCount;
        private System.Windows.Forms.CheckBox FitAllLessonsInXDays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LessonsLimitPerDay;
        private System.Windows.Forms.CheckBox LessonsLimitedPerDay;
        private System.Windows.Forms.Button chooseRings;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView wishesView;
        private System.Windows.Forms.Panel ringsPanel;
        private System.Windows.Forms.ListBox RingsList;
    }
}