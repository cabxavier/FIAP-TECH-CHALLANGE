using CORE.Dto;
using CORE.Entity;
using CORE.Repository;

namespace INFRASTRUCTURE.Repository
{
    public class RegiaoRepository : EFRepository<Regiao>, IRegiaoRepository
    {
        public RegiaoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
