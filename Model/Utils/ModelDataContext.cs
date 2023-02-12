using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Utils.Converters;
using DateOnlyConverter = Model.Utils.Converters.DateOnlyConverter;

namespace Model.Utils
{
    public class ModelDataContext : DbContext
    {
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Album> Albums => Set<Album>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Minimal;Integrated Security=SSPI");
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
