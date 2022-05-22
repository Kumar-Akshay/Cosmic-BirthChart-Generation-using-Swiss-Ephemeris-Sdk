using CosmicGameAPI.Model;
using CosmicGameAPI.Model.Request;
using CosmicGameAPI.Service.Interface;
using CosmicGameAPI.Utility.SDK;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CosmicGameAPI.Service.Implementation
{
    public class BhavaPlanetService : IBhavaPlanetService
    {
        public Array arr_Bhawa;
        private int IFlag = 0;
        private int sidmode = 0;
        public double Ayanamsha_lahiri = 0, Ayanamsha_raman = 0, Ayanamsha_krishnamurti = 0, Ayanamsha_NewCombe = 0, Ayanamsha_999 = 0;
        public double ayanamshaCalculated = 0, tempAyanamshaCalculated = 0;
        public double ayan_lahiri = 0, ayan_raman = 0, ayan_krishnamurti = 0, ayan_NewCombe = 0, ayan_999 = 0;
        public string ayan_lahiriDMS = "", ayan_ramanDMS = "", ayan_krishnamurtiDMS = "", ayan_NewCombeDMS = "", ayan_999DMS = "";

        public List<BhavaModel> BhawaPlanet_BList = new List<BhavaModel>();

        public BhavaPlanetService()
        {

        }

        public List<BhavaAndPlanet> GetAstroChartData(ChartHolderViewModel userInfo, GlobalChartCreatorData chartData)
        {
            Ayanamsha_NewCombe = ayan_NewCombe;
            SDK_Communicator.SetEphePath(AppDomain.CurrentDomain.BaseDirectory);
            var timezone = GetTimeZone(userInfo);
            //Converting Birthdate to UTC 
            TimeUTC timeUTC = SDK_Communicator.SwitchUsersTimeToUTC(userInfo.DOB, timezone);
            //Calculate Julian Day
            double julianDay = SDK_Communicator.CalculateJulianDay(timeUTC);
            var (SelectedAyanmsaDMS, NewCombeAdjust) = GeSelectedAyanmsaDMSandAyanNewCombe(userInfo, julianDay);
            chartData = GetChartData(userInfo, julianDay, timezone, SelectedAyanmsaDMS);
            #region Bhawa List Logic

            //  List<string> BhawaPlanet_BList = new List<string>();
            var lstBhava = GetBhawaList(userInfo, NewCombeAdjust, julianDay);
            #endregion

            #region Planet List Logic
            var lstPlanetList = GetPlanetList(userInfo, NewCombeAdjust, julianDay);
            #endregion

            #region Bhava and Planet List With Ascending Order by degree
            var lstBhavaAndPlanet = GetBhawaAndPlanetsList(lstBhava, lstPlanetList);
            lstBhavaAndPlanet.OrderBy(x => x.Location_DegDig);
            #endregion
            SDK_Communicator.CloseSDK();
            return lstBhavaAndPlanet;
        }
        private double GetTimeZone(ChartHolderViewModel userInfo)
        {
            TimeSpan usersTimeZone = TimeSpan.Parse(userInfo.TimeZone.Substring(1, userInfo.TimeZone.Length - 1));//parameter which is needed for method
            double timezone = usersTimeZone.TotalHours;
            if (userInfo.TimeZone[0] == '-')
                timezone *= -1;
            return timezone;
        }
        private (string, double) GeSelectedAyanmsaDMSandAyanNewCombe(ChartHolderViewModel userInfo, double julianDay)
        {
            var SelectedAyanmsaDMS = "";
            double AyanNewCombe = 0.0;

            var ayanamsa = Convert.ToInt32(userInfo.Ayanamasa);
            (string SelectedAyanmsaDMS, double NewCombeAdjust) result;
            if (userInfo.AyanamasaPolicy == "Nirayana")
            {
                if (ayanamsa == 999)
                {
                    sidmode = SDK_Communicator.SetSidMode(5, 0, 0);
                    tempAyanamshaCalculated = SDK_Communicator.GetAyanamsaUt(julianDay);
                    var ayandays = (userInfo.DOB.Date - new DateTime(291, 04, 15).Date).TotalDays;
                    AyanNewCombe = (ayandays * (50.2388475 / 365.242375)) / 3600;
                    SelectedAyanmsaDMS = SDK_Communicator.ConvertDegreesToDMS(AyanNewCombe);
                }
                else
                {
                    ayanamshaCalculated = SDK_Communicator.GetAyanamsaUt(julianDay);
                    if (ayanamsa == 255)
                    {
                        int SE_SIDBIT_USER_UT = 1024;
                        sidmode = SDK_Communicator.SetSidMode(ayanamsa + SE_SIDBIT_USER_UT, 23.0, 0);
                    }
                    else
                    {
                        sidmode = SDK_Communicator.SetSidMode(ayanamsa, 0, 0);
                    }
                    SelectedAyanmsaDMS = SDK_Communicator.ConvertDegreesToDMS(ayanamshaCalculated);
                }
            }

            var NewCombeAdjust = tempAyanamshaCalculated - AyanNewCombe;
            result = (SelectedAyanmsaDMS, NewCombeAdjust);
            return result;
        }
        private GlobalChartCreatorData GetChartData(ChartHolderViewModel userInfo, double julianDay, double timezone, string SelectedAyanmsaDMS)
        {
            var AYANAMSHAS = new int[4] { 1, 3, 5, 999 };
            var chartData = new GlobalChartCreatorData();

            chartData.txtSideralTime = SDK_Communicator.GetSideralTime(julianDay).ToString();
            chartData.txtSideralTime = SDK_Communicator.ConvertDegreesToDMS(SDK_Communicator.GetSideralTime(julianDay, 0, 0) + timezone);
            string data = Regex.Replace(chartData.txtSideralTime, @"°|'", ":");
            chartData.txtSideralTime = Convert.ToInt32(data.Substring(0, 3)).ToString() + data.Substring(3);
            chartData.txtAyanamsaInDMS = SelectedAyanmsaDMS;
            for (int i = 0; i < AYANAMSHAS.Length; i++)
            {
                string value = CalculateChartData(AYANAMSHAS[i], julianDay);
                PopulateChartData(chartData, AYANAMSHAS[i], value);
            }
            return chartData;

            void PopulateChartData(GlobalChartCreatorData chartData, int ayanamsa, string value)
            {
                if (ayanamsa == 1)
                {
                    chartData.lahiri = value;
                }
                else if (ayanamsa == 3)
                {
                    chartData.raman = value;
                }
                else if (ayanamsa == 5)
                {
                    chartData.krishnamurti = value;
                }
                else if (ayanamsa == 999)
                {
                    chartData.ayanNewCombe = value;
                }
            }

            string CalculateChartData(int ayanamsa, double julianDay)
            {
                sidmode = SDK_Communicator.SetSidMode(ayanamsa, 0, 0);

                double ayanamshaCalculated = SDK_Communicator.GetAyanamsaUt(julianDay);
                // coverted degree
                string ayanamshainDMS1 = SDK_Communicator.ConvertDegreesToDMS(ayanamshaCalculated);

                return ayanamshainDMS1;
            }
        }
        private List<BhavaModel> GetBhawaList(ChartHolderViewModel userInfo, double newCombeAdjust, double julianDay)
        {
            List<BhavaModel> lstBhava = new List<BhavaModel>();

            Array arr_Bhawa_NCombe;
            // Get selected House Value from Dropdown ie to say calculation method Eg: "Placidus Method"
            var hsys = userInfo.HouseSystem;
            arr_Bhawa = new double[13]; arr_Bhawa_NCombe = new double[13];
            // Get House position /House Cusp Calculation. array for 12 houses.
            IFlag = SDK_Communicator.GetIFlag(userInfo.AyanamasaPolicy);

            if (userInfo.AyanamasaPolicy == "Nirayana")
            {

                if (userInfo.Ayanamasa == "1")
                {
                    sidmode = SDK_Communicator.SetSidMode(1, 0, 0);
                    Ayanamsha_lahiri = SDK_Communicator.GetAyanamsaUt(julianDay);
                    GenerateArrayBhawa();
                }
                if (userInfo.Ayanamasa == "3")
                {
                    sidmode = SDK_Communicator.SetSidMode(3, 0, 0);
                    Ayanamsha_raman = SDK_Communicator.GetAyanamsaUt(julianDay);
                    GenerateArrayBhawa();
                }

                if (userInfo.Ayanamasa == "5")
                {
                    sidmode = SDK_Communicator.SetSidMode(5, 0, 0);
                    Ayanamsha_krishnamurti = SDK_Communicator.GetAyanamsaUt(julianDay);
                    GenerateArrayBhawa();
                }
                if (userInfo.Ayanamasa == "999")
                {
                    var ayandaysNC = (userInfo.DOB.Date - new DateTime(291, 04, 15).Date).TotalDays;
                    var AyanNewCombe = (ayandaysNC * (50.2388475 / 365.242375)) / 3600;

                    userInfo.AyanamasaPolicy = "Sayana";
                    GenerateArrayBhawa();
                    AdjustBhawaArray(arr_Bhawa, AyanNewCombe);
                }
            }
            else if (userInfo.AyanamasaPolicy == "Sayana")
            {
                GenerateArrayBhawa();
            }

            var ayandays = (userInfo.DOB.Date - new DateTime(291, 04, 15).Date).TotalDays;
            Ayanamsha_NewCombe = (ayandays * (50.2388475 / 365.242375)) / 3600;//23.153986011503552   ///23.153986011503552
            string ayan_NewCombeDMS = SDK_Communicator.ConvertDegreesToDMS(Ayanamsha_NewCombe); //"023 ° 09 ' 14.35"

            // Ayanamsa Calculation for  General Comparison Purposes.
            {
                sidmode = SDK_Communicator.SetSidMode(1, 0, 0);
                ayan_lahiriDMS = SDK_Communicator.ConvertDegreesToDMS(SDK_Communicator.GetAyanamsaUt(julianDay));

                sidmode = SDK_Communicator.SetSidMode(3, 0, 0);
                ayan_ramanDMS = SDK_Communicator.ConvertDegreesToDMS(SDK_Communicator.GetAyanamsaUt(julianDay));

                sidmode = SDK_Communicator.SetSidMode(5, 0, 0);
                ayan_krishnamurtiDMS = SDK_Communicator.ConvertDegreesToDMS(SDK_Communicator.GetAyanamsaUt(julianDay));
            }
            int index = 0;

            List<BhavaModel> BhawaPlanet_BList = new List<BhavaModel>();
            //  BhawaPlanet_BList BhawaPlanet_BList
            // Adding to the BhawaPlanet_BList
            foreach (double bhava in arr_Bhawa)
            {
                if (index != 0)
                {
                    double KID = bhava / 30;
                    if (KID - Math.Truncate(KID) > 0)
                    {
                        KID = Math.Ceiling(KID);
                    }
                    KID = KID < 0 || KID > 12 ? 0 : KID;

                    lstBhava.Add(new BhavaModel() { Num_Index = index, Item_ID = "B", Item_Name = "BH : " + index, Location_DegDig = bhava, KID = Convert.ToInt32(KID) });

                    BhawaPlanet_BList.Add(new BhavaModel() { Num_Index = index, Item_ID = "B", Item_Name = "BH : " + index, Location_DegDig = bhava, KID = Convert.ToInt32(KID) });
                }
                index++;
            }


            void GenerateArrayBhawa()
            {
                IFlag = SDK_Communicator.GetIFlag(userInfo.AyanamasaPolicy);
                arr_Bhawa = userInfo.AyanamasaPolicy == "Sayana"
                    ? SDK_Communicator.GetHouses_1(julianDay, IFlag, Convert.ToDouble(userInfo.Latitude, CultureInfo.InvariantCulture), Convert.ToDouble(userInfo.Longitude, CultureInfo.InvariantCulture), Convert.ToChar(hsys))
                : SDK_Communicator.GetHousesEx1(julianDay, IFlag, Convert.ToDouble(userInfo.Latitude,
                    CultureInfo.InvariantCulture), Convert.ToDouble(userInfo.Longitude, CultureInfo.InvariantCulture), Convert.ToChar(hsys));
            }

            void AdjustBhawaArray(Array arr_Bhawa, double NewCombeAdjust)
            {
                for (int i = 1; i < arr_Bhawa.Length; i++)
                {
                    var value = Convert.ToDouble(arr_Bhawa.GetValue(i));
                    var finalvalue = value - NewCombeAdjust;
                    if (finalvalue < 0)
                    {
                        finalvalue = 360 + finalvalue;
                    }
                    arr_Bhawa.SetValue(finalvalue, i);
                }
            }

            return lstBhava;
        }
        private List<PlanetModel> GetPlanetList(ChartHolderViewModel userInfo, double newCombeAdjust, double julianDay)
        {
            string serr = "";
            double planetPosition_DegDigital = 0.0;
            var pname = "";
            var planetName = "";
            List<PlanetModel> lstPlanetList = new();
            for (int planet = 0; planet <= 11; planet++)
            {

                if (planet <= 10)
                {
                    //  Getting  Planet positions longitude, latitude, speed in long., speed in lat., and speed in dist
                    string planetPosition = SDK_Communicator.GetPlanetsPositions(julianDay, planet, IFlag, serr);
                    // This is Calculating the set of strings separated by "  :  "
                    string[] StrCount_in_PP = planetPosition.Split(':');

                    //StringBuilder sb = new StringBuilder();
                    planetPosition_DegDigital = Convert.ToDouble(StrCount_in_PP[1]);  // Extract the actual Planet Location (second Set of string)in Deg. Digital

                    if (userInfo.Ayanamasa == "999")
                    {
                        planetPosition_DegDigital = planetPosition_DegDigital + newCombeAdjust;
                    }

                    // Get planet name
                    planetName = SDK_Communicator.GetPlanetName(planet, pname);
                }

                if (planet == 10) { planetName = "Ragu"; }

                if (planet == 11)
                {
                    planetName = "Kethu";

                    double planetPostion_Kethu = 0;
                    if (planetPosition_DegDigital > Convert.ToDouble(180))
                    {
                        planetPostion_Kethu = planetPosition_DegDigital - 180;
                    }
                    else
                    {
                        planetPostion_Kethu = planetPosition_DegDigital + 180;
                    }

                    planetPosition_DegDigital = planetPostion_Kethu;
                }

                if (planet == 1)
                {
                    AstroGlobalVariables.moonValue = planetPosition_DegDigital;
                }
                int index = 15;
                BhawaPlanet_BList.Add(new BhavaModel() { Num_Index = index, Item_ID = "P", Item_Name = planetName, Location_DegDig = planetPosition_DegDigital, });

                double KID = planetPosition_DegDigital / 30;
                if (KID - Math.Truncate(KID) > 0)
                {
                    KID = Math.Ceiling(KID);
                }
                KID = KID < 0 || KID > 12 ? 0 : KID;
                // Adding Planet List
                lstPlanetList.Add(new PlanetModel() { NumIndex = planet, ItemID = "P", ItemName = planetName, KID = Convert.ToInt32(KID), LocationDegDig = planetPosition_DegDigital });  // Planet List updated
            }
            return lstPlanetList;
        }
        private List<BhavaAndPlanet> GetBhawaAndPlanetsList(List<BhavaModel> lstBhava, List<PlanetModel> lstPlanetList)
        {
            int BavLoc = 0;
            int numindex = 0;
            double PlaLoc = 0.1;
            List<BhavaAndPlanet> lstBhavaAndPlanet = new List<BhavaAndPlanet>();
            for (int i = 0; i < lstBhava.Count; i++)
            {
                BavLoc++;
                lstBhavaAndPlanet.Add(new BhavaAndPlanet()
                {
                    Index = numindex++,
                    KID = lstBhava[i].KID,
                    Item_ID = lstBhava[i].Item_ID,
                    Item_Name = lstBhava[i].Item_Name,
                    Relative_Order = BavLoc.ToString(),
                    Location_DegDig = lstBhava[i].Location_DegDig,
                    Location_DMS = SDK_Communicator.ConvertDegreesToDMS(lstBhava[i].Location_DegDig), //coverted degree
                });
                PlaLoc = 0.1;
                BhavaModel nextBhava;
                List<PlanetModel> planets;
                nextBhava = (i == lstBhava.Count - 1) ? lstBhava[0] : lstBhava[i + 1];  // Command to control the loop and also to fetch next bHava

                if (nextBhava.Location_DegDig < lstBhava[i].Location_DegDig)
                    planets = lstPlanetList.Where(p => p.LocationDegDig >= lstBhava[i].Location_DegDig || (p.LocationDegDig > 0 && p.LocationDegDig < nextBhava.Location_DegDig)).ToList();
                else
                    planets = lstPlanetList.Where(p => (p.LocationDegDig >= lstBhava[i].Location_DegDig) && p.LocationDegDig < nextBhava.Location_DegDig).ToList();

                if (planets.Any())
                {
                    planets.ForEach(plnt =>
                    {
                        lstBhavaAndPlanet.Add(new BhavaAndPlanet()
                        {
                            Index = numindex++,
                            KID = plnt.KID,
                            Item_ID = plnt.ItemID,
                            Item_Name = plnt.ItemName,
                            Relative_Order = (BavLoc + PlaLoc).ToString(),
                            Location_DegDig = plnt.LocationDegDig,
                            Location_DMS = SDK_Communicator.ConvertDegreesToDMS(plnt.LocationDegDig), // coverted degree
                        });
                        PlaLoc += .1;
                    });
                }
            }
            return lstBhavaAndPlanet;
        }
    }
}
