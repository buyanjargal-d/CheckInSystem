using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CheckInSystem.Data;

public class CheckInDbContextFactory : IDesignTimeDbContextFactory<CheckInDbContext>
{
    public CheckInDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CheckInDbContext>();
        optionsBuilder.UseSqlite("Data Source=checkin.db");

        return new CheckInDbContext(optionsBuilder.Options);
    }
}
