using System.ComponentModel.DataAnnotations;

namespace ContactManager.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]+ [A-Za-z]+$", ErrorMessage = "Name should only contain letters.")]
        public string Name { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }
        public bool Married { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
        public decimal Salary { get; set; }
    }
}
