using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using time_tracker_case.Models;

namespace time_tracker_case.Contexts;

public class IdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    protected readonly IConfiguration _configuration;

    public IdentityDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(
            _configuration.GetConnectionString("DefaultConnection"),
            options =>
            {
                options.SetPostgresVersion(new Version("9.6"));
            }
        );
    }

    public DbSet<TimeRecord> TimeRecords { get; set; }
    public DbSet<Project> Projects { get; set; }
}
