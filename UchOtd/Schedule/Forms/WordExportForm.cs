using System.Threading;
using System.Threading.Tasks;
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

        CancellationTokenSource _tokenSource;
        CancellationToken _cToken;

        public WordExportForm(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void WordExportForm_Load(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();

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
                Bounds = new Rectangle(210, 10 + (faculties.Count + 1)*25, 150, 25)
            };
            Controls.Add(future);

            var weekFiltered = new CheckBox
            {
                Parent = this,
                Name = "weekFiltered",
                Text = "Фильтр/неделя",
                Checked = false,
                Bounds = new Rectangle(360, 10 + (faculties.Count + 1) * 25, 120, 25)
            };
            Controls.Add(weekFiltered);


            var weekFilter = new ComboBox
            {
                Parent = this,
                Name = "weekFilter",
                Bounds = new Rectangle(480, 10 + (faculties.Count + 1) * 25, 100, 25)
            };
            for (int i = 1; i <= 18; i++)
            {
                weekFilter.Items.Add(i);
            }
            Controls.Add(weekFilter);

            Height = (faculties.Count + 1)*25 + 100;
        }


        private void CheckBoxClicked(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox == null) return;

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

        private async void ExportButtonClick(object sender, EventArgs e)
        {
            var button = (sender as Button);
            if (button == null) return;

            if (button.Text == "Экспорт")
            {
                _cToken = _tokenSource.Token;

                button.Text = "";
                button.Image = UchOtd.Properties.Resources.Loading;

                var lesson8090Length = ((CheckBox)Controls.Find("cb90", false).First()).Checked ? 90 : 80;
                var futureDatesOnly = ((CheckBox)Controls.Find("cbfuture", false).First()).Checked;
                var weekFilteredF = ((CheckBox)Controls.Find("weekFiltered", false).First()).Checked;
                int weekFilterF = -1;
                int.TryParse(((ComboBox)Controls.Find("weekFilter", false).First()).Text, out weekFilterF);

                try
                {
                    await Task.Run(() => WordExport.ExportCustomSchedule(
                                _choice, _repo, "Расписание.docx", false, false,
                                lesson8090Length, 6, MainEditForm.SchoolHeader, futureDatesOnly, weekFilteredF, weekFilterF, _cToken), _cToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                _tokenSource.Cancel();
            }

            button.Image = null;
            button.Text = "Экспорт";
        }
    }
}
