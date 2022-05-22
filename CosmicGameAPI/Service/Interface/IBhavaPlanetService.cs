using CosmicGameAPI.Model;

namespace CosmicGameAPI.Service.Interface
{
    public interface IBhavaPlanetService
    {
        List<BhavaAndPlanet> GetAstroChartData(ChartHolderViewModel userInfo, GlobalChartCreatorData chartData);
    }
}
