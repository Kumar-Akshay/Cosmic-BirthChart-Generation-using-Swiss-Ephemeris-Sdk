using System.Collections.Generic;

namespace CosmicGameAPI.Model.ViewModel.VimsoChart
{
    public class VimsoChartViewModel
    {
        public List<VimsoChartCell> Chart { get; set; }

        public VimsoChartViewModel()
        {
            Chart = new List<VimsoChartCell>();
        }
    }
}