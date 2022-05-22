namespace CosmicGameAPI.Model.Response
{
    public class AddressResponse
    {
        public int CurrentAddressID { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActiveAddress { get; set; }
        public string Timezone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LatLocator { get; set; }
        public string LngLocator { get; set; }
        public bool isAdd { get; set; }
    }
}
