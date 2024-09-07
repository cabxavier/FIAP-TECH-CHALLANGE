using CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INFRASTRUCTURE.Repository.Configuration
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("Contato");
            builder.HasKey(p => p.IdContato);
            builder.Property(p=>p.IdContato).HasColumnType("Int").ValueGeneratedNever().UseIdentityColumn();
            builder.Property(p => p.Nome).HasColumnType("VarChar(100)").IsRequired();
            builder.HasIndex(p => p.Telefone).IsUnique();
            builder.Property(p => p.Telefone).HasColumnType("VarChar(11)").IsRequired();
            builder.HasIndex(p => p.Email).IsUnique();
            builder.Property(p => p.Email).HasColumnType("VarChar(200)").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DateTime").IsRequired();
        }
    }
}
