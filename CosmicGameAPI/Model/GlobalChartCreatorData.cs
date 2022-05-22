using CosmicGameAPI.Model.Response;

namespace CosmicGameAPI.Model
{
    public class GlobalChartCreatorData
    {
        public string lahiri { get; set; }
        public string raman { get; set; }
        public string krishnamurti { get; set; }
        public string ayanNewCombe { get; set; }
        public string txtAyanamsaUsed { get; set; }
        public string txtAyanamsaInDMS { get; set; }
        public string txtSideralTime { get; set; }
        public string txtLongitudeInDMS { get; set; }
        public string txtLatitudeInDMS { get; set; }
        public string txtSunRiseAt { get; set; }
        public string txtSunSetAt { get; set; }
    }

    public static class AstroGlobalVariables
    {
        // StarLordAverageCircularDegDistance
        public static double SL_ACDD = (double)360 / 27;
        public static double moonValue { get; set; }
        public static string DOB { get; set; }
        public static string TOB { get; set; }
        //  public static double yearinSeconds = 31556952; //Converting Year into Seconds (365.25 * 24 * 3600)
        //   public static double yearinSeconds = 31536000; //Converting Year into Seconds (365.00 * 24 * 3600)
        //  Converting Year into Seconds (365.2500 * 24 * 3600)
        public static double yearinSeconds = 31557600;

        public static ChartHolderViewModel userInfo { get; set; }
    }
}
