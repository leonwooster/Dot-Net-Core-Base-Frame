using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Dksh.ePOD.Entities;

namespace Dksh.ePOD.Data
{
    public class AccessControlDA : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AccessControlDA(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("AccessControl"));
            options.EnableSensitiveDataLogging();
        }

        public DbSet<AccessControlBO> MST_AccessControl { get; set; }
    }
}
