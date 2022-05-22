using CosmicGameAPI.Entities;
using CosmicGameAPI.Model;
using CosmicGameAPI.Model.Request;
using CosmicGameAPI.Model.Response;

namespace CosmicGameAPI.Service.Interface
{
    public interface IChartHolderService
    {
        Task<ServiceResponse> LoadAllChartHolder(int userId);
        Task<ServiceResponse> ChartHolderById(int chartHolderID, int userId);
        Task<ServiceResponse> AddCharHolder(ChartHolder chartHolderViewModel);
        Task<ServiceResponse> EditCharHolder(ChartHolder chartHolderViewModel);
        Task<ServiceResponse> DeleteChartHolderRecord(int chartHolderID, int userId);
        Task<ServiceResponse> LoadCountries();
        Task<ServiceResponse> LoadTimeZone(string countryName);
        Task<ServiceResponse> GetAllCurrentAddress(int userId);
        Task<ServiceResponse> AddCurrentAddress(CurrentAddress addressRequest);
        Task<ServiceResponse> EditCurrentAddress(int addressID, int userId);
        Task<ServiceResponse> GetCurrentAddressById(int addressID, int userId);
    }
}
