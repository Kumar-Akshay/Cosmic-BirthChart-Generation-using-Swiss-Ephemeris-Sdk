namespace CosmicGameAPI.Model.Response
{
    public class PlanetResponse
    {
        public int NumIndex { get; set; }
        public string ItemID { get; set; }
        public int KID { get; set; }
        public string ItemName { get; set; }
        public double LocationDegDig { get; set; }
        public string DMS { get; set; }
        public bool Reversing { get; set; }
        public bool NoReversing { get; set; }
        public DateTime ReversingDate { get; set; }
    }
}
