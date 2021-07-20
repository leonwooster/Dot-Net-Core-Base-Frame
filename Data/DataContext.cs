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

        public DbSet<ExternalQuestionnaireBO> tbl_DKSH_TPI_EXTQNS_REQUEST { get; set; }
        public DbSet<ExternalQuestionaireFileBO> tbl_DKSH_TPI_EXTQNS_REQUEST_FILES { get; set; }
        public DbSet<CommonDataBO> tbl_DKSH_TPI_KEYWORDS { get; set; }
        public DbSet<AuditTrailBO> tbl_DKSH_TPI_AUDIT_TRAIL { get; set; }
        public DbSet<ExternalQuestionnaireMYBO> tbl_DKSH_TPI_EXTQNS_REQUEST_EXT_MY { get; set; }
    }
}
