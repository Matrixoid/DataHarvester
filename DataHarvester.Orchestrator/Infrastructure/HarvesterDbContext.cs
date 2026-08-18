using DataHarvester.Orchestrator.Domain.Jobs;
using Microsoft.EntityFrameworkCore;

namespace DataHarvester.Orchestrator.Infrastructure
{
    /// <summary>
    /// Контекст базы данных Entity Framework для работы с таблицами приложения.
    /// </summary>
    public class HarvesterDbContext : DbContext
    {
        /// <summary>
        /// Таблица задач сбора данных.
        /// </summary>
        public DbSet<HarvestingJob> Jobs { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса заданным значением <paramref name="options"/>.
        /// </summary>
        /// <param name="options">Настройки контекста.</param>
        public HarvesterDbContext(DbContextOptions<HarvesterDbContext> options) : base(options)
        {
        }

        protected HarvesterDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HarvestingJob>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Url).IsRequired().HasMaxLength(1024);
                entity.Property(e => e.Status).HasConversion<string>();
            });
        }
    }
}
