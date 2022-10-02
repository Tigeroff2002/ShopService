using Microsoft.EntityFrameworkCore;
namespace ShopService.Import.Storage
{
    /// <summary xml:lang = "ru">
    /// Констекс базы данных сервиса "Импорта".
    /// </summary>
    public class ImportContext : DbContext
    {
        public ImportContext(DbContextOptions<ImportContext> options)
            : base(options) {}
        public DbSet<History> Histories { get; set; } = default!;

    }
}
