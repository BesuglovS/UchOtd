using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using UchOtd.Schedule.Views.DBListViews;

namespace UchOtd.Schedule.Forms.DBLists
{
    public partial class StudentList : Form
    {
        private readonly ScheduleRepository _repo;

        public StudentList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;

            var groups = _repo.StudentGroups.GetAllStudentGroups().OrderBy(sg => sg.Name).ToList();
            studentGroups.DataSource = groups;
            studentGroups.DisplayMember = "Name";
            studentGroups.ValueMember = "StudentGroupId";
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView(string fFilter = "")
        {
            List<Student> studentList;
            if ((studentGroups.SelectedIndex != -1) && (studentGroups.SelectedValue is int))
            {
                var studentIds = _repo
                    .StudentsInGroups
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == (int)studentGroups.SelectedValue)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();
                studentList = _repo.Students.GetAllStudents().Where(s => studentIds.Contains(s.StudentId)).OrderBy(s => s.Expelled).ThenBy(s => s.F).ToList();
            }
            else
            {
                studentList = _repo.Students.GetAllStudents().OrderBy(s => s.Expelled).ThenBy(s => s.F).ToList();
            }

            if (fFilter != "")
            {
                studentList = studentList.Where(s => s.F.ToLower().Contains(fFilter.ToLower())).ToList();
            }

            var studentView = StudentView.StudentsToView(studentList);
            studentCombo.DataSource = studentView;
            studentCombo.DisplayMember = "Fio";
            studentCombo.ValueMember = "StudentId";


            StudentListView.DataSource = studentView;
            
            StudentListView.Columns["StudentId"].Visible = false;
            StudentListView.Columns["Fio"].Width = 200;
            StudentListView.Columns["Fio"].HeaderText = "Ф.И.О.";
            StudentListView.Columns["ZachNumber"].Width = 80;
            StudentListView.Columns["ZachNumber"].HeaderText = "№ зачётки";
            StudentListView.Columns["BirthDate"].Width = 80;
            StudentListView.Columns["BirthDate"].HeaderText = "Дата рождения";
            StudentListView.Columns["Address"].Width = 150;
            StudentListView.Columns["Address"].HeaderText = "Адрес";
            StudentListView.Columns["Phone"].Width = 80;
            StudentListView.Columns["Phone"].HeaderText = "Телефон";
            StudentListView.Columns["Orders"].Width = 150;
            StudentListView.Columns["Orders"].HeaderText = "Приказы";
            StudentListView.Columns["Starosta"].Width = 50;
            StudentListView.Columns["Starosta"].HeaderText = "Староста";
            StudentListView.Columns["NFactor"].Width = 50;
            StudentListView.Columns["NFactor"].HeaderText = "Наяновец";
            StudentListView.Columns["PaidEdu"].Width = 50;
            StudentListView.Columns["PaidEdu"].HeaderText = "Платное обучение";
            StudentListView.Columns["Expelled"].Width = 50;
            StudentListView.Columns["Expelled"].HeaderText = "Отчислен";
            
            StudentListView.ClearSelection();
        }

        private void StudentListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var studentView = ((List<StudentView>)StudentListView.DataSource)[e.RowIndex];
            var student = _repo.Students.GetStudent(studentView.StudentId);

            FBox.Text = student.F;
            IBox.Text = student.I;
            OBox.Text = student.O;
            ZachNumber.Text = student.ZachNumber;
            BirthDate.Value = student.BirthDate;
            Address.Text = student.Address;
            Phone.Text = student.Phone;
            Starosta.Checked = student.Starosta;
            NFactor.Checked = student.NFactor;
            PayForThis.Checked = student.PaidEdu;
            Expelled.Checked = student.Expelled;
            OrderList.Text = student.Orders;
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (checkZachNumberDistinction.Checked)
            {
                if (_repo.Students.FindStudent(ZachNumber.Text) != null)
                {
                    MessageBox.Show("Такой студент уже есть.");
                    return;
                }
            }

            var newStudent = new Student { 
                F = FBox.Text, 
                I = IBox.Text, 
                O = OBox.Text, 
                Address = Address.Text, 
                BirthDate = BirthDate.Value, 
                Expelled = Expelled.Checked, 
                NFactor = NFactor.Checked, 
                Orders = OrderList.Text,
                PaidEdu = PayForThis.Checked,
                Phone = Phone.Text,
                Starosta = Starosta.Checked,
                ZachNumber = ZachNumber.Text
            };

            _repo.Students.AddStudent(newStudent);

