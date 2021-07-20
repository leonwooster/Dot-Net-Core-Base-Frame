using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Dksh.ePOD.Entities;

namespace Dksh.ePOD.Data
{
    public class DataContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging();
        }
        
        public DbSet<AuditTrailBO> AuditTrail { get; set; }
        public DbSet<AddressTypeBO> AddressType { get; set; }
    }
}
