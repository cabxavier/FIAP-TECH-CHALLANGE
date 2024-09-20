using CORE.Dto;
using CORE.Entity;

namespace CORE.Repository
{
    public interface IContatoRegiaoRepository : IRepository<ContatoRegiao>
    {
        Task<ContatoRegiaoDto> GetByContatoIdAndRegiaoIdAsync(int ContatoId, int RegiaoId);
        Task<IList<ContatoRegiaoDto>> GetContatoRegiaoAllAsync();
        Task<ContatoRegiaoDto> GetContatoRegiaoTodosByIdAsync(int id);
    }
}