            RefreshView();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (StudentListView.SelectedCells.Count > 0)
            {
                var studentView = ((List<StudentView>)StudentListView.DataSource)[StudentListView.SelectedCells[0].RowIndex];
                var student = _repo.Students.GetStudent(studentView.StudentId);

                student.F = FBox.Text;
                student.I = IBox.Text;
                student.O = OBox.Text;
                student.Address = Address.Text;
                student.BirthDate = BirthDate.Value;
                student.Expelled = Expelled.Checked;
                student.NFactor = NFactor.Checked;
                student.Orders = OrderList.Text;
                student.PaidEdu = PayForThis.Checked;
                student.Phone = Phone.Text;
                student.Starosta = Starosta.Checked;
                student.ZachNumber = ZachNumber.Text;

                _repo.Students.UpdateStudent(student);

                RefreshView();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (StudentListView.SelectedCells.Count > 0)
            {
                var studentIds = new List<int>();

                for (int i = 0; i < StudentListView.SelectedCells.Count; i++)
                {
                    studentIds.Add(((List<StudentView>)StudentListView.DataSource)[StudentListView.SelectedCells[i].RowIndex].StudentId);
                }

                var ids = studentIds;
                if (_repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => ids.Contains(sig.Student.StudentId)).Count > 0)
                {
                    MessageBox.Show("Студенты состоят в группах.");
                    return;
                }

                studentIds = studentIds.OrderBy(a => a).Distinct().ToList();
                for (int i = 0; i < studentIds.Count; i++)
                {
                    _repo.Students.RemoveStudent(studentIds[i]);
                }

                RefreshView();
            }
        }

        private void deletewithlessons_Click(object sender, EventArgs e)
        {
            if (StudentListView.SelectedCells.Count > 0)
            {
                var studentIds = new List<int>();

                for (int i = 0; i < StudentListView.SelectedCells.Count; i++)
                {
                    studentIds.Add(((List<StudentView>)StudentListView.DataSource)[StudentListView.SelectedCells[i].RowIndex].StudentId);
                }

                var ids = studentIds;

                studentIds = studentIds.OrderBy(a => a).Distinct().ToList();
                for (int i = 0; i < studentIds.Count; i++)
                {
                    var studentGroupLinks = _repo.StudentsInGroups.GetFiltredStudentsInGroups(sig => sig.Student.StudentId == studentIds[i]);

                    foreach (var sig in studentGroupLinks)
                    {
                        _repo.StudentsInGroups.RemoveStudentsInGroups(sig.StudentsInGroupsId);
                    }

                    _repo.Students.RemoveStudent(studentIds[i]);
                }

                RefreshView();
            }
        }

        private void studentCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {                
                var sViews = ((List<StudentView>)StudentListView.DataSource);
                var view = sViews.FirstOrDefault(v => v.StudentId == (int)studentCombo.SelectedValue);
                if (view != null)
                {
                    var index = sViews.IndexOf(view);
                    StudentListView.Rows[index].Cells[0].Selected = true;
                    StudentListView_CellClick(this, new DataGridViewCellEventArgs(0, index));
                }
            }
        }

        private void studentCombo_TextChanged(object sender, EventArgs e)
        {

        }

        private void studentCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sViews = ((List<StudentView>)StudentListView.DataSource);
            if (sViews != null)
            {
                var view = sViews.FirstOrDefault(v => v.StudentId == (int)studentCombo.SelectedValue);
                if (view != null)
                {
                    var index = sViews.IndexOf(view);
                    StudentListView.Rows[index].Cells[0].Selected = true;
                    StudentListView_CellClick(this, new DataGridViewCellEventArgs(0, index));
                }
            }
        }

        private void studentGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void showAll_Click(object sender, EventArgs e)
        {
            studentGroups.SelectedIndex = -1;
            RefreshView();
        }

        private void FBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                add.PerformClick();
            }
        }

        private void FBox_TextChanged(object sender, EventArgs e)
        {
            var fio = FBox.Text;
            var split = fio.Split(' ', '\t');
            if (split.Count() != 3)
            {
                return;
            }
            
            var f = split[0];
            var i = split[1];
            var o = split[2];
            FBox.Text = f[0] + f.Substring(1).ToLower();
            IBox.Text = i;
            OBox.Text = o;
        }

        private void Filter_Click(object sender, EventArgs e)
        {
            RefreshView(FBox.Text);
        }

        private void ImportStudentListFromJson_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Import Student List",
                Filter = "All files|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImportStudentListFromJsonJsonFile(ofd.FileName);
            }
        }

        private void ImportStudentListFromJsonJsonFile(string fileName)
        {
            var sr = new StreamReader(fileName);
            var studentsJsonString = sr.ReadLine();
            sr.Close();

            List<Student> importedList = JsonConvert.DeserializeObject<List<Student>>(studentsJsonString);

            foreach (var student in importedList)
            {
                _repo.Students.AddStudent(student);
            }
        }

        private void jsonExport_Click(object sender, EventArgs e)
        {
            var studentList = new List<Student>();

            if (StudentListView.SelectedCells.Count > 0)
            {
                var rowList = new List<int>();
                var cells = (List<StudentView>) StudentListView.DataSource;

                for (int i = 0; i < StudentListView.SelectedCells.Count; i++)
                {
                    var cell = StudentListView.SelectedCells[i];

                    if (!rowList.Contains(cell.RowIndex))
                    {
                        var studentView = ((List<StudentView>) StudentListView.DataSource)[cell.RowIndex];
                        var student = _repo.Students.GetStudent(studentView.StudentId);
                        studentList.Add(student);

                        rowList.Add(cell.RowIndex);
                    }
                }
            }
            else
            {
                studentList = _repo.Students.GetAllStudents();
            }

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Json files|*.json",
                Title = "Save student list"
            };
            dialog.ShowDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var sw = new StreamWriter(dialog.FileName);
                var output = JsonConvert.SerializeObject(studentList);
                sw.WriteLine(output);
                sw.Close();
            }
        }

        private void ParsePhoneMultiline_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                var text = Clipboard.GetText(TextDataFormat.UnicodeText);
                Phone.Text = FormatPhones(text);
            }
        }

        private string FormatPhones(string text)
        {
            var split = text.Split('\n')
                .Select(str => str.Replace("\r", ""))
                .Where(str => str != "")
                .ToList();
            if (split.Count > 0)
            {
                var sb = new StringBuilder(split[0]);
                for (int i = 1; i < split.Count; i++)
                {
                    if (!split[i].StartsWith("(") && !char.IsLetter(split[i][0]))
                    {
                        sb.Append("; ");
                    }
                    else
                    {
                        sb.Append(" ");
                    }

                    sb.Append(split[i]);
                }

                var phoneText = sb.ToString();

                return phoneText;
            }

            return "";
        }

        private void ParseOneStudent_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                var text = Clipboard.GetText(TextDataFormat.UnicodeText);
                var split = text.Split('\t').ToList();
                if (split.Count != 6)
                {
                    return;
                }
                var enterIndex = split[0].IndexOf("\r\n", StringComparison.Ordinal);
                var F = split[0].Substring(0, enterIndex).Trim();
                var IO = split[0].Substring(enterIndex+2, split[0].Length - enterIndex - 2);
                var IOList = IO.Split(new[] {' '}, 2).ToList();
                F = char.ToUpper(F.First()) + F.Substring(1).ToLower();

                FBox.Text = F;
                IBox.Text = IOList[0].Trim();
                OBox.Text = IOList[1].Trim();
                ZachNumber.Text = split[1].Trim();
                BirthDate.Value = DateTime.ParseExact(split[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                Address.Text = split[3].Trim();
                Phone.Text = FormatPhones(split[4]);
                OrderList.Text = split[5].Replace("\r\n", "");
            }
        }

        public Student ParseStudentInfo(List<String> split)
        {
            if (split.Count != 6)
            {
                return null;
            }
            var enterIndex = split[0].IndexOf("\r\n", StringComparison.Ordinal);
            var F = split[0].Substring(0, enterIndex).Trim();
            var IO = split[0].Substring(enterIndex + 2, split[0].Length - enterIndex - 2).Replace("\r\n", "").Replace("староста", "");
            var IOList = IO.Split(new[] { ' ' }, 2).ToList();
            F = char.ToUpper(F.First()) + F.Substring(1).ToLower();

            DateTime birthDate = new DateTime(1900, 1, 1);
            try
            {
                if (split[2] != "")
                {
                    birthDate = DateTime.ParseExact(split[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            var student = new Student
            {
                F = F,
                I = IOList[0].Trim(),
                O = IOList[1].Trim(),
                ZachNumber = split[1].Trim(),
                BirthDate = birthDate,
                Address = split[3].Trim(),
                Phone = FormatPhones(split[4]),
                Orders = split[5].Trim('\r').Trim('\n').Trim('\r')
            };

            return student;
        }

        bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }

            return true;
        }

        private void ParseIsertStudentList_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                var text = Clipboard.GetText(TextDataFormat.UnicodeText);
                var split = text.Split('\t').ToList();
                int state = 1;
                for (int i = 5; i < split.Count-1; i += 5)
                {
                    var tokenIndex = -1;
                    var tokenSplit = split[i].Split(new[] {"\r\n"}, StringSplitOptions.None).Where(t => t != "").ToList();
                    for (int j = tokenSplit.Count-1; j >= 0 ; j--)
                    {
                        if (IsAllUpper(tokenSplit[j].Split(' ')[0].Trim()))
                        {
                            tokenIndex = j;
                            break;
                        }
                    }
                    var order = (tokenIndex == 1) ? tokenSplit[0] : tokenSplit.GetRange(0, tokenIndex - 1).Aggregate((a, b) => a + "\r\n" + b);
                    var fio = ((tokenSplit.Count - tokenIndex) == 1) ? 
                        tokenSplit[tokenIndex] : 
                        tokenSplit.GetRange(tokenIndex, tokenSplit.Count - tokenIndex).Aggregate((a, b) => a + "\r\n" + b);

                    split[i] = order;
                    split.Insert(i+1, fio);
                    i++;
                }

                var studentsSplit = splitList(split);

                for (int i = 0; i < studentsSplit.Count; i++)
                {
                    var student = ParseStudentInfo(studentsSplit[i]);
                    student.Expelled = importExpelled.Checked;
                    _repo.Students.AddStudent(student);
                }
            }
        }

        private static List<List<string>> splitList(List<string> locations, int nSize = 6)
        {
            var list = new List<List<string>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }

            return list;
        }
    }
}
