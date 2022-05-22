using CosmicGameAPI.Model;
using System.ComponentModel.DataAnnotations;
namespace CosmicGameAPI.Entities
{
    public class ChartHolder
    {
        public int ChartHolderId { get; set; }
        public int UserId { get; set; }

        public string ChildName { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public string Gender { get; set; }

        public string BirthPlace { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public DateTime DOB { get; set; }

        public DateTime TOB { get; set; }

        public string TimeZone { get; set; }

        public string Ayanamasa { get; set; }

        public string HouseSystem { get; set; }

        public string AyanamasaPolicy { get; set; }

        public string LatLocator { get; set; }

        public string LngLocator { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
        public int ChartType { get; set; }
        public bool BirthTimeRectified { get; set; }
        public TimeSpan? RectifiedTOB { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
    }
}
