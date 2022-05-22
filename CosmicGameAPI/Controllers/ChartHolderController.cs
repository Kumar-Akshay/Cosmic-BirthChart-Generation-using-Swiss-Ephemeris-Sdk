using CosmicGameAPI.Entities;
using CosmicGameAPI.Model;
using CosmicGameAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CosmicGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartHolderController : BaseController
    {
        private readonly IChartHolderService _chartHolderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChartHolderController(IHttpContextAccessor httpContextAccessor, IChartHolderService chartHolderService) : base(httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _chartHolderService = chartHolderService;
        }

        [HttpGet("ChartHolderList")]
        public async Task<IActionResult> ChartHolderList()
        {
            var response = await _chartHolderService.LoadAllChartHolder(UserId);
            return Ok(response);
        }

        [HttpGet("ChartHolderById")]
        public async Task<IActionResult> ChartHolderById([FromQuery] int chartHolderID)
        {
            var response = await _chartHolderService.ChartHolderById(chartHolderID, UserId);
            if (response.result == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost("AddChartHolder")]
        public async Task<IActionResult> AddChartHolder([FromBody] ChartHolder chartHolderViewModel)
        {
            chartHolderViewModel.UserId = UserId;
            var response = await _chartHolderService.AddCharHolder(chartHolderViewModel);
            if (response.result is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPut("UpdateChartHolder")]
        public async Task<IActionResult> UpdateChartHolder([FromBody] ChartHolder chartHolderViewModel)
        {
            chartHolderViewModel.UserId = UserId;
            var response = await _chartHolderService.EditCharHolder(chartHolderViewModel);
            if (response.result is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("RemoveChartHolder")]
        public async Task<IActionResult> RemoveChartHolder([FromQuery] int chartHolderID)
        {
            var response = await _chartHolderService.DeleteChartHolderRecord(chartHolderID, UserId);
            if (response.result == null)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet("Countries")]
        public async Task<IActionResult> Countries()
        {
            var response = await _chartHolderService.LoadCountries();
            return Ok(response);
        }

        [HttpGet("TimeZone")]
        public async Task<IActionResult> TimeZone([FromQuery] string countryName)
        {
            var response = await _chartHolderService.LoadTimeZone(countryName);
            return Ok(response);
        }

        [HttpGet("GetAllCurrentAddress")]
        public async Task<IActionResult> GetAllCurrentAddress()
        {
            var response = await _chartHolderService.GetAllCurrentAddress(UserId);
            return Ok(response);
        }

        [HttpPost("AddCurrentAddress")]
        public async Task<IActionResult> AddEditCurrentAddress([FromBody] CurrentAddress addressRequest)
        {
            addressRequest.UserId = UserId;
            var response = await _chartHolderService.AddCurrentAddress(addressRequest);
            return Ok(response);
        }

        [HttpGet("GetCurrentAddressById")]
        public async Task<IActionResult> GetCurrentAddressById([FromQuery] int addressID)
        {
            var response = await _chartHolderService.GetCurrentAddressById(addressID, UserId);
            return Ok(response);
        }

        [HttpGet("EditCurrentAddress")]
        public async Task<IActionResult> EditCurrentAddress([FromQuery] int addressID)
        {
            var response = await _chartHolderService.EditCurrentAddress(addressID, UserId);
            return Ok(response);
        }

        [HttpGet("RemoveCurrentAddress")]
        public async Task<IActionResult> RemoveCurrentAddress([FromQuery] int addressID)
        {
            var response = await _chartHolderService.DeleteChartHolderRecord(addressID, UserId);
            return Ok(response);
        }
    }
}
