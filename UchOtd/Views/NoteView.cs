using System.Collections.Generic;
using System.Linq;
using UchOtd.DomainClasses;

namespace UchOtd.Views
{
    public class NoteView
    {
        public int NoteId { get; set; }
        public string Text { get; set; }        

        public static string Format = "dd.MM.yyyy H:mm:ss";

        public static List<NoteView> FromNoteList(List<Note> list)
        {
            return list.Select(note => new NoteView
            {
                NoteId = note.NoteId, Text = note.Text + "@" + note.Moment.ToString(Format)
            }).ToList();
        }

        public static string ViewFromNote(Note note)
        {
            return note.Text + "@" + 
                   note.Moment.ToString(Format);        
        }
    }
}
