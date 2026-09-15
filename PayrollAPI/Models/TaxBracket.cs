namespace PayrollAPI.Models
{
    public class TaxBracket


    {
        public int Id { get; set; }
        public decimal Threshold { get; set; }
        public decimal BaseTax { get; set; }
        public decimal Rate { get; set; }

        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; } = null!;
    }
}
