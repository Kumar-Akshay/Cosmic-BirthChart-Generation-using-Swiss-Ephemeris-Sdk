using System;

namespace CosmicGameAPI.Utility.SDK
{
    public class SDK_Communicator
    {
        // Refer page 11   2.3.1  - High Pr4ecision Speed
        private const int SEFLG_SPEED = 256;
        // Refer Page 11 2.3.1     Use Swiss Ephemeris  , default
        private const int SEFLG_SWIEPH = 2;
        // Refer Page 11 2.3.12  Sideral positions are defined here.
        private const int SEFLG_SIDEREAL = 64 * 1024;

        public static void SetEphePath(string path)
        {
            try
            {
                NativeMethods.swe_set_ephe_path(path);
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong path" + ex.Message);
            }
        }
        //     public static TimeUTC SwitchToUTC(int year, int month, int day, int hours, int minutes, double seconds, int timezone) {
        //         TimeUTC timeUTC = new TimeUTC(); // class to save output results from method swe_utc_time_zone

        //NativeMethods.swe_utc_time_zone(year, month, day, hours, minutes, seconds, timezone, 
        //                                         out timeUTC.Year, out timeUTC.Month, out timeUTC.Day, 
        //                                         out timeUTC.Hours, out timeUTC.Minutes, out timeUTC.Seconds);	

        //         return timeUTC;
        //     }

        public static TimeUTC SwitchUsersTimeToUTC(DateTime DOB, double timezone)
        {
            TimeUTC timeUTC = new TimeUTC(); // class to save output results from method swe_utc_time_zone
           
            NativeMethods.swe_utc_time_zone(DOB.Year, DOB.Month,DOB.Day, DOB.Hour,
                                           DOB.Minute, DOB.Second, timezone, out int Year, out  int Month,
                                            out int Day, out int Hours, out int Minutes, out double Seconds);
            timeUTC.Year = Year;
            timeUTC.Month = Month;
            timeUTC.Day = Day;
            timeUTC.Hours = Hours;
            timeUTC.Minutes = Minutes;
            timeUTC.Seconds = Seconds;
            return timeUTC;
        }

        public static double CalculateJulianDay(TimeUTC timeUTC)
        {
            // calculate Julian day number in UT (UT1) and ET (TT) from UTC
            var serr = ""; //What this variable mean? Rename it
            double julianDay = NativeMethods.UtcToJD(timeUTC.Year, timeUTC.Month, timeUTC.Day, timeUTC.Hours, timeUTC.Minutes, timeUTC.Seconds, serr);
            return julianDay;
        }

        public static double GetAyanamsaUt(double julianDay)
        { //What does this method and how to name it
            var result = NativeMethods.swe_get_ayanamsa_ut(julianDay);

            return result;
        }

        public static string ConvertDegreesToDMS(double ayanNewCombe)
        {
            var dms = NativeMethods.DegreeToDMS(ayanNewCombe);

            return dms;
        }

        public static int SetSidMode(int sidMode, double t0, int ayan_t0)
        { // what this parameters mean
            var result = NativeMethods.swe_set_sid_mode(sidMode, t0, ayan_t0);

            return result;
        }

        public static double GetSideralTime(double julianDay)
        {
            var result = NativeMethods.swe_sidtime(julianDay);

            return result;
        }

        public static double GetSideralTime(double julianDay, double eps, double nut)
        {
            var result = NativeMethods.swe_sidtime0(julianDay, eps, nut);

            return result;
        }


        public static Array GetHouses_1(double julianDay, int iflag, double latitude, double longitude, char houseSystem)
        {
            //Check
            var result = NativeMethods.swe_houses_1(julianDay, latitude, longitude, houseSystem);
            return result;
        }

        public static Array GetHousesEx1(double julianDay, int iflag, double latitude, double longitude, char houseSystem)
        {
            //Check
            var result = NativeMethods.swe_houses_ex_1(julianDay, iflag, latitude, longitude, houseSystem);
            return result;
        }



        public static string GetPlanetsPositions(double julianDay, int planet, int iflag, string serr)
        {
            var result = NativeMethods.GetPlanetsPostitions(julianDay, planet, iflag, serr);

            return result;
        }

        public static string GetPlanetName(int planet, string planetName)
        {
            var result = NativeMethods.GetPlanetName(planet, planetName);

            return result;
        }

        public static int GetIFlag(string aynamsaPolicy)
        {
            var IFlag = aynamsaPolicy == "Sayana" ? SEFLG_SPEED + SEFLG_SWIEPH : SEFLG_SIDEREAL + SEFLG_SPEED + SEFLG_SWIEPH;


            return IFlag;
        }

        public static void CloseSDK()
        {
            NativeMethods.swe_close();
        }
    }
}