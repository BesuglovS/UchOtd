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
        Dictionary<int, List<int>> choice;

        public WordExportForm(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void WordExportForm_Load(object sender, EventArgs e)
        {
            choice = new Dictionary<int, List<int>>();

            var faculties = _repo.GetAllFaculties();
            for (int i = 0; i < faculties.Count; i++)                
            {
                choice.Add(faculties[i].FacultyId, new List<int>());

                for (int j = 1; j <=7 ; j++)                
                {   
                    var checkBox = new CheckBox();
                    checkBox.Parent = this;
                    checkBox.Name = "cb_" + faculties[i].FacultyId + "_" + j;
                    checkBox.Text = faculties[i].Letter + " " + Constants.DowLocal[j].Substring(0,3);
                    checkBox.Bounds = new Rectangle(-50 + j * 80, 10 + i * 25, 75, 25);
                    this.Controls.Add(checkBox);

                    checkBox.Click += CheckBoxClicked;
                }
            }

            var wordButton = new Button();
            wordButton.Parent = this;
            wordButton.Name = "ExportButton";
            wordButton.Text = "Экспорт";
            wordButton.Bounds = new Rectangle(30, 10 + (faculties.Count+1) * 25, 75, 25);
            this.Controls.Add(wordButton);

            wordButton.Click += ExportButtonClick;

            var checkBox90 = new CheckBox();
            checkBox90.Parent = this;
            checkBox90.Name = "cb90";
            checkBox90.Text = "90 минут";
            checkBox90.Checked = false;
            checkBox90.Bounds = new Rectangle(130, 10 + (faculties.Count + 1) * 25, 75, 25);
            this.Controls.Add(checkBox90);            
        }


        private void CheckBoxClicked(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            var split = checkBox.Name.Split('_');
            var facultyId = int.Parse(split[1]);
            var dow = int.Parse(split[2]);

            if (((CheckBox)sender).Checked)
            {
                choice[facultyId].Add(dow);
            }
            else
            {
                choice[facultyId].Remove(dow);
            }
        }

        private void ExportButtonClick(object sender, EventArgs e)
        {
            WordExport.ExportCustomSchedule(choice, _repo, "Расписание.docx", false, false, ((CheckBox)Controls.Find("cb90", false).First()).Checked ? 90 : 80, 6, MainEditForm.SchoolHeader);
        }
    }
}
