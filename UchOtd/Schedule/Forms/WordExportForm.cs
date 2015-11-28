﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schedule.Constants;
using Schedule.Repositories;
using UchOtd.Core;
using UchOtd.Properties;

namespace UchOtd.Schedule.Forms
{
    public partial class WordExportForm : Form
    {
        private readonly ScheduleRepository _repo;
        Dictionary<int, List<int>> _choice;

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public WordExportForm(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void WordExportForm_Load(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

            _choice = new Dictionary<int, List<int>>();

            var faculties = _repo.Faculties.GetAllFaculties();
            for (int i = 0; i < faculties.Count; i++)                
            {
                _choice.Add(faculties[i].FacultyId, new List<int>());

                for (int j = 1; j <=7 ; j++)                
                {   
                    var checkBox = new CheckBox
                    {
                        Parent = this,
                        Name = "cb_" + faculties[i].FacultyId + "_" + j,
                        Text = faculties[i].Letter + " " + Constants.DowLocal[j].Substring(0, 3),
                        Bounds = new Rectangle(-50 + j*80, 10 + i*25, 75, 25)
                    };
                    Controls.Add(checkBox);

                    checkBox.Click += CheckBoxClicked;
                }
            }

            var wordButton = new Button
            {
                Parent = this,
                Name = "ExportButton",
                Text = "Экспорт",
                Bounds = new Rectangle(30, 10 + (faculties.Count + 1)*25, 75, 25)
            };
            Controls.Add(wordButton);

            wordButton.Click += ExportButtonClick;

            var checkBox90 = new CheckBox
            {
                Parent = this,
                Name = "cb90",
                Text = "90 минут",
                Checked = false,
                Bounds = new Rectangle(130, 10 + (faculties.Count + 1)*25, 75, 25)
            };
            Controls.Add(checkBox90);

            var future = new CheckBox
            {
                Parent = this,
                Name = "cbFuture",
                Text = "только будущие даты",                
                Checked = true,
                Bounds = new Rectangle(210, 10 + (faculties.Count + 1)*25, 150, 25)
            };
            Controls.Add(future);

            var weekFiltered = new CheckBox
            {
                Parent = this,
                Name = "weekFiltered",
                Text = "Фильтр/неделя",
                Checked = false,
                Bounds = new Rectangle(360, 10 + (faculties.Count + 1) * 25, 120, 25)
            };
            Controls.Add(weekFiltered);


            var weekFilter = new ComboBox
            {
                Parent = this,
                Name = "weekFilter",
                Bounds = new Rectangle(480, 10 + (faculties.Count + 1) * 25, 100, 25)
            };
            for (int i = 1; i <= 18; i++)
            {
                weekFilter.Items.Add(i);
            }
            Controls.Add(weekFilter);

            var dailyChangesButton = new Button
            {
                Parent = this,
                Name = "dailyChanges",
                Text = "Изменения за день",
                Bounds = new Rectangle(30, 10 + (faculties.Count + 1) * 25 + 40, 125, 25)
            };
            Controls.Add(dailyChangesButton);

            dailyChangesButton.Click += dailyChangesButtonClick;

            Height = (faculties.Count + 1)*25 + 150;
        }


        private void CheckBoxClicked(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox == null) return;

            var split = checkBox.Name.Split('_');
            var facultyId = int.Parse(split[1]);
            var dow = int.Parse(split[2]);

            if (((CheckBox)sender).Checked)
            {
                _choice[facultyId].Add(dow);
            }
            else
            {
                _choice[facultyId].Remove(dow);
            }
        }

