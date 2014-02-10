using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UchOtd.DomainClasses;

namespace UchOtd.Views
{
    public class NoteView
    {
        public int NoteId { get; set; }
        public string Text { get; set; }        

        public static string format = "dd.MM.yyyy H:mm:ss";

        public static List<NoteView> FromNoteList(List<Note> list)
        {
            var result = new List<NoteView>();

            foreach (var note in list)
            {
                result.Add(new NoteView { 
                    NoteId = note.NoteId,
                    Text = note.Text + "@" +
                           note.Moment.ToString(format)
                });
            }

            return result;
        }

        public static string ViewFromNote(Note note)
        {
            return note.Text + "@" + 
                   note.Moment.ToString(format);        
        }
    }
}
