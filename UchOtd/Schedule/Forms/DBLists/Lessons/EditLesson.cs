using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Schedule.Core;
using Schedule.DomainClasses.Logs;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Views;

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

        int _curTFDIndex;
        Dictionary<string, Tuple<string, List<Lesson>>> _curLessons;

        public EditLesson(ScheduleRepository repo, int groupId, int dow, string time, bool putProposedLessons)
        {
            InitializeComponent();

            _repo = repo;
            _groupId = groupId;
            _dow = dow;
            _time = time;
            _ring = _repo.FindRing(DateTime.ParseExact(_time, "H:mm", CultureInfo.InvariantCulture));
            _putProposedLesson = putProposedLessons;
        }

        private void EditLesson_Load(object sender, EventArgs e)
        {
            var sStarts = _repo.GetSemesterStarts();

            var gl = _repo.GetGroupedGroupLessons(_groupId, sStarts, -1, _putProposedLesson);

            _curLessons = new Dictionary<string, Tuple<string, List<Lesson>>>();
            if (gl.ContainsKey(_dow + " " + _time))
            {
                _curLessons = gl[_dow + " " + _time];
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
            tfd.Text = (new tfdView(_repo.GetTeacherForDiscipline(int.Parse(curTfd.Split('+')[0])))).tfdSummary;

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
                        TeacherForDiscipline = _repo.GetTeacherForDiscipline(int.Parse(_curLessons.Keys.ElementAt(_curTFDIndex).Split('+')[0])),
                        Ring = _ring,
                        Auditorium = _repo.FindAuditorium(newAuds[weekNumber]),
                        State = 1
                    };

                    // lesson.Calendar
                    var date = _repo.GetDateFromDowAndWeek(_dow, weekNumber);
                    var calendar = _repo.FindCalendar(date) ?? new global::Schedule.DomainClasses.Main.Calendar(date);
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

            var curTfd = _repo.GetTeacherForDiscipline(int.Parse(_curLessons.Keys.ElementAt(_curTFDIndex).Split('+')[0]));
            foreach (var week in newWeeks)
            {
                var lesson = new Lesson { 
                    TeacherForDiscipline = curTfd,
                    Ring = _ring,       
                    Auditorium = _repo.FindAuditorium(newAuds[week]),
                    State = 1
                };

                // lesson.Calendar
                var date = _repo.GetDateFromDowAndWeek(_dow, week);
                var calendar = _repo.FindCalendar(date) ?? new global::Schedule.DomainClasses.Main.Calendar(date);
                lesson.Calendar = calendar;

                _repo.AddLesson(lesson);
            }

            Close();
        } 

        private void acceptLessons_Click(object sender, EventArgs e)
        {
            foreach (var lesson in _curLessons[_curLessons.Keys.ElementAt(_curTFDIndex)].Item2)
            {
                lesson.State = 1;

                _repo.UpdateLesson(lesson);

                var acceptEvent = new LessonLogEvent
                {
                    DateTime = DateTime.Now,
                    OldLesson = null,
                    NewLesson = lesson,
                    PublicComment = "Утверждён проект"
                };
                _repo.AddLessonLogEvent(acceptEvent);
            }

            Close();
        }
    }
}
