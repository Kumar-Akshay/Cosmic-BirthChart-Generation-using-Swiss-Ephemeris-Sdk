using Microsoft.VisualBasic;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CosmicGameAPI.Utility.SDK
{
    public static class NativeMethods
    {
        //  Private Variables 
        private static double[] x = new double[6];
        private static double[] Cusp = new double[13];
        private static double[] Ascmc = new double[10];
        public static int SE_GREG_CAL = 1;
        public static int SE_CALC_RISE = 1;
        public static int SE_SUN = 1;


        //  SDK Path
        const string SdkPath = @"dlls\swedll64.dll";

        public static int SE_BIT_DISC_CENTER = 256;
        public static int SE_BIT_NO_REFRACTION = 512;
        public static int SE_BIT_GEOCTR_NO_ECL_LAT = 128;
        public static int SE_BIT_HINDU_RISING = SE_BIT_DISC_CENTER;

        //  private static int SE_JUL_CAL = 0;
        private static double[] dret = new double[2];
        private static double[] xx = new double[6];

        [DllImport(SdkPath)]
        public static extern void swe_set_ephe_path(string path);

        [DllImport(SdkPath)]
        public static extern void swe_close();

        [DllImport(SdkPath)]
        public static extern string swe_version(string svers);

        [DllImport(SdkPath)]
        private static extern double swe_julday
           (
           int Year,
           int Month,
           int Day,
           double hour,
           int gregflg
           );

        [DllImport(SdkPath)]
        private static extern double swe_house_pos(
            double armc,        /* ARMC */
            double geolat,  /* geographic latitude, in degrees */
            double eps,         /* ecliptic obliquity, in degrees */
            int hsys,       /* house method, one of the letters PKRCAV */
            double xpin, 	/* array of 2 doubles: ecl. longitude and latitude of the planet */
            char serr);


        [DllImport(SdkPath)]
        public static extern double swe_sidtime(double tjd_ut);

        [DllImport(SdkPath)]
        public static extern double swe_sidtime0(double tjd_ut, double eps, double nut);

        [DllImport(SdkPath)]
        public static extern string swe_get_ayanamsa_name(Int32 isidmode);

        [DllImport(SdkPath)]
        public static extern int swe_lmt_to_lat(double tjd_lmt, double geolon, double tjd_lat, string serr);

        [DllImport(SdkPath)]

        public static extern int swe_calc
            (
            double tjd,
            int ipl,
            int iflag,
            //  x must be first of six array elements
            out double x,
            //  serr must be able to hold 256 bytes
            string serr
            );

        [DllImport(SdkPath)]

        private static extern void swe_get_planet_name(
                  int ipl,
                  StringBuilder spname
                );


        [DllImport(SdkPath)]

        private static extern int swe_houses_ex(
                  double tjd_ut,
                  int iflag,
                  /* geographic latitude, in degrees */
                  double geolat,
                  /* geographic longitude, in degrees
                  *eastern longitude is positive,
                  *western longitude is negative,
                  * northern latitude is positive,
                  * southern latitude is negative */
                  double geolon,

                  int ihsy,
                  //  hcusps must be first of 13 array elements
                  [In, Out] double[] hcusps,
                  //  ascmc must be first of 10 array elements
                  [In, Out] double[] ascmc
                );

        [DllImport(SdkPath)]

        private static extern int swe_houses(
                 double tjd_ut,
                 // geographic latitude, in degrees
                 double geolat,
                 /* geographic longitude, in degrees
                                          * eastern longitude is positive,
                                          * western longitude is negative,
                                          * northern latitude is positive,
                                          * southern latitude is negative */
                 double geolon,
                 int ihsy,
                 // hcusps must be first of 13 array elements
                 out double hcusps,
                 // ascmc must be first of 10 array elements
                 out double ascmc
               );

        [DllImport(SdkPath)]
        public static extern void swe_utc_time_zone(
             int iyear,
             int imonth,
             int iday,
             int ihour,
             int imin,
             double dsec,
             double d_timezone,
             out int iyear_out,
             out int imonth_out,
             out int iday_out,
             out int ihour_out,
             out int imin_out,
             out double dsec_out
            );

        [DllImport(SdkPath)]
        private static extern int swe_utc_to_jd(
             int iyear,
             int imonth,
             int iday,
             int ihour,
             int imin,
             /* note : second is a decimal */
             double dsec,
             /* Gregorian calendar: 1, Julian calendar: 0 */
             int gregflag,
             /* return array, two doubles:
                                   * dret[0] = Julian day in ET (TT)
                                   * dret[1] = Julian day in UT (UT1) */
             out double dret,
             /* error string */
             out string serr
            );

        [DllImport(SdkPath)]
        private static extern int swe_calc_ut(
            double tjd_ut,
            int ipl,
            int iflag,
            [In, Out] double[] xx,
            string serr
            );

        [DllImport(SdkPath)]
        public static extern int swe_set_sid_mode(
           int sid_mode,
           double t0,
           double ayan_t0
           );

        [DllImport(SdkPath)]

        // Julian day number in UT 
        public static extern double swe_get_ayanamsa_ut(
         double tjd_ut
         );


        [DllImport(SdkPath)]

        public static extern double swe_set_topo(
         double longitude, double latitude, double altitude
         );


        [DllImport(SdkPath)]
        public static extern int swe_rise_trans(
            double tjd_ut, int ipl, string starname, int epheflag, int rsmi, double[] geopos, double atpress, double attemp, out double trise, StringBuilder serr
            );

        [DllImport(SdkPath)]
        public static extern void swe_jdut1_to_utc(double tjd_ut, int gregflag, out int year, out int month, out int day, out int hour, out int mint, out double sec);



        public static Array swe_houses_1(double tjd_ut, double lat, double lon, char hsys)
        {
            int resultHouse = swe_houses(tjd_ut, lat, lon, Strings.AscW(hsys), out Cusp[0], out Ascmc[1]);
            return Cusp;
        }

        public static Array swe_houses_ex_1(double tjd_ut, int iflag, double lat, double lon, char hsys)
        {
            int resultHouse = swe_houses_ex(tjd_ut, iflag, lat, lon, Strings.AscW(hsys), Cusp, Ascmc);
            return Cusp;
        }

        public static double UtcToJD(int year, int month, int day, int hour, int min, double sec, string serr)
        {
            serr = new string(Strings.ChrW(0), 255);
            int result = swe_utc_to_jd(year, month, day, hour, min, sec, SE_GREG_CAL, out dret[0], out serr);
            var jd_et = dret[0];
            var jd_ut = dret[1];
            return jd_ut;
        }

        public static string GetPlanetsPostitions(double julianDay, int bodyNumber, int iFlag, string serr)
        {
            serr = new string(Strings.ChrW(0), 255);
            int result = swe_calc_ut(julianDay, bodyNumber, iFlag, xx, serr);
            var arr = xx;
            return result + ":" + xx[0].ToString() + ":" + xx[1].ToString();
        }

        public static string GetPlanetName(int ipl, string pname)
        {
            StringBuilder planetName = new StringBuilder(40);
            swe_get_planet_name(ipl, planetName);
            return planetName.ToString();
        }

        public static string DegreeToDMS(double degDig)
        {
            double sec;
            double min;
            double fract;
            double trunk;
            trunk = Math.Truncate(Math.Abs(degDig));
            fract = Math.Abs(degDig) - Math.Truncate(Math.Abs(degDig));
            min = Math.Truncate(fract * 60);
            sec = Math.Round((Double)fract * 3600 - min * 60, 2);

            if (sec == 60)
            {
                min += 1;
                sec = 0;
            }
            if (min == 60)
            {
                trunk += 1;
                min = 0;
            }

            return String.Format((Math.Sign(degDig) * trunk).ToString(), "000") + " ° " + String.Format(min.ToString(), "00") + " ' " + String.Format(sec.ToString(), "00.00");
        }

        public static DateTime TimeFunction(double seconds, DateTime datetoadd)
        {
            return new DateTime(datetoadd.Year, datetoadd.Month, datetoadd.Day, datetoadd.Hour, datetoadd.Minute, datetoadd.Second).AddSeconds(seconds);
        }
    }
}
