﻿using System;

namespace UchOtd.DomainClasses
{
    public class Note
    {
        public Note()
        {
        }

        public Note(string text, DateTime moment, string targetComputer)
        {
            Text = text;
            Moment = moment;
            TargetComputer = targetComputer;
        }

        public int NoteId { get; set; }
        public string Text { get; set; }
        public DateTime Moment { get; set; }
        public string TargetComputer { get; set; }
    }
}
