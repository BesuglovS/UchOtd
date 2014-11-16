using Schedule.Constants;
using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UchOtd.Core;

namespace UchOtd.Schedule.Forms
{
    public partial class WordExportForm : Form
    {
        private readonly ScheduleRepository _repo;
        Dictionary<int, List<int>> _choice;

        public WordExportForm(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void WordExportForm_Load(object sender, EventArgs e)
        {
            _choice = new Dictionary<int, List<int>>();

            var faculties = _repo.Faculties.GetAllFaculties();
            for (int i = 0; i < faculties.Count; i++)                
            {
                _choice.Add(faculties[i].FacultyId, new List<int>());

                for (int j = 1; j <=7 ; j++)                
                {   
                    var checkBox = new CheckBox
                    {
                        Parent = this,
                        Name = "cb_" + faculties[i].FacultyId + "_" + j,
                        Text = faculties[i].Letter + " " + Constants.DowLocal[j].Substring(0, 3),
                        Bounds = new Rectangle(-50 + j*80, 10 + i*25, 75, 25)
                    };
                    Controls.Add(checkBox);

                    checkBox.Click += CheckBoxClicked;
                }
            }

            var wordButton = new Button
            {
                Parent = this,
                Name = "ExportButton",
                Text = "Экспорт",
                Bounds = new Rectangle(30, 10 + (faculties.Count + 1)*25, 75, 25)
            };
            Controls.Add(wordButton);

            wordButton.Click += ExportButtonClick;

            var checkBox90 = new CheckBox
            {
                Parent = this,
                Name = "cb90",
                Text = "90 минут",
                Checked = false,
                Bounds = new Rectangle(130, 10 + (faculties.Count + 1)*25, 75, 25)
            };
            Controls.Add(checkBox90);

            var future = new CheckBox
            {
                Parent = this,
                Name = "cbFuture",
                Text = "только будущие даты",                
                Checked = true,
                Bounds = new Rectangle(230, 10 + (faculties.Count + 1)*25, 200, 25)
            };
            Controls.Add(future);  
        }


        private void CheckBoxClicked(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            var split = checkBox.Name.Split('_');
            var facultyId = int.Parse(split[1]);
            var dow = int.Parse(split[2]);

            if (((CheckBox)sender).Checked)
            {
                _choice[facultyId].Add(dow);
            }
            else
            {
                _choice[facultyId].Remove(dow);
            }
        }

        private void ExportButtonClick(object sender, EventArgs e)
        {
            WordExport.ExportCustomSchedule(
                _choice, _repo, "Расписание.docx", false, false, 
                ((CheckBox)Controls.Find("cb90", false).First()).Checked ? 90 : 80, 6, MainEditForm.SchoolHeader,
                ((CheckBox)Controls.Find("cbfuture", false).First()).Checked);
        }
    }
}
