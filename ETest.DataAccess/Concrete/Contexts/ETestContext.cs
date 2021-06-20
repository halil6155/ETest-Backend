using ETest.DataAccess.Extensions;
using ETest.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETest.DataAccess.Concrete.Contexts
{
    public class ETestContext:DbContext
    {
        protected readonly IConfiguration Configuration;

        public ETestContext(DbContextOptions<ETestContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MSSQL"))
                    .EnableSensitiveDataLogging());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapConfiguration();
            modelBuilder.CascadeConfiguration();
            modelBuilder.CustomSeedDate();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}