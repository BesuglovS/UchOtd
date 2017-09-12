using System.ComponentModel;
using System.Windows.Forms;

namespace UchOtd.Schedule.Forms.DBLists
{
    partial class DisciplineList
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
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TypeSequence = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SyncGroupName = new System.Windows.Forms.CheckBox();
            this.AuditoriumHoursPerWeek = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkForDoubleDiscsOnAdding = new System.Windows.Forms.CheckBox();
            this.reloadGroupList = new System.Windows.Forms.Button();
            this.CompletelyDelete = new System.Windows.Forms.Button();
            this.Paste = new System.Windows.Forms.Button();
            this.Attestation = new System.Windows.Forms.ComboBox();
            this.Group = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.PracticalHours = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LectureHours = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AuditoriumHours = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.DisciplineName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.DisciplinesList = new System.Windows.Forms.DataGridView();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.hoursFilteredByWeek = new System.Windows.Forms.CheckBox();
            this.orderByDisciplineName = new System.Windows.Forms.CheckBox();
            this.withoutTypeSequence = new System.Windows.Forms.CheckBox();
            this.WithLessonsToday = new System.Windows.Forms.CheckBox();
            this.noPost = new System.Windows.Forms.CheckBox();
            this.noArt = new System.Windows.Forms.CheckBox();
            this.WithExamsOnly = new System.Windows.Forms.CheckBox();
            this.noCulture = new System.Windows.Forms.CheckBox();
            this.zeroHours = new System.Windows.Forms.Button();
            this.orderByGroupname = new System.Windows.Forms.CheckBox();
            this.mixedGroups = new System.Windows.Forms.CheckBox();
            this.DifferenceByOne = new System.Windows.Forms.CheckBox();
            this.HoursFitFiltered = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.refresh = new System.Windows.Forms.Button();
            this.groupNameList = new System.Windows.Forms.ComboBox();
            this.groupnameFilter = new System.Windows.Forms.CheckBox();
            this.discnameFilter = new System.Windows.Forms.CheckBox();
            this.filter = new System.Windows.Forms.TextBox();
            this.hoursWeekFilter = new System.Windows.Forms.TextBox();
            this.controlsPanel.SuspendLayout();
            this.ListPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisciplinesList)).BeginInit();
            this.filterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.label12);
            this.controlsPanel.Controls.Add(this.label11);
            this.controlsPanel.Controls.Add(this.TypeSequence);
            this.controlsPanel.Controls.Add(this.label10);
            this.controlsPanel.Controls.Add(this.SyncGroupName);
            this.controlsPanel.Controls.Add(this.AuditoriumHoursPerWeek);
            this.controlsPanel.Controls.Add(this.label9);
            this.controlsPanel.Controls.Add(this.checkForDoubleDiscsOnAdding);
            this.controlsPanel.Controls.Add(this.reloadGroupList);
            this.controlsPanel.Controls.Add(this.CompletelyDelete);
            this.controlsPanel.Controls.Add(this.Paste);
            this.controlsPanel.Controls.Add(this.Attestation);
            this.controlsPanel.Controls.Add(this.Group);
            this.controlsPanel.Controls.Add(this.label6);
            this.controlsPanel.Controls.Add(this.PracticalHours);
            this.controlsPanel.Controls.Add(this.label5);
            this.controlsPanel.Controls.Add(this.LectureHours);
            this.controlsPanel.Controls.Add(this.label4);
            this.controlsPanel.Controls.Add(this.AuditoriumHours);
            this.controlsPanel.Controls.Add(this.label3);
            this.controlsPanel.Controls.Add(this.label2);
            this.controlsPanel.Controls.Add(this.remove);
            this.controlsPanel.Controls.Add(this.update);
            this.controlsPanel.Controls.Add(this.add);
            this.controlsPanel.Controls.Add(this.DisciplineName);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(233, 630);
            this.controlsPanel.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(164, 326);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 111;
            this.label12.Text = "ПЛ=4";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(164, 306);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 110;
            this.label11.Text = "ЛП=3";
            // 
            // TypeSequence
            // 
            this.TypeSequence.Location = new System.Drawing.Point(8, 361);
            this.TypeSequence.Name = "TypeSequence";
            this.TypeSequence.Size = new System.Drawing.Size(198, 20);
            this.TypeSequence.TabIndex = 108;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 345);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(189, 13);
            this.label10.TabIndex = 109;
            this.label10.Text = "Последовательность лекц=1 / пр=2";
            // 
            // SyncGroupName
            // 
            this.SyncGroupName.AutoSize = true;
            this.SyncGroupName.Location = new System.Drawing.Point(8, 322);
            this.SyncGroupName.Name = "SyncGroupName";
            this.SyncGroupName.Size = new System.Drawing.Size(157, 17);
            this.SyncGroupName.TabIndex = 107;
            this.SyncGroupName.Text = "Синхронизировать группу";
            this.SyncGroupName.UseVisualStyleBackColor = true;
            // 
            // AuditoriumHoursPerWeek
            // 
            this.AuditoriumHoursPerWeek.Location = new System.Drawing.Point(6, 151);
            this.AuditoriumHoursPerWeek.Name = "AuditoriumHoursPerWeek";
            this.AuditoriumHoursPerWeek.Size = new System.Drawing.Size(198, 20);
            this.AuditoriumHoursPerWeek.TabIndex = 105;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(194, 13);
            this.label9.TabIndex = 106;
            this.label9.Text = "Аудиторные часы в неделю (ШКОЛА)";
            // 
            // checkForDoubleDiscsOnAdding
            // 
            this.checkForDoubleDiscsOnAdding.AutoSize = true;
            this.checkForDoubleDiscsOnAdding.Location = new System.Drawing.Point(8, 299);
            this.checkForDoubleDiscsOnAdding.Name = "checkForDoubleDiscsOnAdding";
            this.checkForDoubleDiscsOnAdding.Size = new System.Drawing.Size(160, 17);
            this.checkForDoubleDiscsOnAdding.TabIndex = 104;
            this.checkForDoubleDiscsOnAdding.Text = "Проверка при добавлении";
            this.checkForDoubleDiscsOnAdding.UseVisualStyleBackColor = true;
            // 
            // reloadGroupList
            // 
            this.reloadGroupList.Location = new System.Drawing.Point(88, 387);
            this.reloadGroupList.Name = "reloadGroupList";
            this.reloadGroupList.Size = new System.Drawing.Size(115, 81);
            this.reloadGroupList.TabIndex = 103;
            this.reloadGroupList.Text = "Перезагрузить список групп";
            this.reloadGroupList.UseVisualStyleBackColor = true;
            this.reloadGroupList.Click += new System.EventHandler(this.reloadGroupList_Click);
            // 
            // CompletelyDelete
            // 
            this.CompletelyDelete.Location = new System.Drawing.Point(8, 474);
            this.CompletelyDelete.Name = "CompletelyDelete";
            this.CompletelyDelete.Size = new System.Drawing.Size(195, 57);
            this.CompletelyDelete.TabIndex = 102;
            this.CompletelyDelete.Text = "Совсем удалить";
            this.CompletelyDelete.UseVisualStyleBackColor = true;
            this.CompletelyDelete.Click += new System.EventHandler(this.CompletelyDelete_Click);
            // 
            // Paste
            // 
            this.Paste.Location = new System.Drawing.Point(209, 25);
            this.Paste.Name = "Paste";
            this.Paste.Size = new System.Drawing.Size(18, 20);
            this.Paste.TabIndex = 100;
            this.Paste.Text = "*";
            this.Paste.UseVisualStyleBackColor = true;
            this.Paste.Click += new System.EventHandler(this.PasteClick);
            // 
            // Attestation
            // 
            this.Attestation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Attestation.FormattingEnabled = true;
            this.Attestation.Location = new System.Drawing.Point(6, 63);
            this.Attestation.Name = "Attestation";
            this.Attestation.Size = new System.Drawing.Size(197, 21);
            this.Attestation.TabIndex = 101;
            // 
            // Group
            // 
            this.Group.FormattingEnabled = true;
            this.Group.Location = new System.Drawing.Point(7, 272);
            this.Group.Name = "Group";
            this.Group.Size = new System.Drawing.Size(197, 21);
            this.Group.TabIndex = 5;
            this.Group.SelectedIndexChanged += new System.EventHandler(this.Group_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Группа";
            // 
            // PracticalHours
            // 
            this.PracticalHours.Location = new System.Drawing.Point(6, 233);
            this.PracticalHours.Name = "PracticalHours";
            this.PracticalHours.Size = new System.Drawing.Size(198, 20);
            this.PracticalHours.TabIndex = 4;
            this.PracticalHours.Enter += new System.EventHandler(this.HoursEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Семинары/Практические часы";
            // 
            // LectureHours
            // 
            this.LectureHours.Location = new System.Drawing.Point(6, 194);
            this.LectureHours.Name = "LectureHours";
            this.LectureHours.Size = new System.Drawing.Size(198, 20);
            this.LectureHours.TabIndex = 3;
            this.LectureHours.Enter += new System.EventHandler(this.HoursEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Лекционные часы";
            // 
            // AuditoriumHours
            // 
            this.AuditoriumHours.Location = new System.Drawing.Point(6, 103);
            this.AuditoriumHours.Name = "AuditoriumHours";
            this.AuditoriumHours.Size = new System.Drawing.Size(198, 20);
            this.AuditoriumHours.TabIndex = 1;
            this.AuditoriumHours.Enter += new System.EventHandler(this.HoursEnter);
            this.AuditoriumHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AuditoriumHours_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Аудиторные часы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Форма отчётности";
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(8, 445);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 31;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.RemoveClick);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(8, 416);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 30;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.UpdateClick);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(7, 387);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 6;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.AddClick);
            // 
            // DisciplineName
            // 
            this.DisciplineName.Location = new System.Drawing.Point(6, 25);
            this.DisciplineName.Name = "DisciplineName";
            this.DisciplineName.Size = new System.Drawing.Size(197, 20);
            this.DisciplineName.TabIndex = 0;
            this.DisciplineName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DisciplineName_KeyPress);
            this.DisciplineName.Leave += new System.EventHandler(this.DisciplineName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Наименование дисциплины";
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.viewPanel);
            this.ListPanel.Controls.Add(this.filterPanel);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(233, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(1308, 630);
            this.ListPanel.TabIndex = 28;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.DisciplinesList);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 67);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(1308, 563);
            this.viewPanel.TabIndex = 2;
            // 
            // DisciplinesList
            // 
            this.DisciplinesList.AllowUserToAddRows = false;
            this.DisciplinesList.AllowUserToDeleteRows = false;
            this.DisciplinesList.AllowUserToResizeRows = false;
            this.DisciplinesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DisciplinesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisciplinesList.Location = new System.Drawing.Point(0, 0);
            this.DisciplinesList.Name = "DisciplinesList";
            this.DisciplinesList.ReadOnly = true;
            this.DisciplinesList.Size = new System.Drawing.Size(1308, 563);
            this.DisciplinesList.TabIndex = 0;
            this.DisciplinesList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DiscipineListViewCellClick);
            this.DisciplinesList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DiscipineListView_CellFormatting);
            this.DisciplinesList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DiscipineListView_CellMouseDoubleClick);
            this.DisciplinesList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DisciplinesList_CellMouseDown);
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.hoursWeekFilter);
            this.filterPanel.Controls.Add(this.hoursFilteredByWeek);
            this.filterPanel.Controls.Add(this.orderByDisciplineName);
            this.filterPanel.Controls.Add(this.withoutTypeSequence);
            this.filterPanel.Controls.Add(this.WithLessonsToday);
            this.filterPanel.Controls.Add(this.noPost);
            this.filterPanel.Controls.Add(this.noArt);
            this.filterPanel.Controls.Add(this.WithExamsOnly);
            this.filterPanel.Controls.Add(this.noCulture);
            this.filterPanel.Controls.Add(this.zeroHours);
            this.filterPanel.Controls.Add(this.orderByGroupname);
            this.filterPanel.Controls.Add(this.mixedGroups);
            this.filterPanel.Controls.Add(this.DifferenceByOne);
            this.filterPanel.Controls.Add(this.HoursFitFiltered);
            this.filterPanel.Controls.Add(this.label8);
            this.filterPanel.Controls.Add(this.label7);
            this.filterPanel.Controls.Add(this.refresh);
            this.filterPanel.Controls.Add(this.groupNameList);
            this.filterPanel.Controls.Add(this.groupnameFilter);
            this.filterPanel.Controls.Add(this.discnameFilter);
            this.filterPanel.Controls.Add(this.filter);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(1308, 67);
            this.filterPanel.TabIndex = 1;
            // 
            // hoursFilteredByWeek
            // 
            this.hoursFilteredByWeek.AutoSize = true;
            this.hoursFilteredByWeek.Location = new System.Drawing.Point(131, 7);
            this.hoursFilteredByWeek.Name = "hoursFilteredByWeek";
            this.hoursFilteredByWeek.Size = new System.Drawing.Size(110, 17);
            this.hoursFilteredByWeek.TabIndex = 21;
            this.hoursFilteredByWeek.Text = "Часы на неделю";
            this.hoursFilteredByWeek.UseVisualStyleBackColor = true;
            // 
            // orderByDisciplineName
            // 
            this.orderByDisciplineName.AutoSize = true;
            this.orderByDisciplineName.Checked = true;
            this.orderByDisciplineName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.orderByDisciplineName.Location = new System.Drawing.Point(1105, 48);
            this.orderByDisciplineName.Name = "orderByDisciplineName";
            this.orderByDisciplineName.Size = new System.Drawing.Size(205, 17);
            this.orderByDisciplineName.TabIndex = 20;
            this.orderByDisciplineName.Text = "сортировать по имени дисциплины";
            this.orderByDisciplineName.UseVisualStyleBackColor = true;
            // 
            // withoutTypeSequence
            // 
            this.withoutTypeSequence.AutoSize = true;
            this.withoutTypeSequence.Location = new System.Drawing.Point(1105, 29);
            this.withoutTypeSequence.Name = "withoutTypeSequence";
            this.withoutTypeSequence.Size = new System.Drawing.Size(197, 17);
            this.withoutTypeSequence.TabIndex = 19;
            this.withoutTypeSequence.Text = "без последовательности занятий";
            this.withoutTypeSequence.UseVisualStyleBackColor = true;
            // 
            // WithLessonsToday
            // 
            this.WithLessonsToday.AutoSize = true;
            this.WithLessonsToday.Location = new System.Drawing.Point(1105, 9);
            this.WithLessonsToday.Name = "WithLessonsToday";
            this.WithLessonsToday.Size = new System.Drawing.Size(134, 17);
            this.WithLessonsToday.TabIndex = 18;
            this.WithLessonsToday.Text = "с занятиями сегодня";
            this.WithLessonsToday.UseVisualStyleBackColor = true;
            // 
            // noPost
            // 
            this.noPost.AutoSize = true;
            this.noPost.Location = new System.Drawing.Point(950, 48);
            this.noPost.Name = "noPost";
            this.noPost.Size = new System.Drawing.Size(113, 17);
            this.noPost.TabIndex = 17;
            this.noPost.Text = "без аспирантуры";
            this.noPost.UseVisualStyleBackColor = true;
            // 
            // noArt
            // 
            this.noArt.AutoSize = true;
            this.noArt.Location = new System.Drawing.Point(948, 27);
            this.noArt.Name = "noArt";
            this.noArt.Size = new System.Drawing.Size(155, 17);
            this.noArt.TabIndex = 16;
            this.noArt.Text = "без факультета искусств";
            this.noArt.UseVisualStyleBackColor = true;
            // 
            // WithExamsOnly
            // 
            this.WithExamsOnly.AutoSize = true;
            this.WithExamsOnly.Location = new System.Drawing.Point(948, 7);
            this.WithExamsOnly.Name = "WithExamsOnly";
            this.WithExamsOnly.Size = new System.Drawing.Size(139, 17);
            this.WithExamsOnly.TabIndex = 15;
            this.WithExamsOnly.Text = "Только с экзаменами";
            this.WithExamsOnly.UseVisualStyleBackColor = true;
            // 
            // noCulture
            // 
            this.noCulture.AutoSize = true;
            this.noCulture.Location = new System.Drawing.Point(763, 24);
            this.noCulture.Name = "noCulture";
            this.noCulture.Size = new System.Drawing.Size(158, 17);
            this.noCulture.TabIndex = 14;
            this.noCulture.Text = "без физической культуры";
            this.noCulture.UseVisualStyleBackColor = true;
            // 
            // zeroHours
            // 
            this.zeroHours.Location = new System.Drawing.Point(763, 41);
            this.zeroHours.Name = "zeroHours";
            this.zeroHours.Size = new System.Drawing.Size(179, 23);
            this.zeroHours.TabIndex = 13;
            this.zeroHours.Text = "0 часов в расписании";
            this.zeroHours.UseVisualStyleBackColor = true;
            this.zeroHours.Click += new System.EventHandler(this.zeroHours_Click);
            // 
            // orderByGroupname
            // 
            this.orderByGroupname.AutoSize = true;
            this.orderByGroupname.Location = new System.Drawing.Point(763, 7);
            this.orderByGroupname.Name = "orderByGroupname";
            this.orderByGroupname.Size = new System.Drawing.Size(179, 17);
            this.orderByGroupname.TabIndex = 12;
            this.orderByGroupname.Text = "сортировать по имени группы";
            this.orderByGroupname.UseVisualStyleBackColor = true;
            // 
            // mixedGroups
            // 
            this.mixedGroups.AutoSize = true;
            this.mixedGroups.Location = new System.Drawing.Point(554, 43);
            this.mixedGroups.Name = "mixedGroups";
            this.mixedGroups.Size = new System.Drawing.Size(163, 17);
            this.mixedGroups.TabIndex = 11;
            this.mixedGroups.Text = "только смешанные группы";
            this.mixedGroups.UseVisualStyleBackColor = true;
            // 
            // DifferenceByOne
            // 
            this.DifferenceByOne.AutoSize = true;
            this.DifferenceByOne.Location = new System.Drawing.Point(554, 24);
            this.DifferenceByOne.Name = "DifferenceByOne";
            this.DifferenceByOne.Size = new System.Drawing.Size(196, 17);
            this.DifferenceByOne.TabIndex = 10;
            this.DifferenceByOne.Text = "в т.ч. убрать с превышением на 1";
            this.DifferenceByOne.UseVisualStyleBackColor = true;
            // 
            // HoursFitFiltered
            // 
            this.HoursFitFiltered.AutoSize = true;
            this.HoursFitFiltered.Location = new System.Drawing.Point(554, 5);
            this.HoursFitFiltered.Name = "HoursFitFiltered";
            this.HoursFitFiltered.Size = new System.Drawing.Size(199, 17);
            this.HoursFitFiltered.TabIndex = 9;
            this.HoursFitFiltered.Text = "только не совпадающие по часам";
            this.HoursFitFiltered.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(316, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Фильтр по группе";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Фильтр по названию";
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(436, 12);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(112, 41);
            this.refresh.TabIndex = 6;
            this.refresh.Text = "Обновить";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // groupNameList
            // 
            this.groupNameList.FormattingEnabled = true;
            this.groupNameList.Location = new System.Drawing.Point(330, 29);
            this.groupNameList.Name = "groupNameList";
            this.groupNameList.Size = new System.Drawing.Size(100, 21);
            this.groupNameList.TabIndex = 5;
            this.groupNameList.SelectedIndexChanged += new System.EventHandler(this.groupNameList_SelectedIndexChanged);
            // 
            // groupnameFilter
            // 
            this.groupnameFilter.AutoSize = true;
            this.groupnameFilter.Location = new System.Drawing.Point(309, 32);
            this.groupnameFilter.Name = "groupnameFilter";
            this.groupnameFilter.Size = new System.Drawing.Size(15, 14);
            this.groupnameFilter.TabIndex = 4;
            this.groupnameFilter.UseVisualStyleBackColor = true;
            // 
            // discnameFilter
            // 
            this.discnameFilter.AutoSize = true;
            this.discnameFilter.Location = new System.Drawing.Point(13, 33);
            this.discnameFilter.Name = "discnameFilter";
            this.discnameFilter.Size = new System.Drawing.Size(15, 14);
            this.discnameFilter.TabIndex = 3;
            this.discnameFilter.UseVisualStyleBackColor = true;
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(34, 30);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(267, 20);
            this.filter.TabIndex = 0;
            // 
            // hoursWeekFilter
            // 
            this.hoursWeekFilter.Location = new System.Drawing.Point(236, 5);
            this.hoursWeekFilter.Name = "hoursWeekFilter";
            this.hoursWeekFilter.Size = new System.Drawing.Size(65, 20);
            this.hoursWeekFilter.TabIndex = 22;
            // 
            // DisciplineList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1541, 630);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "DisciplineList";
            this.Text = "Дисциплины";
            this.Load += new System.EventHandler(this.DisciplineListLoad);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.ListPanel.ResumeLayout(false);
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DisciplinesList)).EndInit();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel controlsPanel;
        private ComboBox Attestation;
        private ComboBox Group;
        private Label label6;
        private TextBox PracticalHours;
        private Label label5;
        private TextBox LectureHours;
        private Label label4;
        private TextBox AuditoriumHours;
        private Label label3;
        private Label label2;
        private Button remove;
        private Button update;
        private Button add;
        private TextBox DisciplineName;
        private Label label1;
        private Panel ListPanel;
        private Panel viewPanel;
        private DataGridView DisciplinesList;
        private Panel filterPanel;
        private TextBox filter;
        private Button Paste;
        private CheckBox discnameFilter;
        private Button refresh;
        private ComboBox groupNameList;
        private CheckBox groupnameFilter;
        private Label label8;
        private Label label7;
        private Button CompletelyDelete;
        private CheckBox HoursFitFiltered;
        private CheckBox DifferenceByOne;
        private Button reloadGroupList;
        private CheckBox mixedGroups;
        private CheckBox orderByGroupname;
        private Button zeroHours;
        private CheckBox checkForDoubleDiscsOnAdding;
        private TextBox AuditoriumHoursPerWeek;
        private Label label9;
        private CheckBox noCulture;
        private CheckBox WithExamsOnly;
        private CheckBox noArt;
        private CheckBox noPost;
        private CheckBox WithLessonsToday;
        private CheckBox SyncGroupName;
        private TextBox TypeSequence;
        private Label label10;
        private Label label12;
        private Label label11;
        private CheckBox withoutTypeSequence;
        private CheckBox orderByDisciplineName;
        private CheckBox hoursFilteredByWeek;
        private TextBox hoursWeekFilter;
    }
}