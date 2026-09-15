using System.ComponentModel.DataAnnotations;

namespace PayrollAPI.Dtos
{
    public class CreateEmployeeDto
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal GrossMonthlySalary { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public bool UifExempt { get; set; }
    }
}
