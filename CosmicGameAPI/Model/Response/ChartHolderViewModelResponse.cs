using System.ComponentModel;

namespace CosmicGameAPI.Model.Response
{
    public class ChartHolderViewModelResponse
    {
        public int ChartHolderId { get; set; }

        public string UserId { get; set; }

        [DisplayName("Child name")]
        public string ChildName { get; set; }

        [DisplayName("Father name")]
        public string FatherName { get; set; }

        [DisplayName("Mother name")]
        public string MotherName { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Aynamsa Policy")]
        public string AynamsaPolicy { get; set; }


        [DisplayName("Birth place")]
        public string BirthPlace { get; set; }


        [DisplayName("City")]
        public string City { get; set; }


        [DisplayName("State")]
        public string State { get; set; }


        [DisplayName("Country")]
        public string Country { get; set; }

        [DisplayName("Postal code")]
        public string PostalCode { get; set; }

        [DisplayName("Birth date")]

        public DateTime DOB { get; set; }

        [DisplayName("Birth time")]
        public DateTime TOB { get; set; }


        [DisplayName("Timezone")]
        public string TimeZone { get; set; }

        [DisplayName("Chart Type")]
        public int ChartType { get; set; }

        [DisplayName("Horarary Number Selection Type")]
        public string HorararyNumberSelectionType { get; set; }

        [DisplayName("HorararyNumberSelection")]
        public int? HorararyNumberSelection { get; set; }

        [DisplayName("Email")]
        public string UserEmailId { get; set; }

        [DisplayName("Ayanamsa")]
        public string Ayanamsa { get; set; }

        [DisplayName("House system")]
        public string HouseSystem { get; set; }

        [DisplayName("Dms Location")]
        public DmsLocation DmsLocation { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string LatLocator { get; set; }

        public string LngLocator { get; set; }

        public string DMSLat { get; set; }

        public string DMSLong { get; set; }

        [DisplayName("Decimal Location")]
        public string DecimalLocation { get; set; }

        [DisplayName("Contact #")]
        public string ContactPhoneNumber { get; set; }

        [DisplayName("Email ID")]
        public string Email { get; set; }

        public bool BirthTimeRectified { get; set; }

        public Nullable<System.TimeSpan> RectifiedTOB { get; set; }

        public bool RectifiedAddOrDeduct { get; set; }

        public bool ShowAstroChart { get; set; }
        public double MoonDegree { get; set; }
    }
}
