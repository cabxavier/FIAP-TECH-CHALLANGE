using CORE.Entity;
using CORE.Repository;

namespace INFRASTRUCTURE.Repository
{
    public class ContatoRepository : EFRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
