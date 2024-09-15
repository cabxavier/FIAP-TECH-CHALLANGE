using CORE.Dto;
using CORE.Entity;

namespace CORE.Repository
{
    public interface IContatoRepository : IRepository<Contato>
    {
        IList<RegiaoDto> ObterContatoRegiaoPorDdd(int Ddd);
    }
}
