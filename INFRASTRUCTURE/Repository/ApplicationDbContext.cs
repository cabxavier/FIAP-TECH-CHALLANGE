using CORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contato> Contato { get; set; }
        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<ContatoRegiao> ContatoRegiao { get; set; }       
    }
}
