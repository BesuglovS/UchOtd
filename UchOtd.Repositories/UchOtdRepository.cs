using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using UchOtd.DataLayer;
using UchOtd.DataLayer.Migrations;
using UchOtd.DomainClasses;

namespace UchOtd.Repositories
{
    public class UchOtdRepository : IDisposable
    {
        public string ConnectionString { get; set; }

        public UchOtdRepository(string connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UchOtdContext, Configuration>());

            ConnectionString = connectionString;
        }

        public void SaveChanges()
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                context.SaveChanges();
            }
        }

        public void RecreateDb()
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                context.Database.Delete();
                context.Database.CreateIfNotExists();
            }
        }

        public void DebugLog(string message)
        {
            var sw = new StreamWriter("DebugLog.txt", true);
            sw.WriteLine(message);
            sw.Close();
        }

        #region IDisposable
        public void Dispose()
        {
        }
        #endregion

        #region NotesPepository
        public List<Note> GetAllNotes()
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Notes.ToList();
            }
        }

        public bool ContainsNote(Note note)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Notes.FirstOrDefault(n =>
                    n.Moment == note.Moment &&
                    n.TargetComputer == note.TargetComputer &&
                    n.Text == note.Text) != null;
            }
        }

        public bool ContainsNote(DateTime moment, string targetComputer, string text)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Notes.FirstOrDefault(n =>
                    n.Moment == moment &&
                    n.TargetComputer == targetComputer &&
                    n.Text == text) != null;
            }
        }

        public bool ContainsNote(string noteView)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                var parts = noteView.Split('@');
                if (parts.Length != 3)
                {
                    return false;
                }

                var moment = parts[0] != "" ?
                    DateTime.ParseExact(parts[0], "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture) :
                    new DateTime(1970, 1, 1);
                var text = parts[1];
                var targetComputer = parts[2];

                return context.Notes.FirstOrDefault(n =>
                    n.Moment.Equals(moment) &&
                    n.TargetComputer == targetComputer &&
                    n.Text == text) != null;
            }
        }        

        public List<Note> GetFiltredNotes(Func<Note, bool> condition)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Notes.ToList().Where(condition).ToList();
            }
        }

        public Note GetFirstFiltredNote(Func<Note, bool> condition)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Notes.ToList().FirstOrDefault(condition);
            }
        }

        public Note GetNote(int noteId)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Notes.FirstOrDefault(n => n.NoteId == noteId);
            }
        }

        public Note FindNote(string text)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Notes.FirstOrDefault(n => n.Text == text);
            }
        }

        public void AddNote(Note note)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                note.NoteId = 0;

                context.Notes.Add(note);
                context.SaveChanges();
            }
        }

        public void UpdateNote(Note note)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                var curNote = GetNote(note.NoteId);

                curNote.Text = note.Text;
                curNote.Moment = note.Moment;
                curNote.TargetComputer = note.TargetComputer;

                context.SaveChanges();
            }
        }

        public void RemoveNote(int noteId)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                var note = context.Notes.FirstOrDefault(n => n.NoteId == noteId);

                context.Notes.Remove(note);
                context.SaveChanges();
            }
        }

        public void AddNotesRange(IEnumerable<Note> noteList)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                foreach (var note in noteList)
                {
                    note.NoteId = 0;
                    context.Notes.Add(note);
                }

                context.SaveChanges();
            }
        }        
        #endregion
        
        #region PhonesPepository
        public List<Phone> GetAllPhones()
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Phones.ToList();
            }
        }

        public List<Phone> GetFiltredPhones(Func<Phone, bool> condition)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Phones.ToList().Where(condition).ToList();
            }
        }

        public Phone GetFirstFiltredPhone(Func<Phone, bool> condition)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Phones.ToList().FirstOrDefault(condition);
            }
        }

        public Phone GetPhone(int phoneId)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Phones.FirstOrDefault(p => p.PhoneId == phoneId);
            }
        }

        public Phone FindPhone(string name)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                return context.Phones.FirstOrDefault(p => p.Name == name);
            }
        }

        public void AddPhone(Phone phone)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                phone.PhoneId = 0;

                context.Phones.Add(phone);
                context.SaveChanges();
            }
        }

        public void UpdatePhone(Phone phone)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                var curPhone = GetPhone(phone.PhoneId);

                curPhone.Name = phone.Name;
                curPhone.Number = phone.Number;

                context.SaveChanges();
            }
        }

        public void RemovePhone(int phoneId)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                var phone = context.Phones.FirstOrDefault(p => p.PhoneId == phoneId);

                context.Phones.Remove(phone);
                context.SaveChanges();
            }
        }

        public void AddPhonesRange(IEnumerable<Phone> phoneList)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                foreach (var phone in phoneList)
                {
                    phone.PhoneId = 0;
                    context.Phones.Add(phone);
                }

                context.SaveChanges();
            }
        }
        #endregion
    }    
}
