using CosmicGameAPI.Model.ViewModel.VimsoChart;

namespace CosmicGameAPI.Service.Interface
{
    public interface IChartsService
    {
        Task<dynamic> GetTraditionalChart(int chartHolderId);
        Task<dynamic> GetBaskiChartAsync(int chartHolderId, string rectifiedTOB, int dayDifference);
        Task<dynamic> GetVimsoDissaiLoad(int chartHolderId);
        Task<dynamic> GetPuthiForDissai(int chartHolderId, int dissaiGroupId);
        Task<dynamic> GetAntraSoksumaForPuthi(int chartHolderId, int puthiGroupId, int dissaiGroupId);
        Task<dynamic> GetPranaForSoksuma(int chartHolderId, int puthiGroupId, int soksumaGroupId);
    }
}
