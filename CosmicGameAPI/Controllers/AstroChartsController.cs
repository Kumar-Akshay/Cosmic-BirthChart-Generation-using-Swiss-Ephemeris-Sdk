using CosmicGameAPI.Service.Helper;
using CosmicGameAPI.Service.Implementation;
using CosmicGameAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CosmicGameAPI.Controllers
{
    [Route("api/[controller]")]
    public class AstroChartsController : BaseController
    {
        private readonly IChartsService _chartsService;
        public IHttpContextAccessor HttpContextAccessor { get; }

        public AstroChartsController(IChartsService chartsService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _chartsService = chartsService;
            HttpContextAccessor = httpContextAccessor;
        }

        
        [HttpGet("GetTraditionalChart")]
        public async Task<IActionResult> GetTraditionalChart([FromQuery]int chartHolderId)
        {
            try
            {
                if(chartHolderId > 0)
                {
                    var response = await _chartsService.GetTraditionalChart(chartHolderId);
                    return Json(response);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet("GetBaskiChart")]
        public async Task<IActionResult> GetBaskiChart([FromQuery] int chartHolderId, string rectifiedTOB, int dayDifference)
        {
            try
            {
                if (chartHolderId > 0)
                {
                    var response = await _chartsService.GetBaskiChartAsync(chartHolderId,rectifiedTOB,dayDifference);
                    return Json(response);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //On GET vimso chart automatically loading dissai table
        [HttpGet("GetVimsoDissaiChart")]
        public async Task<IActionResult> GetVimsoDissaiChart(int chartHolderId)
        {
            try
            {
                var response = await _chartsService.GetVimsoDissaiLoad(chartHolderId);
                return Json(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //After clicking on one dissai we get here and loading puthi table
        [HttpGet("GetVimsoPuthiChart")]
        public async Task<IActionResult> GetVimsoPuthiChart(int chartHolderId, int dissaiGroupId)
        {
            try
            {
                var response = await _chartsService.GetPuthiForDissai(chartHolderId, dissaiGroupId);
                return Json(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //After clicking on one puthi we get here and loading antra+soksuma table
        [HttpGet("GetVimsoAntraSoksumaChart")]
        public async Task<IActionResult> GetVimsoAntraSoksumaChart(int chartHolderId, int puthiGroupId, int dissaiGroupId)
        {
            try
            {
                var response = await _chartsService.GetAntraSoksumaForPuthi(chartHolderId, puthiGroupId, dissaiGroupId);
                return Json(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //After clicking on one soksuma we get here and loading prana table
        [HttpGet("GetVimsoPranaChart")]
        public async Task<IActionResult> GetVimsoPranaChart(int chartHolderId, int puthiGroupId, int soksumaGroupId)
        {
            try
            {
                var response = await _chartsService.GetPranaForSoksuma(chartHolderId, puthiGroupId, soksumaGroupId);
                return Json(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
