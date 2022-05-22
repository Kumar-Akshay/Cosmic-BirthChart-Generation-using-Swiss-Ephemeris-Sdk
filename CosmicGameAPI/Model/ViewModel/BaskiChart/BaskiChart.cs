using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmicGameAPI.Model.ViewModel.BaskiChart
{
    public class BaskiChart
    {
        public string Item_ID { get; set; } // This info have to be collected from the lstBhavaAndPlanetList. 
                                            // This piece of data is required to identify the Bhava and planet. This will help you to post the data to the table.
        public string Item_Name { get; set; }  // This will Identify Bhava or Planet

        public string ArcDist_DMS { get; set; }

        public int? Index { get; set; }

        public int? KCount { get; set; }

        //public double RCount { get; set; }  // This will track the Bhava and Planet       

        public string OCC_Star { get; set; }

        public string Rasi { get; set; }

        public string RasiL_1 { get; set; }

        public string RasiL_2 { get; set; }

        public string STR_L { get; set; }

        public string S1_STR_L { get; set; }

        public string S2_STR_L { get; set; }

        public string S3_STR_L { get; set; }

        public string S4_STR_L { get; set; }

        public bool IsPlanet { get; set; }

        public bool IsBhava { get; set; }
    }
}