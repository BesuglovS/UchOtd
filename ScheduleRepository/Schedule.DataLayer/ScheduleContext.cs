﻿using System.Data.Entity;
using Schedule.DomainClasses.Analyse;
using Schedule.DomainClasses.Config;
using Schedule.DomainClasses.Logs;
using Schedule.DomainClasses.Main;
using Schedule.DomainClasses.Session;

namespace Schedule.DataLayer
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext()
            :base("data source=tcp:uch-otd-disp,1433;Database=Schedule14152;User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True")
        {
        }

        public ScheduleContext(string connectionString)
            : base(connectionString)
        {
        }

        // Main
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Ring> Rings { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<StudentsInGroups> StudentsInGroups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherForDiscipline> TeacherForDiscipline { get; set; }

        public DbSet<AuditoriumEvent> AuditoriumEvents { get; set; }

        // Faculties
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<GroupsInFaculty> GroupsInFaculties { get; set; }

        public DbSet<ScheduleNote> ScheduleNotes { get; set; }
        
        // Logs
        public DbSet<LessonLogEvent> LessonLog { get; set; }

        // Options
        public DbSet<ConfigOption> Config { get; set; }

        // Analyse
        public DbSet<TeacherWish> TeacherWishes { get; set; }

        public DbSet<CustomTeacherAttribute> CustomTeacherAttributes { get; set; }        
        public DbSet<CustomDisciplineAttribute> CustomDisciplineAttributes { get; set; }
        public DbSet<CustomStudentGroupAttribute> CustomStudentGroupAttributes { get; set; }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftRing> ShiftRings { get; set; }
         
        // Session
        public DbSet<Exam> Exams { get; set; }
        public DbSet<LogEvent> EventLog { get; set; }
    }
}
