using CosmicGameAPI.Entities;
using CosmicGameAPI.Model;
using CosmicGameAPI.Model.ViewModel;
using CosmicGameAPI.Model.ViewModel.BaskiChart;
using CosmicGameAPI.Model.ViewModel.TraditionalChart;
using CosmicGameAPI.Model.ViewModel.VimsoChart;
using CosmicGameAPI.Utility.SDK;
using System.Text;

namespace CosmicGameAPI.Service.Implementation
{
    public static class ChartCreator
    {

        public static DateTime GetSunRiseSet(double[] GeoPosition, string timezonestr,DateTime dob, int Status)
        {
            try
            {
                string starname = string.Empty;
                int SE_SUN = 0;
                int iPlanetNo = SE_SUN;
                int iFlag = 0;
                int SE_CALC_RISE = 1;
                int SE_CALC_SET = 2;
                double sunRise = 0;
                double sunSet = 0.0f;
                StringBuilder sErr = new StringBuilder(255);
                DateTime dateTime = DateTime.Now;


                TimeSpan usersTimeZone = TimeSpan.Parse(timezonestr.Substring(1, timezonestr.Length - 1));//parameter which is needed for method
                double timezone = usersTimeZone.TotalHours;
                if (timezonestr[0] == '-')
                    timezone *= -1;

                SDK_Communicator.SetEphePath(AppDomain.CurrentDomain.BaseDirectory);

                TimeUTC timeUTC = SDK_Communicator.SwitchUsersTimeToUTC(dob, timezone);
                double julianDay = SDK_Communicator.CalculateJulianDay(timeUTC);

                if (Status == 0)
                {
                    SDK_Communicator.GetSunRise(julianDay, iPlanetNo, starname, iFlag, SE_CALC_RISE, GeoPosition, 0.0f, 0.0f, out sunRise, sErr);
                    dateTime = SDK_Communicator.JulianTimeToUTC(sunRise, timezone);
                }
                if (Status == 1)
                {
                    SDK_Communicator.GetSunRise(julianDay, iPlanetNo, starname, iFlag, SE_CALC_SET, GeoPosition, 0.0f, 0.0f, out sunSet, sErr);
                    dateTime = SDK_Communicator.JulianTimeToUTC(sunSet, timezone);
                }
                SDK_Communicator.CloseSDK();
                return dateTime;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static TraditionalChartViewModel GetTraditionalChart(List<BhavaAndPlanet> lstPlanetGridData)
        {

            TraditionalChartViewModel lstTraditionalData = new TraditionalChartViewModel();

            var lstKID1o6 = lstPlanetGridData.Where(x => x.KID <= 6).OrderBy(x => x.Location_DegDig).ToList();
            var lstKID7o12 = lstPlanetGridData.Where(x => x.KID > 6).OrderByDescending(x => x.Location_DegDig).ToList();

            PopulateTraditoinalData(lstTraditionalData, lstKID1o6);
            PopulateTraditoinalData(lstTraditionalData, lstKID7o12);

            return lstTraditionalData;
        }

        public static BaskiChartViewModel GetBaskiChartGrid(CosmicDbContext _cosmicDbContext, List<BhavaAndPlanet> lstBhavaAndPlanet)
        {
            BaskiChartViewModel baskiChartViewModel = new BaskiChartViewModel();

            lstBhavaAndPlanet = lstBhavaAndPlanet.OrderBy(x => x.Location_DegDig).ToList();

            foreach (var bhavaAndPlanet in lstBhavaAndPlanet)
            {
                var subLordData = _cosmicDbContext.u_Lev4_S4SL_Registers.Where(x => x.S4SL_ArcDist >= bhavaAndPlanet.Location_DegDig).FirstOrDefault();
                if (subLordData != null)
                {
                    var isBhava = bhavaAndPlanet.Item_Name.Contains("BH");
                    baskiChartViewModel.Chart.Add(new BaskiChart()
                    {
                        Item_ID = bhavaAndPlanet.Relative_Order,
                        Item_Name = bhavaAndPlanet.Item_Name,
                        ArcDist_DMS = bhavaAndPlanet.Location_DMS,
                        Index = bhavaAndPlanet.Index,
                        KCount = Convert.ToInt32(subLordData.KC_INDEX.Value),
                        Rasi = subLordData.KC_Name,
                        RasiL_1 = subLordData.KC_Rasi_Lord,
                        RasiL_2 = subLordData.KC_Rasi_Lord_SPL,
                        OCC_Star = subLordData.StarName,
                        STR_L = subLordData.StarLord,
                        S1_STR_L = subLordData.S1SL,
                        S2_STR_L = subLordData.S2SL,
                        S3_STR_L = subLordData.S3SL,
                        S4_STR_L = subLordData.S4SL,
                        IsBhava = isBhava,
                        IsPlanet = !isBhava
                    });
                }
            }
            baskiChartViewModel.Chart = baskiChartViewModel.Chart.OrderBy(x => x.Index).ToList();
            return baskiChartViewModel;
        }

        public static IQueryable<VimsoMasterRegister> GetVimsoDataQuery(CosmicDbContext _cosmicDbContext, double moonDegree)
        {
            var maxDegree = moonDegree + 120;

            if (maxDegree < 360)
            {
                return _cosmicDbContext.VimsoMasterRegisters.Where(x => x.S4SL_ArcDist >= moonDegree && x.S4SL_ArcDist <= maxDegree);
            }
            else
            {
                maxDegree -= 360;
                return _cosmicDbContext.VimsoMasterRegisters.Where(x => x.S4SL_ArcDist >= moonDegree && x.S4SL_ArcDist <= 360).Concat(_cosmicDbContext.VimsoMasterRegisters.Where(x => x.S4SL_ArcDist <= maxDegree));
            }
        }

        public static VimsoChartViewModel GetVimsoChart(List<VimsoDTO> Data, DateTime startDate, double moonDegree)
        {
            const double YEAR_DEGREES = 40 / 3;
            const double DAYS_IN_YEAR = 365.25;
            const double SECONDS_IN_YEAR = DAYS_IN_YEAR * 24 * 60 * 60;

            var result = new VimsoChartViewModel();
            var currentDate = startDate;
            var start = 0;
            for (int i = 0; i < Data.Count - 1; i++)
            {
                if (Data[i].Gp != Data[i + 1].Gp || i == Data.Count - 2)
                {
                    var timeDifference = 0.0;
                    var overflow = CheckDegreeOverflow(Data[i].MovingDistance, moonDegree);
                    if (overflow)
                        timeDifference = (Data[i].MovingDistance - Data[start].MovingDistance) / YEAR_DEGREES * SECONDS_IN_YEAR * Data[i].VimsoPeriod;

                    currentDate = currentDate.AddSeconds(timeDifference);
                    var gp = Data[i].Gp;
                    var starLord = Data[i].Name;

                    result.Chart.Add(new VimsoChartCell()
                    {
                        Gp = gp,
                        StarLord = starLord,
                        Date = overflow ? currentDate : new DateTime()
                    });

                    if (overflow)
                        timeDifference = (Data[i + 1].MovingDistance - Data[i].MovingDistance);
                    currentDate = currentDate.AddSeconds(timeDifference / YEAR_DEGREES * SECONDS_IN_YEAR * Data[i].VimsoPeriod);

                    start = i + 1;
                }
            }

            return result;
        }

        #region private methods
        private static void PopulateTraditoinalData(TraditionalChartViewModel lstTraditionalData, List<BhavaAndPlanet> lstData)
        {
            int style = lstData[0].KID > 6 ? 2 : 1; //basically changes the direction of arrow on frontend (up, down)
            foreach (var x in lstData)
            {
                var decsecMin = SDK_Communicator.ConvertDegreesToDMS(x.Location_DegDig).Substring(0, 8);
                var data = " <br />";
                if (x.Item_Name.Contains("BH :"))
                {
                    x.Item_Name = x.Item_Name.Length >= 7 ? x.Item_Name.Substring(0, 7) : x.Item_Name;
                    data += "<span class=\"bhavaStyle" + style + "\">" + x.Item_Name + " :  &nbsp;" + decsecMin + "</span>";
                }
                else
                {
                    x.Item_Name = x.Item_Name.Length >= 3 ? x.Item_Name.Substring(0, 3) : x.Item_Name;
                    data += x.Item_Name + " :  &nbsp;" + decsecMin;
                }
                lstTraditionalData.Cells[x.KID - 1].Code += data;
            }
        }

        private static bool CheckDegreeOverflow(double currentDegree, double moonDegree)
        {
            var result = !((moonDegree + 120 > 360.0 && currentDegree < moonDegree && currentDegree > moonDegree - 240)
                    || (moonDegree + 120 <= 360.0 && (currentDegree < moonDegree || currentDegree > moonDegree + 120)));

            return result;
        }
        #endregion
    }
}