        private async void ExportButtonClick(object sender, EventArgs e)
        {
            var button = (sender as Button);
            if (button == null) return;

            if (button.Text == "Экспорт")
            {
                _cToken = _tokenSource.Token;

                button.Text = "";
                button.Image = Resources.Loading;

                var lesson8090Length = ((CheckBox)Controls.Find("cb90", false).First()).Checked ? 90 : 80;
                var futureDatesOnly = ((CheckBox)Controls.Find("cbfuture", false).First()).Checked;
                var weekFilteredF = ((CheckBox)Controls.Find("weekFiltered", false).First()).Checked;
                int weekFilterF = -1;
                int.TryParse(((ComboBox)Controls.Find("weekFilter", false).First()).Text, out weekFilterF);

                try
                {
                    await Task.Run(() => WordExport.ExportCustomSchedule(
                                _choice, _repo, "Расписание.docx", false, false,
                                lesson8090Length, 6, MainEditForm.SchoolHeader, futureDatesOnly, weekFilteredF, weekFilterF, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            button.Image = null;
            button.Text = "Экспорт";
        }

        private async void dailyChangesButtonClick(object sender, EventArgs e)
        {
            var button = (sender as Button);
            if (button == null) return;

            if (button.Text == "Изменения за день")
            {
                _cToken = _tokenSource.Token;

                button.Text = "";
                button.Image = Resources.Loading;

                await Task.Run(() => {
                                         // facultyId + DOW
                                         var result = new List<Tuple<int, DayOfWeek>>();

                                         var evts = _repo.LessonLogEvents.GetFiltredLessonLogEvents(lle => lle.DateTime.Date == DateTime.Now.Date);

                                         var fg = _repo.GroupsInFaculties.GetAllGroupsInFaculty()
                                             .GroupBy(gif => gif.Faculty.FacultyId,
                                                 gif => gif.StudentGroup.StudentGroupId)
                                             .ToList();


                                         foreach (var ev in evts)
                                         {
                                             int studentGroupId;
                                             if (ev.OldLesson != null)
                                             {
                                                 studentGroupId = ev.OldLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;

                                                 var id = studentGroupId;
                                                 var studentIds = _repo
                                                     .StudentsInGroups
                                                     .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == id)
                                                     .Select(sig => sig.Student.StudentId)
                                                     .ToList();

                                                 var facultyScheduleChanged = (
                                                     from faculty in fg
                                                     where _repo
                                                         .StudentsInGroups
                                                         .GetFiltredStudentsInGroups(sig =>
                                                             studentIds.Contains(sig.Student.StudentId) &&
                                                             faculty.Contains(sig.StudentGroup.StudentGroupId)).Any()
                                                     select faculty.Key)
                                                     .ToList();

                                                 var localEvent = ev;
                                                 foreach (var dowFacTuple in facultyScheduleChanged
                                                     .Select(faculty => Tuple.Create(faculty, localEvent.OldLesson.Calendar.Date.DayOfWeek))
                                                     .Where(dowFacTuple => !result.Contains(dowFacTuple)))
                                                 {
                                                     result.Add(dowFacTuple);
                                                 }
                                             }



                                             if (ev.NewLesson != null)
                                             {
                                                 studentGroupId = ev.NewLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;

                                                 var id = studentGroupId;
                                                 var studentIds = _repo
                                                     .StudentsInGroups
                                                     .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == id)
                                                     .Select(sig => sig.Student.StudentId)
                                                     .ToList();

                                                 var facultyScheduleChanged = (
                                                     from faculty in fg
                                                     where _repo
                                                         .StudentsInGroups
                                                         .GetFiltredStudentsInGroups(sig =>
                                                             studentIds.Contains(sig.Student.StudentId) &&
                                                             faculty.Contains(sig.StudentGroup.StudentGroupId)).Any()
                                                     select faculty.Key)
                                                     .ToList();

                                                 var localEvent = ev;
                                                 foreach (var dowFacTuple in facultyScheduleChanged
                                                     .Select(faculty => Tuple.Create(faculty, localEvent.NewLesson.Calendar.Date.DayOfWeek))
                                                     .Where(dowFacTuple => !result.Contains(dowFacTuple)))
                                                 {
                                                     result.Add(dowFacTuple);
                                                 }
                                             }
                                         }

                                         var messageString = result
                                             .OrderBy(df => df.Item1)
                                             .ThenBy(df => df.Item2)
                                             .Aggregate("", (current, dowFac) =>
                                                 current + (_repo.Faculties.GetFaculty(dowFac.Item1).Letter + " - " +
                                                            Constants.DowLocal[Constants.DowRemap[(int)dowFac.Item2]] + Environment.NewLine));

                                         MessageBox.Show(messageString, "Изменения на сегодня");
                });

            }
            else
            {
                _tokenSource.Cancel();
            }

            button.Image = null;
            button.Text = "Изменения за день";
        }
    }
}
