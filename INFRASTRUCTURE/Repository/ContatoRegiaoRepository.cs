using CORE.Entity;
using CORE.Repository;

namespace INFRASTRUCTURE.Repository
{
    public class ContatoRegiaoRepository : EFRepository<ContatoRegiao>, IContatoRegiaoRepository
    {
        public ContatoRegiaoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
