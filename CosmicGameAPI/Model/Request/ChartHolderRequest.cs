namespace CosmicGameAPI.Model.Request
{
    public class ChartHolderRequest
    {
        public string ChartHolderID { get; set; }
        public int UserID { get; set; }
        public string ChildName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public bool Gender { get; set; }
        public string BirhPlace { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string DOB { get; set; }
        public string TimeZone { get; set; }
        public string Ayanamsa { get; set; }
        public string HouseSystem { get; set; }
        public string AynamsaPolicy { get; set; }
        public string Email { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string LatLocator { get; set; }
        public string LngLocator { get; set; }
        public int? ChartType { get; set; }
        public bool BirthTimeRectified { get; set; }
        public TimeSpan? RectifiedTOB { get; set; }
        public bool IsAdd { get; set; }
    }
}
