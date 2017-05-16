namespace UchOtd.Schedule.MainImport
{
    public class Teacher
    {
        public Teacher()
        {
        }

        public Teacher(string fio, string phone)
        {
            FIO = fio;
            Phone = phone;
        }

        public int TeacherId { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
    }
}
