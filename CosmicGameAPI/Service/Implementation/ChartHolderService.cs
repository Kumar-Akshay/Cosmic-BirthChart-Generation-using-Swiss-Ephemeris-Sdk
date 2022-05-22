using CosmicGameAPI.Entities;
using CosmicGameAPI.Model;
using CosmicGameAPI.Model.Response;
using CosmicGameAPI.Model.ViewModel;
using CosmicGameAPI.Service.Interface;
using CosmicGameAPI.Utility.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CosmicGameAPI.Service.Implementation
{
    public class ChartHolderService : IChartHolderService, IDisposable
    {
        private readonly CosmicDbContext _cosmicDbContext;
        private readonly ICommonService _commonService;
        private readonly IBaseAutoMapper<ChartHolderViewModel, ChartHolder> _mapToViewModel;
        public ChartHolderService(CosmicDbContext cosmicDbContext, ICommonService commonService, IBaseAutoMapper<ChartHolderViewModel, ChartHolder> mapToViewModel)
        {
            _cosmicDbContext = cosmicDbContext;
            _commonService = commonService;
            _mapToViewModel = mapToViewModel;
        }
        #region ChartHolder
        public async Task<ServiceResponse> LoadAllChartHolder(int userId)
        {
            ServiceResponse objResponse = new();
            try
            {
                if (userId > 0)
                {
                    var response = await _cosmicDbContext.ChartHolders.AsNoTracking().Where(p => p.UserId == userId).ToListAsync();
                    objResponse.result = response;
                    objResponse.message = "Successfully";
                    objResponse.success = true;
                }
                else
                {
                    objResponse.result = new List<ChartHolder>();
                    objResponse.success = false;
                    objResponse.message = "User has not any chart";
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> LoadAllChartHolder : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> ChartHolderById(int chartHolderID, int userId)
        {
            ServiceResponse objResponse = new();
            try
            {
                if (userId > 0)
                {
                    var chartHolder = await _cosmicDbContext.ChartHolders.AsNoTracking().Where(p => p.UserId == userId && p.ChartHolderId == chartHolderID).FirstOrDefaultAsync();
                    if (chartHolder is null)
                    {
                        objResponse.result = null;
                        objResponse.success = false;
                        objResponse.message = "ChartHolder not exist.";
                        return objResponse;
                    }
                    objResponse.result = chartHolder;
                    objResponse.success = true;
                    objResponse.message = "Sucessfully.";
                }
                else
                {
                    objResponse.result = null;
                    objResponse.success = false;
                    objResponse.message = "Invalid ChartId";
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.result = null;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> LoadSingleChartHolder : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> AddCharHolder(ChartHolder chartHolderViewModel)
        {
            ServiceResponse objResponse = new();
            try
            {   
                if(chartHolderViewModel == null)
                {
                    //return error response
                }
                var _charHolder = await _cosmicDbContext.ChartHolders.Where(p => p.ChartHolderId == chartHolderViewModel.ChartHolderId).FirstOrDefaultAsync();
                if(_charHolder == null)
                {
                    _cosmicDbContext.ChartHolders.Add(chartHolderViewModel);
                    await _cosmicDbContext.SaveChangesAsync();
                    objResponse.success = true;
                    objResponse.message = "Added successfully";
                    objResponse.status = HttpStatusCode.OK;
                }
                else
                {
                    objResponse.success = false;
                    objResponse.message = "Chart already exist";
                    objResponse.status = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> AddEditCharHolderRecord : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> EditCharHolder(ChartHolder chartHolderViewModel)
        {
            ServiceResponse objResponse = new();
            try
            {
                if (chartHolderViewModel.ChartHolderId <= 0)
                {
                    objResponse.success = false;
                    objResponse.message = "Invalid chartId";
                    objResponse.status = HttpStatusCode.OK;
                }
                var _charHolder = await _cosmicDbContext.ChartHolders.Where(p => p.UserId == chartHolderViewModel.UserId && p.ChartHolderId == chartHolderViewModel.ChartHolderId).FirstOrDefaultAsync();
                if (_charHolder != null)
                {
                    _cosmicDbContext.ChartHolders.Update(chartHolderViewModel);
                    await _cosmicDbContext.SaveChangesAsync();
                    objResponse.success = true;
                    objResponse.message = "Added successfully";
                    objResponse.status = HttpStatusCode.OK;
                }
                else
                {
                    objResponse.success = false;
                    objResponse.message = "Chart does not exist";
                    objResponse.status = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> AddEditCharHolderRecord : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> DeleteChartHolderRecord(int chartHolderID, int userId)
        {
            ServiceResponse objResponse = new();
            try
            {
                if (chartHolderID > 0)
                {
                    var chartHolder = await _cosmicDbContext.ChartHolders.Where(p => p.UserId == userId && p.ChartHolderId == chartHolderID).FirstOrDefaultAsync();
                    if (chartHolder is null)
                    {
                        objResponse.result = null;
                        objResponse.success = false;
                        objResponse.message = "ChartHolder not found.";
                    }
                    _cosmicDbContext.ChartHolders.Remove(chartHolder);
                    await _cosmicDbContext.SaveChangesAsync();
                    objResponse.result = chartHolder;
                    objResponse.success = true;
                }
                else
                {
                    objResponse.result = null;
                    objResponse.success = false;
                    objResponse.message = "Successfully";
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> LoadSingleChartHolder : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }

        #endregion

        #region CurrentAddress
        public async Task<ServiceResponse> GetAllCurrentAddress(int userId)
        {
            ServiceResponse objResponse = new();
            try
            {
                var response = await _cosmicDbContext.CurrentAddresses.AsNoTracking().Where(p => p.UserId == userId).ToListAsync();
                objResponse.result = response;
                objResponse.success = true;
                objResponse.message = "Successfully";
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> LoadAllChartHolder : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> GetCurrentAddressById(int addressID, int userId)
        {
            ServiceResponse objResponse = new();
            try
            {
                int usreId = 0; // userId from Jwt
                if (usreId > 0)
                {
                    var response = _cosmicDbContext.CurrentAddresses.AsNoTracking().Where(p => p.UserId == usreId && p.Id == addressID).ToListAsync();
                    objResponse.result = response;
                    objResponse.success = true;
                    objResponse.message = "Successfully";
                }
                else
                {
                    objResponse.result = null;
                    objResponse.success = false;
                    objResponse.message = "Something went wrong...!";
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> LoadSingleCurrentAddress : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> AddCurrentAddress(CurrentAddress addressRequest)
        {
            ServiceResponse objResponse = new();
            try
            {
                var getAddress = await _cosmicDbContext.CurrentAddresses.Where(p => p.Id == addressRequest.Id).FirstOrDefaultAsync();
                if (getAddress is null)
                {
                    _cosmicDbContext.CurrentAddresses.Add(getAddress);
                    await _cosmicDbContext.SaveChangesAsync();
                    objResponse.success = true;
                    objResponse.message = "Added successfully";
                    objResponse.status = HttpStatusCode.OK;
                }
                else
                {
                    objResponse.success = false;
                    objResponse.message = "Address does not exist";
                    objResponse.status = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> AddEditCharHolderRecord : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> EditCurrentAddress(int addressID, int userId)
        {
            ServiceResponse objResponse = new();
            try
            {
                var getAddress = await _cosmicDbContext.CurrentAddresses.Where(p => p.Id == addressID).FirstOrDefaultAsync();
                if (getAddress is null)
                {
                    _cosmicDbContext.CurrentAddresses.Update(getAddress);
                    await _cosmicDbContext.SaveChangesAsync();
                    objResponse.success = true;
                    objResponse.message = "Updated successfully";
                    objResponse.status = HttpStatusCode.OK;
                }
                else
                {
                    objResponse.success = false;
                    objResponse.message = "Address does not exist";
                    objResponse.status = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> AddEditCharHolderRecord : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> DeleteCurrentAddress(int addressID, int userId)
        {
            ServiceResponse objResponse = new();
            try
            {
                var getAddress = await _cosmicDbContext.CurrentAddresses.Where(p => p.Id == addressID).FirstOrDefaultAsync();
                if (getAddress is null)
                {
                    _cosmicDbContext.CurrentAddresses.Remove(getAddress);
                    await _cosmicDbContext.SaveChangesAsync();
                    objResponse.success = true;
                    objResponse.message = "Delete successfully";
                    objResponse.status = HttpStatusCode.OK;
                }
                else
                {
                    objResponse.success = false;
                    objResponse.message = "Address does not exist";
                    objResponse.status = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.ToString();
                objResponse.success = false;
                objResponse.status = HttpStatusCode.InternalServerError;
                await _commonService.SetErorr("ChartHolderService -> AddEditCharHolderRecord : " + ex.Message + "::" + ex.StackTrace);
            }
            return objResponse;
        }

        #endregion
        public async Task<ServiceResponse> LoadCountries()
        {
            try
            {
                ServiceResponse objResponse = new();
                try
                {
                    var countries = await _cosmicDbContext.Countries.AsNoTracking().Select(p => p.CountryName).OrderBy(p => p).ToListAsync();
                    objResponse.result = countries.Distinct().ToList();
                    objResponse.success = true;
                    objResponse.message = "Successfully";
                }
                catch (Exception ex)
                {
                    objResponse.message = ex.ToString();
                    objResponse.success = false;
                    objResponse.status = HttpStatusCode.InternalServerError;
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                await _commonService.SetErorr("ChartHolderService -> LoadCountries : " + ex.Message + "::" + ex.StackTrace);
                throw;
            }
        }
        public async Task<ServiceResponse> LoadTimeZone(string countryName)
        {
            try
            {
                ServiceResponse objResponse = new();
                var timeZones = _cosmicDbContext.Countries.AsNoTracking().Where(p => p.CountryName == countryName).Select(p => new TimeZoneModel { TimeZoneValueAreas = p.TimeZoneValue + " " + p.AreasCovered, ActualTimeZone = p.ActualTimeZone, TimeZoneValue = p.TimeZoneValue }).ToList().Distinct().OrderByDescending(p => p.TimeZoneValue).ToList();
                objResponse.result = timeZones;
                objResponse.success = true;
                objResponse.message = "Successfuly";
                return objResponse;
            }
            catch (Exception ex)
            {
                await _commonService.SetErorr("ChartHolderService -> LoadTimeZone : " + ex.Message + "::" + ex.StackTrace);
                throw;
            }
        }
        public void Dispose()
        {
            if (_cosmicDbContext != null)
            {
                _cosmicDbContext.Dispose();
            }
        }
    }
}
