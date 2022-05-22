using System.ComponentModel.DataAnnotations;

namespace CosmicGameAPI.Entities
{
    public class VimsoMasterRegister
    {
        [Key]
        public int LineNo { get; set; }
        public int Rasi_id { get; set; }
        public string Rasi { get; set; }
        public string Rasi_L { get; set; }
        public int Star_id { get; set; }
        public string StarName { get; set; }
        public int Vimso_pd { get; set; }
        public int Di_Gp { get; set; }
        public string StarLord { get; set; }
        public int Pu_Gp { get; set; }
        public string S1SL { get; set; }
        public int An_Gp { get; set; }
        public string S2SL { get; set; }
        public int So_Gp { get; set; }
        public string S3SL { get; set; }
        public string S4SL { get; set; }
        public double S4SL_ArcDist { get; set; }
        public double Moving_Distance { get; set; }
        public string DMS { get; set; }
        public string Pre_S4SL_ArcDist { get; set; }
    }
}
