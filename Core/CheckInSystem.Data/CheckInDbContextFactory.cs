using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CheckInSystem.Data;

/// <summary>
/// CheckInDbContextFactory ангилал нь Entity Framework Core-ийн өгөгдлийн сангийн контекстийг дизайн үе шатанд үүсгэхэд ашиглагдана.
/// Энэ нь миграци болон бусад дизайнтай холбоотой үйлдлүүдэд CheckInDbContext-ийг үүсгэхэд шаардлагатай.
/// </summary>
public class CheckInDbContextFactory : IDesignTimeDbContextFactory<CheckInDbContext>
{
    /// <summary>
    /// CheckInDbContext-ийн шинэ жишээг үүсгэнэ.
    /// Энэ арга нь өгөгдлийн сангийн тохиргоог (энд SQLite ашиглаж байна) тодорхойлж, контекстийг буцаана.
    /// </summary>
    /// <param name="args">Аргументууд (ихэвчлэн ашиглагддаггүй).</param>
    /// <returns>CheckInDbContext-ийн шинэ жишээ.</returns>
    public CheckInDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CheckInDbContext>();
        // SQLite өгөгдлийн сангийн холболтын мөрийг тохируулж байна
        optionsBuilder.UseSqlite("Data Source=checkin.db");

        return new CheckInDbContext(optionsBuilder.Options);
    }
}
