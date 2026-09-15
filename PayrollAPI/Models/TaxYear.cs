namespace PayrollAPI.Models
{
    public class TaxYear

    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public decimal PrimaryRebate { get; set; }
        public decimal UifRate { get; set; }
        public decimal UifCeiling { get; set; }
        public decimal SdlRate { get; set; }
        public decimal SdlAnnualThreshold { get; set; }

        public List<TaxBracket> Brackets { get; set; } = new();
    }
}
