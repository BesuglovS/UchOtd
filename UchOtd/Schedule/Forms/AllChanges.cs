using Schedule.Repositories;
using Schedule.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule.Forms
{
    public partial class AllChanges : Form
    {
        ScheduleRepository repo;

        public AllChanges(ScheduleRepository Repo)
        {
            InitializeComponent();

            repo = Repo;
        }

        private void AllChanges_Load(object sender, EventArgs e)
        {
            var changes = repo
                .GetAllLessonLogEvents()
                .OrderByDescending(lle => lle.DateTime)                
                .ToList();

            var changesView = LessonLogEventView.FromEventList(changes);

            view.DataSource = changesView;
            
            view.Columns["LessonLogEventId"].HeaderText = "Id";
            view.Columns["LessonLogEventId"].Width = 50;

            view.Columns["OldLesson"].HeaderText = "Старый урок";
            view.Columns["OldLesson"].Width = 150;

            view.Columns["NewLesson"].HeaderText = "Новый урок";
            view.Columns["NewLesson"].Width = 150;

            view.Columns["DateTime"].HeaderText = "Дата + время";
            view.Columns["DateTime"].Width = 100;

            view.Columns["PublicComment"].HeaderText = "PublicComment";
            view.Columns["PublicComment"].Width = 100;

            view.Columns["HiddenComment"].HeaderText = "HiddenComment";
            view.Columns["HiddenComment"].Width = 100;

            view.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            view.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }
    }
}
