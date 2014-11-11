namespace UchOtd.DomainClasses
{
    public class Phone
    {
        public Phone()
        {
        }

        public Phone(string name, string number)
        {
            Name = name;
            Number = number;
        }

        public int PhoneId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }
}
