using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Schedule.DomainClasses.Logs;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Repositories.Common;
using UchOtd.Schedule.Core;
using UchOtd.Schedule.Views;
using Calendar = Schedule.DomainClasses.Main.Calendar;

namespace UchOtd.Schedule.Forms.DBLists.Lessons
{
    public partial class EditLesson : Form
    {
        private readonly ScheduleRepository _repo;
        private readonly int _groupId;
        private readonly int _dow;
        private readonly string _time;
        private readonly Ring _ring;
        private readonly bool _putProposedLesson;

        int _curTfdIndex;
        Dictionary<string, Tuple<string, List<Lesson>>> _curLessons;

        public EditLesson(ScheduleRepository repo, int groupId, int dow, string time, bool putProposedLessons)
        {
            InitializeComponent();

            _repo = repo;
            _groupId = groupId;
            _dow = dow;
            _time = time;
            _ring = _repo.Rings.FindRing(DateTime.ParseExact(_time, "H:mm", CultureInfo.InvariantCulture));
            _putProposedLesson = putProposedLessons;
        }

        private void EditLesson_Load(object sender, EventArgs e)
        {
            var sStarts = _repo.CommonFunctions.GetSemesterStarts();

            var gl = _repo.Lessons.GetGroupedGroupLessons(_groupId, sStarts, -1, _putProposedLesson, false);

            _curLessons = new Dictionary<string, Tuple<string, List<Lesson>>>();
            if (gl.ContainsKey(_dow + " " + _time))
            {
                _curLessons = gl[_dow + " " + _time];
                _curTfdIndex = 0;
            }
            else
            {
                _curTfdIndex = -1;
                tfdIndex.Text = "Пусто тут барин!";
            }

            DisplayTfd(_curTfdIndex);
        }

        private void DisplayTfd(int curTfdIndex)
        {
            if (curTfdIndex == -1)
            {
                return;
            }

            var curTfd = _curLessons.Keys.ElementAt(curTfdIndex);

            // tfd Index
            tfdIndex.Text = (curTfdIndex + 1) + " / " + (_curLessons.Keys.Count);

            // tfd Summary
            tfd.Text = (new TfdView(_repo.TeacherForDisciplines.GetTeacherForDiscipline(int.Parse(curTfd.Split('+')[0])))).TfdSummary;

            // Weeks
            lessonWeeks.Text = _curLessons[curTfd].Item1;

            // Auds
            
            auditoriums.Text = AudStringsFromLessonList(_curLessons[curTfd].Item2);

            // isProposed
            if (_curLessons[curTfd].Item2[0].State == 2)
            {
                proposedLessons.Checked = true;
                acceptLessons.Enabled = true;
                saveChanges.Enabled = false;
            }
            else
            {
                proposedLessons.Checked = false;
                acceptLessons.Enabled = false;
                saveChanges.Enabled = true;
            }

        }

        private string AudStringsFromLessonList(List<Lesson> lessons)
        {
            KeyValuePair<string, Tuple<string, List<Lesson>>> item;
            var audStrings = "";
            Dictionary<string, List<int>> AudWeeks = new Dictionary<string, List<int>>();
            for (int j = 0; j < lessons.Count; j++)
            {
                var lesson = lessons[j];
                if (!AudWeeks.ContainsKey(lesson.Auditorium.Name))
                {
                    AudWeeks.Add(lesson.Auditorium.Name, new List<int>());
                }

                AudWeeks[lesson.Auditorium.Name].Add(_repo.CommonFunctions.CalculateWeekNumber(lesson.Calendar.Date));
            }

            if (AudWeeks.Count == 1)
            {
                audStrings += AudWeeks.Keys.First();
            }
            else
            {
                var l = new List<string>();
                foreach (KeyValuePair<string, List<int>> audWeek in AudWeeks.OrderBy(aw => aw.Value.Min()))
                {
                    l.Add(CommonFunctions.CombineWeeks(audWeek.Value) + " - " + audWeek.Key);
                }

                audStrings += l.Aggregate((a, b) => a + Environment.NewLine + b);
            }
            return audStrings;
        }

        private void NextTfdClick(object sender, EventArgs e)
        {
            _curTfdIndex++;
            if (_curTfdIndex >= _curLessons.Keys.Count)
            {
                _curTfdIndex = 0;
            }

            DisplayTfd(_curTfdIndex);
        }

        private void PrevTfdClick(object sender, EventArgs e)
        {
            _curTfdIndex--;
            if (_curTfdIndex < 0)
            {
                _curTfdIndex = _curLessons.Keys.Count - 1;
            }

            DisplayTfd(_curTfdIndex);
        }

        private void RemoveLessonsClick(object sender, EventArgs e)
        {
            var lessonsIds = _curLessons[_curLessons.Keys.ElementAt(_curTfdIndex)].Item2.Select(l => l.LessonId);
            foreach (var lesson in lessonsIds)
            {
                _repo.Lessons.RemoveLesson(lesson);
            }

            Close();
        }

