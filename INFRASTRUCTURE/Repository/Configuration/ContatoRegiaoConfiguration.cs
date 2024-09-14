using CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INFRASTRUCTURE.Repository.Configuration
{
    public class ContatoRegiaoConfiguration : IEntityTypeConfiguration<ContatoRegiao>
    {
        public void Configure(EntityTypeBuilder<ContatoRegiao> builder)
        {
            builder.ToTable("ContatoRegiao");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("Int").UseIdentityColumn();
            builder.Property(p => p.ContatoId).HasColumnType("Int").IsRequired();
            builder.Property(p => p.RegiaoId).HasColumnType("Int").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DateTime").IsRequired();

            builder.HasOne(p => p.Contato)
           .WithMany(c => c.ContatosRegioes)
           .HasPrincipalKey(c => c.Id);

            builder.HasOne(p => p.Regiao)
            .WithMany(c => c.ContatosRegioes)
            .HasPrincipalKey(c => c.Id);

            builder.HasIndex(p => new { p.ContatoId });
            builder.HasIndex(p => new { p.RegiaoId });
        }
    }
}
