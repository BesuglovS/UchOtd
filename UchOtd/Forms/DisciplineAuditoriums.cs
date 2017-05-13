using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Core;

namespace UchOtd.Forms
{
    public partial class DisciplineAuditoriums : Form
    {
        private readonly ScheduleRepository _initialRepo;
        private readonly string filename = "DisciplineAuditoriums.txt";

        public DisciplineAuditoriums(ScheduleRepository repo)
        {
            _initialRepo = repo;
            InitializeComponent();
        }

        private void DisciplineAuditoriums_Load(object sender, EventArgs e)
        {
            var allFaculties = _initialRepo.Faculties.GetAllFaculties().OrderBy(f => f.SortingOrder).ToList();

            faculties.DisplayMember = "Name";
            faculties.ValueMember = "FacultyId";
            faculties.DataSource = allFaculties;
        }

        private async void Go_Click(object sender, EventArgs e)
        {
            var allSemesters = semesters.Text.Split('\n').ToList();
            var facultyNames = new List<string>();
            foreach (var selecteditem in faculties.SelectedItems)
            {
                if (selecteditem != null)
                {
                    facultyNames.Add((selecteditem as Faculty).Name);
                }
            }

            try
            {
                await Task.Run(() =>
                {
                    foreach (var semester in allSemesters.OrderBy(a => a))
                    {
                        var dbName = "Schedule" + semester;

                        var repo = new ScheduleRepository("Server=uch-otd-disp,1433;Database=" + dbName + "; User ID=sa;Password=ghjuhfvvf; multipleactiveresultsets=True");

                        TextFileUtilities.WriteString(filename, "Семестр - " + semester);

                        var allFaculties = new List<Faculty>();
                        for (int i = 0; i < facultyNames.Count; i++)
                        {
                            var fName = facultyNames[i];

                            var f = repo.Faculties.FindFaculty(fName);

                            if (f != null)
                            {
                                allFaculties.Add(f);
                            }
                        }


                        foreach (var faculty in allFaculties.OrderBy(f => f.SortingOrder))
                        {
                            TextFileUtilities.WriteString(filename, "Факультет (" + faculty.FacultyId + ") - " + faculty.Name);

                            var faculty1 = faculty;
                            var facultyGroups =
                                repo.GroupsInFaculties.GetFiltredGroupsInFaculty(gif => gif.Faculty.FacultyId == faculty1.FacultyId)
                                    .Select(gif => gif.StudentGroup)
                                    .ToList();

                            for (int j = 0; j < facultyGroups.Count; j++)
                            {
                                var group = facultyGroups[j];

                                TextFileUtilities.WriteString(filename, "Группа " + @group.Name);

                                var studentIds = repo
                                    .StudentsInGroups
                                    .GetFiltredStudentsInGroups(
                                        sig => sig.StudentGroup.StudentGroupId == @group.StudentGroupId)
                                    .ToList()
                                    .Select(stig => stig.Student.StudentId);

                                var groupsListIds = repo
                                    .StudentsInGroups
                                    .GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                                    .ToList()
                                    .Select(stig => stig.StudentGroup.StudentGroupId);

                                var tfds =
                                    repo.TeacherForDisciplines.GetFiltredTeacherForDiscipline(
                                            tfd => groupsListIds.Contains(tfd.Discipline.StudentGroup.StudentGroupId))
                                        .OrderBy(tfd => tfd.Discipline.Name)
                                        .ToList();

                                foreach (var tfd in tfds)
                                {
                                    var tfd1 = tfd;
                                    var tfdLessons =
                                        repo.Lessons.GetFiltredLessons(
                                                l =>
                                                    l.State == 1 &&
                                                    l.TeacherForDiscipline.TeacherForDisciplineId == tfd1.TeacherForDisciplineId)
                                            .ToList();
                                    var auds = (tfdLessons.Count == 0) ? "" : tfdLessons.Select(l => l.Auditorium.Name).Distinct().OrderBy(a => a).ToList().Aggregate((a, b) => a + "\t" + b);

                                    TextFileUtilities.WriteString(filename, tfd.Discipline.Name + "\t" + auds);
                                }
                            }
                        }
                    }

                });
            }
            catch (Exception exc)
            {
                TextFileUtilities.WriteString(filename, DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - " + exc.Message);
            }
        }
    }
}
