using CosmicGameAPI.Entities;
using CosmicGameAPI.Model;
using CosmicGameAPI.Model.Request;
using CosmicGameAPI.Model.ViewModel;
using CosmicGameAPI.Model.ViewModel.BaskiChart;
using CosmicGameAPI.Model.ViewModel.VimsoChart;
using CosmicGameAPI.Service.Implementation;
using CosmicGameAPI.Service.Interface;
using CosmicGameAPI.Utility.Mapper;
using CosmicGameAPI.Utility.SDK;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CosmicGameAPI.Service.Implementation
{
    public class ChartsService : IChartsService, IDisposable
    {
        private readonly CosmicDbContext _cosmicDbContext;
        private readonly ICommonService _commonService;
        private readonly IBhavaPlanetService _bhavaPlanetService;
        private readonly IBaseAutoMapper<ChartHolder, ChartHolderViewModel> _mapToViewModel;
        

        public ChartsService(CosmicDbContext cosmicDbContext, IBaseAutoMapper<ChartHolder, ChartHolderViewModel> mapToViewModel, ICommonService commonService, IBhavaPlanetService bhavaPlanetService)
        {
            _cosmicDbContext = cosmicDbContext;
            _commonService = commonService;
            _bhavaPlanetService = bhavaPlanetService;
            _mapToViewModel = mapToViewModel;
        }

        public void Dispose()
        {
            if (_cosmicDbContext != null)
            {
                _cosmicDbContext.Dispose();
            }
        }

        public async Task<dynamic> GetTraditionalChart(int chartHolderId)
        {
            //Verify the user address exist or not

            //var userId = _userManager.GetUserId(HttpContext.User);
            //var hasAddress = _context.CustCurrentAddress.Any(x => x.UserId == userid);
            //if (!hasAddress)
            //{
            //    //return RedirectToAction("Create", "CustomAddress", new { returnUrl = "/ChartHolders/AstroChart?chartHolderId=" + chartHolderId });
            //}

            // Retrive the chart holder information
            var chartHolder = await _cosmicDbContext.ChartHolders.Where(x => x.ChartHolderId == chartHolderId).FirstOrDefaultAsync();
            if (chartHolder is null)
            {
                // return chartHolder not exist
            }
            //  Chart model map to chartholder table
            var chartHolderViewModel = _mapToViewModel.ToSingle(chartHolder);
            //var _date = new DateTime(chartHolder.DOB.Year, chartHolder.DOB.Month, chartHolder.DOB.Day, Convert.ToInt32(chartHolder.TOB.Hour), Convert.ToInt32(chartHolder.TOB.Minute), Convert.ToInt32(chartHolder.TOB.Second));
            //in bad case it will do same thing twice

            GlobalChartCreatorData chartData = new();
            var lstBhavaAndPlanet = _bhavaPlanetService.GetAstroChartData(chartHolderViewModel, chartData);
            var traditionalData = ChartCreator.GetTraditionalChart(lstBhavaAndPlanet);
            //var traditionalDataViewModel = traditionalData.CreateTableBody();

            return new
            {
                ChartHolder = chartHolderViewModel,
                TraditionalChartData = traditionalData,
            };
        }
        public async Task<dynamic> GetBaskiChartAsync(int chartHolderId, string rectifiedTOB, int dayDifference)
        {
            var BaskiChart = new BaskiChartViewModel();
            if (!string.IsNullOrEmpty(rectifiedTOB))
            {
                var retifiedTime = TimeSpan.Parse(rectifiedTOB);
                var chartHolder = await _cosmicDbContext.ChartHolders.Where(p=>p.ChartHolderId == chartHolderId).FirstOrDefaultAsync();
                chartHolder.DOB = dayDifference == 0 ? chartHolder.DOB : chartHolder.DOB.AddDays(dayDifference);
                chartHolder.RectifiedTOB = retifiedTime;
                chartHolder.BirthTimeRectified = true;
                _cosmicDbContext.Update(chartHolder);
                await _cosmicDbContext.SaveChangesAsync();

                var date = new DateTime(
                    chartHolder.DOB.Year,
                    chartHolder.DOB.Month,
                    chartHolder.DOB.Day,
                               Convert.ToInt32(retifiedTime.Hours),
                               Convert.ToInt32(retifiedTime.Minutes),
                               Convert.ToInt32(retifiedTime.Seconds));

                date = date.AddDays(dayDifference);

                var chartHolderViewModel = _mapToViewModel.ToSingle(_cosmicDbContext.ChartHolders.Where(a => a.ChartHolderId == chartHolderId).FirstOrDefault());

                chartHolderViewModel.DOB = date;
                chartHolderViewModel.TOB = date;

                //  Astro chart creator class sdk configure for astro chart
                GlobalChartCreatorData chartData = new();
                var lstBhavaAndPlanet = _bhavaPlanetService.GetAstroChartData(chartHolderViewModel, chartData);
                BaskiChart = ChartCreator.GetBaskiChartGrid(_cosmicDbContext, lstBhavaAndPlanet);

                return BaskiChart;
            }
            else
            {
                return "RectifiedTOB or dayDifference can not be empty";
            }
        }

        #region Vimso Chart
        public async Task<dynamic> GetVimsoDissaiLoad(int chartHolderId)
        {
            var charholder = await _cosmicDbContext.ChartHolders.Where(p => p.ChartHolderId == chartHolderId).FirstOrDefaultAsync();
            if (charholder is null)
            {
                return "CharHolder does not exist";
            }
            var chartHolderViewModel = _mapToViewModel.ToSingle(charholder);
            var lstBhavaAndPlanet = _bhavaPlanetService.GetAstroChartData(chartHolderViewModel, new GlobalChartCreatorData());

            //  Set chart birth date to original date.
            var birthDate = new DateTime(chartHolderViewModel.DOB.Year, chartHolderViewModel.DOB.Month, chartHolderViewModel.DOB.Day, Convert.ToInt32(chartHolderViewModel.TOB.Hour),Convert.ToInt32(chartHolderViewModel.TOB.Minute),Convert.ToInt32(chartHolderViewModel.TOB.Second));

            if(lstBhavaAndPlanet.Count == 0)
            {
                return "Something went wrong try again";
            }
            var moonDegreeValue = lstBhavaAndPlanet.Where(t => t.Item_Name == "Moon").FirstOrDefault().Location_DegDig;
            return await GetVimsoDissaiAsync(moonDegreeValue, birthDate);
        }
        public async Task<dynamic> GetPuthiForDissai(int chartHolderId,int dissaiGroupId)
        {
            var charholder = await _cosmicDbContext.ChartHolders.Where(p => p.ChartHolderId == chartHolderId).FirstOrDefaultAsync();
            if (charholder is null)
            {
                return "CharHolder does not exist";
            }
            if(dissaiGroupId > 0)
            {
                return "Invalid Dissai Group Id";
            }
            var chartHolderViewModel = _mapToViewModel.ToSingle(charholder);
            var lstBhavaAndPlanet = _bhavaPlanetService.GetAstroChartData(chartHolderViewModel, new GlobalChartCreatorData());

            //  Set chart birth date to original date.
            var birthDate = new DateTime(chartHolderViewModel.DOB.Year, chartHolderViewModel.DOB.Month, chartHolderViewModel.DOB.Day, Convert.ToInt32(chartHolderViewModel.TOB.Hour), Convert.ToInt32(chartHolderViewModel.TOB.Minute), Convert.ToInt32(chartHolderViewModel.TOB.Second));

            if (lstBhavaAndPlanet.Count == 0)
            {
                return "Something went wrong try again";
            }
            var moonDegreeValue = lstBhavaAndPlanet.Where(t => t.Item_Name == "Moon").FirstOrDefault().Location_DegDig;

            var currentDissai = await _cosmicDbContext.VimsoMasterRegisters.Where(x => x.Di_Gp == dissaiGroupId).Select(x => new VimsoDTO { VimsoPeriod = x.Vimso_pd, Name = x.S1SL, Gp = x.Pu_Gp, MovingDistance = x.S4SL_ArcDist }).ToListAsync();
            var PuthiChart = ChartCreator.GetVimsoChart(currentDissai, birthDate, moonDegreeValue);


            // Age Calculation
            VimsoChartCell neighbourDissai = new();
            VimsoChartViewModel Dissai = await GetVimsoDissaiAsync(moonDegreeValue, birthDate);
            var selectDissai = Dissai.Chart.Where(p => p.Gp == dissaiGroupId).FirstOrDefault();
            if (selectDissai != null)
            {
                neighbourDissai = Dissai.Chart.Where(p => p.Gp == dissaiGroupId - 1).FirstOrDefault();
            }
            var Age = CalculateAge(selectDissai, neighbourDissai, birthDate);

            return new { Age, PuthiChart };
        }
        public async Task<dynamic> GetAntraSoksumaForPuthi(int chartHolderId, int puthiGroupId, int dissaiGroupId)
        {
            var charholder = await _cosmicDbContext.ChartHolders.Where(p => p.ChartHolderId == chartHolderId).FirstOrDefaultAsync();
            if (charholder is null)
            {
                return "CharHolder does not exist";
            }
            if(puthiGroupId > 0)
            {
                return "Invalid Puthin Group Id";
            }
            if (dissaiGroupId > 0)
            {
                return "Invalid Dissai Group Id";
            }
            var chartHolderViewModel = _mapToViewModel.ToSingle(charholder);
            var lstBhavaAndPlanet = _bhavaPlanetService.GetAstroChartData(chartHolderViewModel, new GlobalChartCreatorData());

            //  Set chart birth date to original date.
            var birthDate = new DateTime(chartHolderViewModel.DOB.Year, chartHolderViewModel.DOB.Month, chartHolderViewModel.DOB.Day, Convert.ToInt32(chartHolderViewModel.TOB.Hour), Convert.ToInt32(chartHolderViewModel.TOB.Minute), Convert.ToInt32(chartHolderViewModel.TOB.Second));

            if (lstBhavaAndPlanet.Count == 0)
            {
                return "Something went wrong try again";
            }
            var moonDegreeValue = lstBhavaAndPlanet.Where(t => t.Item_Name == "Moon").FirstOrDefault().Location_DegDig;

            var currentPuthi = await _cosmicDbContext.VimsoMasterRegisters.Where(x => x.Pu_Gp == puthiGroupId).Select(x => new VimsoDTO { VimsoPeriod = x.Vimso_pd, Name = x.S2SL, Gp = x.An_Gp, MovingDistance = x.S4SL_ArcDist }).ToListAsync();
            var AntraChart = ChartCreator.GetVimsoChart(currentPuthi, birthDate, moonDegreeValue);

            var result = new AntraSoksumaChartViewModel();
            foreach (var antra in AntraChart.Chart)
            {
                var currentAntra = await _cosmicDbContext.VimsoMasterRegisters.Where(x => x.An_Gp == antra.Gp).Select(x => new VimsoDTO { VimsoPeriod = x.Vimso_pd, Name = x.S3SL, Gp = x.So_Gp, MovingDistance = x.S4SL_ArcDist }).ToListAsync();
                var SoksumaChart = ChartCreator.GetVimsoChart(currentAntra, antra.Date, moonDegreeValue);
                result.Chart.Add(new AntraSoksumaRow { Antra = antra, Soksumas = SoksumaChart.Chart });
            }


            // Age Calculation
            VimsoChartCell neighbour = new();
            VimsoChartCell selected = new();

            var currentDissai = await _cosmicDbContext.VimsoMasterRegisters.Where(x => x.Di_Gp == dissaiGroupId).Select(x => new VimsoDTO { VimsoPeriod = x.Vimso_pd, Name = x.S1SL, Gp = x.Pu_Gp, MovingDistance = x.S4SL_ArcDist }).ToListAsync();
            var PuthiChart = ChartCreator.GetVimsoChart(currentDissai, birthDate, moonDegreeValue);

            selected = PuthiChart.Chart.Where(p => p.Gp == puthiGroupId).FirstOrDefault();
            if(selected != null)
            {
                neighbour = PuthiChart.Chart.Where(p => p.Gp == puthiGroupId - 1).FirstOrDefault();
            }
            var Age = CalculateAge(selected, neighbour, birthDate);

            return new { Age, result };
        }
        public async Task<dynamic> GetPranaForSoksuma(int chartHolderId, int puthiGroupId, int soksumaGroupId)
        {
            var charholder = await _cosmicDbContext.ChartHolders.Where(p => p.ChartHolderId == chartHolderId).FirstOrDefaultAsync();
            if (charholder is null)
            {
                return "CharHolder does not exist";
            }
            if (puthiGroupId > 0)
            {
                return "Invalid Puthin Group Id";
            }
            if (soksumaGroupId > 0)
            {
                return "Invalid Soksuma Group Id";
            }
            var chartHolderViewModel = _mapToViewModel.ToSingle(charholder);
            var lstBhavaAndPlanet = _bhavaPlanetService.GetAstroChartData(chartHolderViewModel, new GlobalChartCreatorData());

            //  Set chart birth date to original date.
            var birthDate = new DateTime(chartHolderViewModel.DOB.Year, chartHolderViewModel.DOB.Month, chartHolderViewModel.DOB.Day, Convert.ToInt32(chartHolderViewModel.TOB.Hour), Convert.ToInt32(chartHolderViewModel.TOB.Minute), Convert.ToInt32(chartHolderViewModel.TOB.Second));

            if (lstBhavaAndPlanet.Count == 0)
            {
                return "Something went wrong try again";
            }
            var moonDegreeValue = lstBhavaAndPlanet.Where(t => t.Item_Name == "Moon").FirstOrDefault().Location_DegDig;

            var currentSoksuma = await _cosmicDbContext.VimsoMasterRegisters.Where(x => x.So_Gp == soksumaGroupId).Select(x => new VimsoDTO { VimsoPeriod = x.Vimso_pd, Name = x.S4SL, Gp = x.LineNo, MovingDistance = x.S4SL_ArcDist }).ToListAsync();
            var PranaChart = ChartCreator.GetVimsoChart(currentSoksuma, birthDate, moonDegreeValue);


            // Age Calculation
            VimsoChartCell neighbour = new();
            VimsoChartCell selected = new();
            var currentPuthi = await _cosmicDbContext.VimsoMasterRegisters.Where(x => x.Pu_Gp == puthiGroupId).Select(x => new VimsoDTO { VimsoPeriod = x.Vimso_pd, Name = x.S2SL, Gp = x.An_Gp, MovingDistance = x.S4SL_ArcDist }).ToListAsync();
            var AntraChart = ChartCreator.GetVimsoChart(currentPuthi, birthDate, moonDegreeValue);

            var result = new AntraSoksumaChartViewModel();
            foreach (var antra in AntraChart.Chart)
            {
                var currentAntra = await _cosmicDbContext.VimsoMasterRegisters.Where(x => x.An_Gp == antra.Gp).Select(x => new VimsoDTO { VimsoPeriod = x.Vimso_pd, Name = x.S3SL, Gp = x.So_Gp, MovingDistance = x.S4SL_ArcDist }).ToListAsync();
                var SoksumaChart = ChartCreator.GetVimsoChart(currentAntra, antra.Date, moonDegreeValue);
                selected = SoksumaChart.Chart.Where(p => p.Gp == soksumaGroupId).FirstOrDefault();
                if (selected != null)
                {
                    neighbour = SoksumaChart.Chart.Where(p => p.Gp == soksumaGroupId - 1).FirstOrDefault();
                    break;
                }
            }
            var Age = CalculateAge(selected, neighbour, birthDate);

            return new { Age, PranaChart};
        }

        private string CalculateAge(VimsoChartCell obj, VimsoChartCell neighbour, DateTime birthDate)
        {
            var text = string.Empty;
            if(neighbour != null)
            {
                var neighbourDate = neighbour.Date;
                text += (neighbourDate.Year - birthDate.Year) + " years " + (neighbourDate.Month - birthDate.Month) + " months " + (neighbourDate.Day - birthDate.Day) + " days ";
            }
            text += " TO ";
            if (obj != null)
            {
                var currentDate = obj.Date;
                text += (currentDate.Year - birthDate.Year) + " years " + (currentDate.Month - birthDate.Month) + " months " + (currentDate.Day - birthDate.Day) + " days ";
            }
            return text.Replace("-","");
        }

        #endregion


        #region Private methods
        
        private async Task<VimsoChartViewModel> GetVimsoDissaiAsync(double moonDegreeValue,DateTime birthDate)
        {
            var VimsoData = ChartCreator.GetVimsoDataQuery(_cosmicDbContext, moonDegreeValue);
            var VimsoDissaiData = await VimsoData.Select(x => new VimsoDTO { VimsoPeriod = x.Vimso_pd, Name = x.StarLord, Gp = x.Di_Gp, MovingDistance = x.S4SL_ArcDist }).ToListAsync();
            var DissaiChart = ChartCreator.GetVimsoChart(VimsoDissaiData, birthDate, moonDegreeValue);
            return DissaiChart;
        }

        #endregion
    }
}
