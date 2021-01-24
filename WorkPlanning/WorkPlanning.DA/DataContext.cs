using Microsoft.EntityFrameworkCore;
using System.Linq;
using WorkPlanning.DA.Models;

namespace WorkPlanning.DA
{
    internal class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<WorkerModel> Workers { get; set; }
        public DbSet<ShiftModel> Shifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var modelEntity = modelBuilder.Entity(entity.Name);
                // This sets the key Id and other common properties for all entities that inherit from BaseEntity, only the base type needs to be conf
                if (typeof(BaseEntity).IsAssignableFrom(entity.ClrType) && entity.BaseType == null)
                {
                    modelEntity.HasKey(nameof(BaseEntity.Guid));
                    modelEntity.HasIndex(nameof(BaseEntity.DateCreated)).IsUnique(false);
                }
            }
            modelBuilder.Model.GetEntityTypes()
              .SelectMany(e => e.GetForeignKeys())
              .ToList()
              .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Cascade);//enabled cascade delete
        }
    }
}
