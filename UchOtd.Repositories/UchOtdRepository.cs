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
        private readonly UchOtdContext _context = new UchOtdContext();

        public UchOtdRepository()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UchOtdContext, Configuration>());
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void RecreateDB()
        {
            _context.Database.Delete();
            _context.Database.CreateIfNotExists();
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
            _context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #region NotesPepository
        public List<Note> GetAllNotes()
        {
            return _context.Notes.ToList();
        }

        public bool ContainsNote(Note note)
        {
            if (_context.Notes.FirstOrDefault(n => 
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

        public bool ContainsNote(DateTime Moment, string TargetComputer, string Text)
        {
            if (_context.Notes.FirstOrDefault(n =>
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

        public bool ContainsNote(string noteView)
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

            var notes = _context.Notes.ToList();

            if (_context.Notes.FirstOrDefault(n =>
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

        public List<Note> GetFiltredNotes(Func<Note, bool> condition)
        {
            return _context.Notes.ToList().Where(condition).ToList();
        }

        public Note GetFirstFiltredNote(Func<Note, bool> condition)
        {
            return _context.Notes.ToList().FirstOrDefault(condition);
        }

        public Note GetNote(int noteId)
        {
            return _context.Notes.FirstOrDefault(n => n.NoteId == noteId);
        }

        public Note FindNote(string text)
        {
            return _context.Notes.FirstOrDefault(n => n.Text == text);
        }

        public void AddNote(Note note)
        {
            note.NoteId = 0;

            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public void UpdateNote(Note note)
        {
            var curNote = GetNote(note.NoteId);

            curNote.Text = note.Text;
            curNote.Moment = note.Moment;
            curNote.TargetComputer = note.TargetComputer;            

            _context.SaveChanges();
        }

        public void RemoveNote(int noteId)
        {
            var note = GetNote(noteId);

            _context.Notes.Remove(note);
            _context.SaveChanges();
        }

        public void AddNotesRange(IEnumerable<Note> noteList)
        {
            foreach (var note in noteList)
            {
                note.NoteId = 0;
                _context.Notes.Add(note);
            }

            _context.SaveChanges();
        }        
        #endregion
        
        #region PhonesPepository
        public List<Phone> GetAllPhones()
        {
            return _context.Phones.ToList();
        }

        public List<Phone> GetFiltredPhones(Func<Phone, bool> condition)
        {
            return _context.Phones.ToList().Where(condition).ToList();
        }

        public Phone GetFirstFiltredPhone(Func<Phone, bool> condition)
        {
            return _context.Phones.ToList().FirstOrDefault(condition);
        }

        public Phone GetPhone(int phoneId)
        {
            return _context.Phones.FirstOrDefault(p => p.PhoneId == phoneId);
        }

        public Phone FindPhone(string name)
        {
            return _context.Phones.FirstOrDefault(p => p.Name == name);
        }

        public void AddPhone(Phone phone)
        {
            phone.PhoneId = 0;

            _context.Phones.Add(phone);
            _context.SaveChanges();
        }

        public void UpdatePhone(Phone phone)
        {
            var curPhone = GetPhone(phone.PhoneId);

            curPhone.Name = phone.Name;
            curPhone.Number = phone.Number;

            _context.SaveChanges();
        }

        public void RemovePhone(int phoneId)
        {
            var phone = GetPhone(phoneId);

            _context.Phones.Remove(phone);
            _context.SaveChanges();
        }

        public void AddPhonesRange(IEnumerable<Phone> phoneList)
        {
            foreach (var phone in phoneList)
            {
                phone.PhoneId = 0;
                _context.Phones.Add(phone);
            }

            _context.SaveChanges();
        }
        #endregion
    
    }    
}
