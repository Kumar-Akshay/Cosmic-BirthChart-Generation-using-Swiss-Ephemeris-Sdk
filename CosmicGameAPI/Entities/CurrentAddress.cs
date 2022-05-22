using System.ComponentModel.DataAnnotations;

namespace CosmicGameAPI.Entities
{
    public class CurrentAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string PostalCode { get; set; }
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        public decimal IsActive { get; set; }
        [MaxLength(100)]
        public string TimeZone { get; set; }
        [MaxLength(25)]
        public string LatLocator { get; set; }
        [MaxLength(25)]
        public string LngLocator { get; set; }
        [MaxLength(50)]
        public string Longitude { get; set; }
        [MaxLength(50)]
        public string Latitude { get; set; }
    }
}
