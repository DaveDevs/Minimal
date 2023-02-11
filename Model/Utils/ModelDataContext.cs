using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Utils.Converters;
using DateOnlyConverter = Model.Utils.Converters.DateOnlyConverter;

namespace Model.Utils
{
    public class ModelDataContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=StoreDB;");
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            base.ConfigureConventions(builder);
        }
    }
}
