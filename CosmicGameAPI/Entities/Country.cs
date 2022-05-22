namespace CosmicGameAPI.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string ActualTimeZone { get; set; }
        public string NameOfTimeZone { get; set; }
        public int TotalTimeZone { get; set; }
        public int CountOfTimeZone { get; set; }
        public string AreasCovered { get; set; }
        public string YearFrom { get; set; }
        public decimal IsDST { get; set; }
        public DateTime DSTFromTime { get; set; }
        public DateTime DSTToTime { get; set; }
        public string TimeZoneValue { get; set; }
    }
}
