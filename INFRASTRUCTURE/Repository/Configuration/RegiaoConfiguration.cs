﻿using CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INFRASTRUCTURE.Repository.Configuration
{
    public class RegiaoConfiguration : IEntityTypeConfiguration<Regiao>
    {
        public void Configure(EntityTypeBuilder<Regiao> builder)
        {
            builder.ToTable("Regiao");
            builder.HasKey(p => p.IdRegiao);
            builder.Property(p => p.IdRegiao).HasColumnType("Int").ValueGeneratedNever().UseIdentityColumn();
            builder.Property(p => p.Ddd).HasColumnType("Int").IsRequired();
            builder.HasIndex(p => p.Ddd).IsUnique();
            builder.Property(p => p.DataCriacao).HasColumnType("DateTime").IsRequired();
        }
    }
}