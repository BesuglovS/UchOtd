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
        private readonly string filename = "D:\\Github\\DisciplineAuditoriums.txt";

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
                    var result = new Dictionary<string, List<string>>(); // DiscName + List<audName>

                    foreach (var semester in allSemesters.OrderBy(a => a))
                    {
                        var dbName = "S" + semester.Trim() + "AA";
                        
                        var connectionString = "data source=tcp:" + StartupForm.CurrentServerName + ",1433; Database=" + dbName +
                                               "; User ID=sa;Password=ghjuhfvvf; multipleactiveresultsets=True";

                        var repo = new ScheduleRepository(connectionString);

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

                                TextFileUtilities.WriteString(filename, "Группа " + group.Name);

                                var studentIds = repo
                                    .StudentsInGroups
                                    .GetFiltredStudentsInGroups(
                                        sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId && !sig.Student.Expelled)
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
                                    var auds = (tfdLessons.Count == 0) ? new List<string>() : 
                                        tfdLessons
                                        .Select(l => l.Auditorium.Name)
                                        .Distinct()
                                        .OrderBy(a => a)
                                        .ToList();

                                    if (!result.ContainsKey(tfd1.Discipline.Name))
                                    {
                                        result.Add(tfd1.Discipline.Name, new List<string>());
                                    }

                                    foreach (var aud in auds)
                                    {
                                        if (!result[tfd1.Discipline.Name].Contains(aud))
                                        {
                                            result[tfd1.Discipline.Name].Add(aud);
                                        }
                                    }

                                    //TextFileUtilities.WriteString(filename, tfd.Discipline.Name + "\t" + auds);
                                }
                            }
                        }
                    }


                    foreach (string discName in result.Keys.OrderBy(a => a).ToList())
                    {
                        var auds = (result[discName].Count == 0)
                            ? ""
                            : result[discName].OrderBy(a => a).Aggregate((a, b) => a + "\t" + b);
                        TextFileUtilities.WriteString(filename, discName + "\t" + auds);
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
