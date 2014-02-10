using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UchOtd.Core;
using UchOtd.DomainClasses;
using UchOtd.Repositories;
using UchOtd.Views;

namespace UchOtd.Forms.Notes
{
    public partial class Notes : Form
    {
        readonly UchOtdRepository _UOrepo;
        private readonly TaskScheduler _uiScheduler;           

        public Notes(UchOtdRepository UOrepo)
        {
            InitializeComponent();

            _UOrepo = UOrepo;

            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        private void Notes_Load(object sender, EventArgs e)
        {
            RefreshView();

            noteMoment.Value = DateTime.Now;
        }

        private void RefreshView()
        {
            var notes = _UOrepo                
                .GetFiltredNotes(n =>
                    n.Text.ToUpper().Contains(filter.Text.ToUpper()) ||
                    n.Text.ToUpper().Contains(LayoutSupport.ConvertEnRU(filter.Text.ToLower()).ToUpper()))                
                .ToList();

            view.DataSource = notes;

            view.Columns["NoteId"].Visible = false;

            view.Columns["Text"].HeaderText = "Текст заметки";
            view.Columns["Text"].Width = 300;

            view.Columns["Moment"].HeaderText = "Дата + время";
            view.Columns["Moment"].Width = 100;

            view.Columns["TargetComputer"].Visible = false;
            view.Columns["TargetComputer"].HeaderText = "Целевой компьютер";
            view.Columns["TargetComputer"].Width = 100;
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (currentDateTime.Checked)
            {
                noteMoment.Value = DateTime.Now;
            }

            var newNote = new Note() {
                Text = noteText.Text,
                Moment = noteMoment.Value,
                TargetComputer = TargetComputer.Text
            };

            _UOrepo.AddNote(newNote);

            RefreshView();
        }

        private void view_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            var note = ((List<Note>)view.DataSource)[e.RowIndex];

            noteText.Text = note.Text;
            noteMoment.Value = note.Moment;
            TargetComputer.Text = note.TargetComputer;
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (view.SelectedCells.Count > 0)
            {
                var note = ((List<Note>)view.DataSource)[view.SelectedCells[0].RowIndex];

                note.Text = noteText.Text;
                note.Moment = noteMoment.Value;
                note.TargetComputer = TargetComputer.Text;

                _UOrepo.UpdateNote(note);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (view.SelectedCells.Count > 0)
            {
                var note = ((List<Note>)view.DataSource)[view.SelectedCells[0].RowIndex];

                _UOrepo.RemoveNote(note.NoteId);

                noteText.Text = "";
                noteMoment.Value = DateTime.Now;
                TargetComputer.Text = "";

                RefreshView();
            }
        }

        private void filter_TextChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void showAll_Click(object sender, EventArgs e)
        {
            filter.Text = "";
        }
    }

    public static class LocalUtilities
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
