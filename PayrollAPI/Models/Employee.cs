namespace PayrollAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal GrossMonthlySalary { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool UifExempt { get; set; }
    }
}
