using System.Collections.Generic;

namespace CosmicGameAPI.Model.ViewModel.VimsoChart
{
    public class AntraSoksumaChartViewModel
    {
        public List<AntraSoksumaRow> Chart { get; set; }
        public AntraSoksumaChartViewModel()
        {
            Chart = new List<AntraSoksumaRow>();
        }
    }
}