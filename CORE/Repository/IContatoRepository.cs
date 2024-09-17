using CORE.Dto;
using CORE.Entity;

namespace CORE.Repository
{
    public interface IContatoRepository : IRepository<Contato>
    {
       Task <IList<RegiaoDto>> ObterContatoRegiaoPorDdd(string Ddd);
    }
}
