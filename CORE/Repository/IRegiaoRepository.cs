using CORE.Dto;
using CORE.Entity;

namespace CORE.Repository
{
    public interface IRegiaoRepository : IRepository<Regiao>
    {
        Task<RegiaoDto> GetByDddAsync(string Ddd);
    }
}
