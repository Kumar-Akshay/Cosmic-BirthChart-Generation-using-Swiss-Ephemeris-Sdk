using CosmicGameAPI.Entities;
using CosmicGameAPI.Service.Interface;

namespace CosmicGameAPI.Service.Implementation
{
    public class CommonService : ICommonService , IDisposable
    {
        private readonly CosmicDbContext _cosmicDbContext;

        public CommonService(CosmicDbContext cosmicDbContext)
        {
            _cosmicDbContext = cosmicDbContext;
        }

        public void Dispose()
        {
            if (_cosmicDbContext != null)
            {
                _cosmicDbContext.Dispose();
            }
        }
        public async Task SetErorr(string description)
        {
            _cosmicDbContext.ErrorLogs.Add(new ErrorLog() { Date = DateTime.Now, Description = description });
            await _cosmicDbContext.SaveChangesAsync();
        }
    }
}
