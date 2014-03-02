using Schedule.Core;
using Schedule.DomainClasses.Logs;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Schedule.Forms.DBLists.Lessons
{
    public partial class EditLesson : Form
    {
        private readonly ScheduleRepository _repo;
        private readonly int _dow;
        private readonly Ring _ring;

        int _curTFDIndex;
        readonly Dictionary<int, Tuple<string, List<Lesson>>> _curLessons;

        public EditLesson(ScheduleRepository repo, int groupId, int dow, string time)
        {
            InitializeComponent();

            _repo = repo;
            _dow = dow;
            _ring = _repo.FindRing(DateTime.ParseExact(time, "H:mm", CultureInfo.InvariantCulture));

            var sStarts = _repo.GetSemesterStarts();

            var gl = _repo.GetGroupedGroupLessons(groupId, sStarts);

            _curLessons = new Dictionary<int, Tuple<string, List<Lesson>>>();
            if (gl.ContainsKey(dow + " " + time))
            {
                _curLessons = gl[dow + " " + time];
                _curTFDIndex = 0;
            }
            else
            {
                _curTFDIndex = -1;
                tfdIndex.Text = "Пусто тут барин!";
            }            

            DisplayTFD(_curTFDIndex);
        }

        private void DisplayTFD(int curTFDIndex)
        {
            if (curTFDIndex == -1)
            {
                return;
            }

            var curTfd = _curLessons.Keys.ElementAt(curTFDIndex);

            // tfd Index
            tfdIndex.Text = (curTFDIndex + 1) + " / " + (_curLessons.Keys.Count);

            // tfd Summary
            tfd.Text = (new tfdView(_repo.GetTeacherForDiscipline(curTfd))).tfdSummary;

            // Weeks
            lessonWeeks.Text = _curLessons[curTfd].Item1;
            
            // Auds
            string audString = "";
            var audWeekList = _curLessons[curTfd].Item2.ToDictionary(l => _repo.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
            var grouped = audWeekList.GroupBy(a => a.Value);

            var gcount = grouped.Count();
            if (gcount == 1)
            {
                audString += grouped.ElementAt(0).Key;
            }
            else
            {
                for (int j = 0; j < gcount; j++)
                {
                    var jItem = grouped.ElementAt(j);
                    audString += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

                    if (j != gcount - 1)
                    {
                        audString += Environment.NewLine;
                    }
                }
            }
            auditoriums.Text = audString;            
        }

        private void NextTFDClick(object sender, EventArgs e)
        {
            _curTFDIndex++;
            if (_curTFDIndex >= _curLessons.Keys.Count)
            {
                _curTFDIndex = 0;
            }

            DisplayTFD(_curTFDIndex);
        }

        private void PrevTFDClick(object sender, EventArgs e)
        {
            _curTFDIndex--;
            if (_curTFDIndex < 0)
            {
                _curTFDIndex = _curLessons.Keys.Count - 1;
            }

            DisplayTFD(_curTFDIndex);
        }

        private void RemoveLessonsClick(object sender, EventArgs e)
        {
            var lessonsIds = _curLessons[_curLessons.Keys.ElementAt(_curTFDIndex)].Item2.Select(l => l.LessonId);
            foreach (var lesson in lessonsIds)
            {
                _repo.RemoveLesson(lesson);
            }

            Close();
        }

        private void SaveChangesClick(object sender, EventArgs e)
        {
            var oldWeeks = _curLessons[_curLessons.Keys.ElementAt(_curTFDIndex)].Item2.Select(l => _repo.CalculateWeekNumber(l.Calendar.Date)).ToList();
            var newWeeks = ScheduleRepository.WeeksStringToList(lessonWeeks.Text);

            var oldAuds = _curLessons[_curLessons.Keys.ElementAt(_curTFDIndex)].Item2.ToDictionary(
                l => _repo.CalculateWeekNumber(l.Calendar.Date), 
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

                    var oldLesson = _curLessons[_curLessons.Keys.ElementAt(_curTFDIndex)].Item2
                        .FirstOrDefault(l => audKey == _repo.CalculateWeekNumber(l.Calendar.Date));

                    var weekNumber = -1;
                    if (oldLesson != null)
                    {
                        weekNumber = _repo.CalculateWeekNumber(_repo.GetLesson(oldLesson.LessonId).Calendar.Date);
                        _repo.RemoveLessonActiveStateWOLog(oldLesson.LessonId);
                    }

                    var newLesson = new Lesson
                    {
                        TeacherForDiscipline = _repo.GetTeacherForDiscipline(_curLessons.Keys.ElementAt(_curTFDIndex)),
                        Ring = _ring,
                        Auditorium = _repo.FindAuditorium(newAuds[weekNumber]),
                        IsActive = true
                    };

                    // lesson.Calendar
                    var date = _repo.GetDateFromDowAndWeek(_dow, weekNumber);
                    var calendar = _repo.FindCalendar(date) ?? new DomainClasses.Main.Calendar(date);
                    newLesson.Calendar = calendar;

                    _repo.AddLessonWOLog(newLesson);

                    _repo.AddLessonLogEvent(new LessonLogEvent
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

            var oldLessonsIdToDelete = _curLessons[_curLessons.Keys.ElementAt(_curTFDIndex)].Item2
                .Where(l => oldWeeks.Contains(_repo.CalculateWeekNumber(l.Calendar.Date)))
                .Select(l => l.LessonId);
            foreach (var lessonId in oldLessonsIdToDelete)
            {
                _repo.RemoveLesson(lessonId);
            }

            var curTfd = _repo.GetTeacherForDiscipline(_curLessons.Keys.ElementAt(_curTFDIndex));
            foreach (var week in newWeeks)
            {
                var lesson = new Lesson { 
                    TeacherForDiscipline = curTfd,
                    Ring = _ring,       
                    Auditorium = _repo.FindAuditorium(newAuds[week]),
                    IsActive = true
                };

                // lesson.Calendar
                var date = _repo.GetDateFromDowAndWeek(_dow, week);
                var calendar = _repo.FindCalendar(date) ?? new DomainClasses.Main.Calendar(date);
                lesson.Calendar = calendar;

                _repo.AddLesson(lesson);
            }

            Close();
        }
    }
}
