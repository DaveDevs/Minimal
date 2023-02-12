﻿using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Utils.Converters;

namespace Model.Utils;

public class ModelDataContext : DbContext
{
    public ModelDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Album> Albums => Set<Album>();

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");

        base.ConfigureConventions(builder);
    }
}