using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using time_tracker_case.Models;

namespace time_tracker_case.Contexts;

public class IdentityDbContext : IdentityDbContext<ApplicationUser>
{
    protected readonly IConfiguration _configuration;

    public IdentityDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<TimeRecord> TimeRecords { get; set; }
}
