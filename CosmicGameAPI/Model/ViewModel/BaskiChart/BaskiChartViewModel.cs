using System.Collections.Generic;

namespace CosmicGameAPI.Model.ViewModel.BaskiChart
{
    public class BaskiChartViewModel
    {
        public List<BaskiChart> Chart { get; set; }

        public BaskiChartViewModel() {
            Chart = new List<BaskiChart>() {
                // new BaskiChart() {
                //     // STR_L = "Dis", 
                //     // S1_STR_L = "Put", 
                //     // S2_STR_L = "Ant", 
                //     // S3_STR_L = "Suk", 
                //     // S4_STR_L = "Pir", 
                //     // IsBhava = true
                // }
            };
        }
    }
}