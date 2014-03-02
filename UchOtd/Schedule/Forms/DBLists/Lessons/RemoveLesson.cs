using Schedule.Repositories;
using Schedule.Views.DBListViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule.Forms.DBLists.Lessons
{
    public partial class RemoveLesson : Form
    {
        private readonly ScheduleRepository _repo;

        public RemoveLesson(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void showLessons_Click(object sender, EventArgs e)
        {
            
        }

        private void RemoveLesson_Load(object sender, EventArgs e)
        {
            var groups = _repo.GetAllStudentGroups();
            groups = groups.OrderBy(g => g.Name).ToList();

            groupName.DataSource = groups;
            groupName.ValueMember = "StudentGroupId";
            groupName.DisplayMember = "Name";

            dayOfWeek.Items.Clear();
            foreach (var dow in Constants.Constants.DOWLocal.Values)
            {
                dayOfWeek.Items.Add(dow);
            }

            var rings = _repo.GetAllRings();
            var ringViews = RingView.RingsToView(rings);
            ringViews = ringViews.OrderBy(rv => DateTime.ParseExact(rv.Time, "H:mm", CultureInfo.InvariantCulture)).ToList();

            ring.DataSource = ringViews;
            ring.ValueMember = "RingId";
            ring.DisplayMember = "Time";
        }
    }
}
