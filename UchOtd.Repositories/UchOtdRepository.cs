using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UchOtd.DataLayer.Migrations;
using UchOtd.DomainClasses;

namespace UchOtd.Repositories
{
    public class UchOtdRepository : IDisposable
    {
        public string ConnectionString { get; set; }

        public UchOtdRepository(string connectionString = "Name=UchOtdConnection")
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

        public void RecreateDB()
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
        private void Dispose(bool b)
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
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
                if (context.Notes.FirstOrDefault(n =>
                        n.Moment == note.Moment &&
                        n.TargetComputer == note.TargetComputer &&
                        n.Text == note.Text) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ContainsNote(DateTime Moment, string TargetComputer, string Text)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                if (context.Notes.FirstOrDefault(n =>
                        n.Moment == Moment &&
                        n.TargetComputer == TargetComputer &&
                        n.Text == Text) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ContainsNote(string noteView)
        {
            using (var context = new UchOtdContext(ConnectionString))
            {
                DateTime Moment;
                var parts = noteView.Split('@');
                if (parts.Length != 3)
                {
                    return false;
                }

                if (parts[0] != "")
                {
                    Moment = DateTime.ParseExact(parts[0], "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    Moment = new DateTime(1970, 1, 1);
                }
                var Text = parts[1];
                var TargetComputer = parts[2];

                var notes = context.Notes.ToList();

                if (context.Notes.FirstOrDefault(n =>
                        n.Moment.Equals(Moment) &&
                        n.TargetComputer == TargetComputer &&
                        n.Text == Text) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                var note = GetNote(noteId);

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
                var phone = GetPhone(phoneId);

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