        private void SaveChangesClick(object sender, EventArgs e)
        {
            var oldWeeks = _curLessons[_curLessons.Keys.ElementAt(_curTfdIndex)].Item2.Select(l => _repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date)).ToList();
            var newWeeks = CommonFunctions.WeeksStringToList(lessonWeeks.Text);

            var oldAuds = _curLessons[_curLessons.Keys.ElementAt(_curTfdIndex)].Item2.ToDictionary(
                l => _repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date), 
                l => l.Auditorium.Name);
            var newAuds = Utilities.GetAudWeeksList(auditoriums.Text);
            var singleNewAud = newAuds.FirstOrDefault(a => a.Key == 0);
            if (singleNewAud.Value != null)
            {
                newAuds.Clear();
                foreach (var week in newWeeks)
                {
                    newAuds.Add(week, singleNewAud.Value);
                }
            }

            var keys = oldAuds.Keys.ToList();
            foreach (var audKey in keys)            
            {
                if (newAuds.ContainsKey(audKey) && (oldAuds[audKey] == newAuds[audKey]))
                {
                    oldAuds[audKey] = "-1";
                    oldWeeks.Remove(audKey);
                    newWeeks.Remove(audKey);
                    newAuds.Remove(audKey);
                }

                if (newAuds.ContainsKey(audKey) && (oldAuds[audKey] != newAuds[audKey]))
                {

                    var oldLesson = _curLessons[_curLessons.Keys.ElementAt(_curTfdIndex)].Item2
                        .FirstOrDefault(l => audKey == _repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date));

                    var weekNumber = -1;
                    if (oldLesson != null)
                    {
                        weekNumber = _repo.CommonFunctions.CalculateWeekNumber(_repo.Lessons.GetLesson(oldLesson.LessonId).Calendar.Date);
                        _repo.Lessons.RemoveLessonActiveStateWoLog(oldLesson.LessonId);
                    }

                    var newLesson = new Lesson
                    {
                        TeacherForDiscipline = _repo.TeacherForDisciplines.GetTeacherForDiscipline(int.Parse(_curLessons.Keys.ElementAt(_curTfdIndex).Split('+')[0])),
                        Ring = _ring,
                        Auditorium = _repo.Auditoriums.Find(newAuds[weekNumber]),
                        State = 1
                    };

                    // lesson.Calendar
                    var date = _repo.CommonFunctions.GetDateFromDowAndWeek(_dow, weekNumber);
                    var calendar = _repo.Calendars.FindCalendar(date) ?? new Calendar(date);
                    newLesson.Calendar = calendar;

                    _repo.Lessons.AddLessonWoLog(newLesson);

                    _repo.LessonLogEvents.AddLessonLogEvent(new LessonLogEvent
                                                {
                                                    DateTime = DateTime.Now,
                                                    OldLesson = oldLesson,
                                                    NewLesson = newLesson,
                                                    PublicComment = "",
                                                    HiddenComment = ""
                                                });

                    oldAuds[audKey] = "-1";
                    oldWeeks.Remove(audKey);
                    newWeeks.Remove(audKey);
                    newAuds.Remove(audKey);
                }
            }


            var idsToRemove = oldAuds.Where(a => a.Value == "-1").Select(a => a.Key).ToList();
            foreach (var audId in idsToRemove)
            {
                oldAuds.Remove(audId);
            }

            var oldLessonsIdToDelete = _curLessons[_curLessons.Keys.ElementAt(_curTfdIndex)].Item2
                .Where(l => oldWeeks.Contains(_repo.CommonFunctions.CalculateWeekNumber(l.Calendar.Date)))
                .Select(l => l.LessonId);
            foreach (var lessonId in oldLessonsIdToDelete)
            {
                _repo.Lessons.RemoveLesson(lessonId);
            }

            var curTfd = _repo.TeacherForDisciplines.GetTeacherForDiscipline(int.Parse(_curLessons.Keys.ElementAt(_curTfdIndex).Split('+')[0]));
            foreach (var week in newWeeks)
            {
                var lesson = new Lesson { 
                    TeacherForDiscipline = curTfd,
                    Ring = _ring,
                    Auditorium = _repo.Auditoriums.Find(newAuds[week]),
                    State = 1
                };

                // lesson.Calendar
                var date = _repo.CommonFunctions.GetDateFromDowAndWeek(_dow, week);
                var calendar = _repo.Calendars.FindCalendar(date) ?? new Calendar(date);
                lesson.Calendar = calendar;

                _repo.Lessons.AddLesson(lesson);
            }

            Close();
        } 

        private void acceptLessons_Click(object sender, EventArgs e)
        {
            foreach (var lesson in _curLessons[_curLessons.Keys.ElementAt(_curTfdIndex)].Item2)
            {
                lesson.State = 1;

                _repo.Lessons.UpdateLesson(lesson);

                var acceptEvent = new LessonLogEvent
                {
                    DateTime = DateTime.Now,
                    OldLesson = null,
                    NewLesson = lesson,
                    PublicComment = "Утверждён проект"
                };
                _repo.LessonLogEvents.AddLessonLogEvent(acceptEvent);
            }

            Close();
        }
    }
}
