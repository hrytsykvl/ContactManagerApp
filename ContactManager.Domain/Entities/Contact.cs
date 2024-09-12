namespace ContactManager.Domain.Entities
{
    public class Contact
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool Married { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
