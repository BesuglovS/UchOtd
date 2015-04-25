using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DomainClasses.Main;

namespace Schedule.Repositories.Repositories.Main
{
    public class FacultiesRepository:BaseRepository<Faculty>
    {
        public List<Faculty> GetAllFaculties()
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Faculties.ToList();
            }
        }

        public List<Faculty> GetFiltredFaculties(Func<Faculty, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Faculties.ToList().Where(condition).ToList();
            }
        }

        public Faculty GetFirstFiltredFaculty(Func<Faculty, bool> condition)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Faculties.ToList().FirstOrDefault(condition);
            }
        }

        public Faculty GetFaculty(int facultyId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Faculties.FirstOrDefault(f => f.FacultyId == facultyId);
            }
        }

        public Faculty FindFaculty(string name)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.Faculties.FirstOrDefault(f => f.Name == name);
            }
        }

        public void AddFaculty(Faculty faculty)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                faculty.FacultyId = 0;

                context.Faculties.Add(faculty);
                context.SaveChanges();
            }
        }

        public void UpdateFaculty(Faculty faculty)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var curFaculty = context.Faculties.FirstOrDefault(f => f.FacultyId == faculty.FacultyId);

                if (curFaculty != null)
                {
                    curFaculty.Name = faculty.Name;
                    curFaculty.Letter = faculty.Letter;
                    curFaculty.SortingOrder = faculty.SortingOrder;

                    curFaculty.DeanSigningSchedule = faculty.DeanSigningSchedule;
                    curFaculty.ScheduleSigningTitle = faculty.ScheduleSigningTitle;

                    curFaculty.DeanSigningSessionSchedule = faculty.DeanSigningSessionSchedule;
                    curFaculty.SessionSigningTitle = faculty.SessionSigningTitle;
                }

                context.SaveChanges();
            }
        }

        public void RemoveFaculty(int facultyId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                var faculty = context.Faculties.FirstOrDefault(f => f.FacultyId == facultyId);

                context.Faculties.Remove(faculty);
                context.SaveChanges();
            }
        }

        public void AddFacultyRange(IEnumerable<Faculty> facultyList)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                foreach (var faculty in facultyList)
                {
                    faculty.FacultyId = 0;
                    context.Faculties.Add(faculty);
                }

                context.SaveChanges();
            }
        }

        public List<StudentGroup> GetFacultyGroups(int facultyId)
        {
            using (var context = new ScheduleContext(ConnectionString))
            {
                return context.GroupsInFaculties
                    .Where(gif => gif.Faculty.FacultyId == facultyId)
                    .Select(gif => gif.StudentGroup)
                    .ToList();
            }
        }
    }
}
