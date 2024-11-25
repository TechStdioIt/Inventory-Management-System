using IMS.Infrastructure.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMS.Infrastructure.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationDbUser>
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _dbCon;
        private readonly bool _isdev;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
      : base(options)
        {
            _configuration = configuration;
            _isdev = _configuration.GetValue<bool>("IsDevelopment");
            _dbCon = _isdev ? "DevConnection" : "DevConnectionProduction";
            _connectionString = _configuration.GetConnectionString(_dbCon);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
