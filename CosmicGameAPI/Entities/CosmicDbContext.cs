using Microsoft.EntityFrameworkCore;

namespace CosmicGameAPI.Entities
{
    public class CosmicDbContext : DbContext
    {
        public CosmicDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<BhavaPlanet> BhavaPlanets { get; set; }
        public DbSet<ChartHolder> ChartHolders { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CurrentAddress> CurrentAddresses { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginInfo> UserLoginInfos { get; set; }
        public DbSet<VimsoMasterRegister> VimsoMasterRegisters { get; set; }
        public DbSet<U_Lev4_S4SL_Register> u_Lev4_S4SL_Registers { get; set; }
    }
}
