using CORE.Dto;
using CORE.Entity;

namespace CORE.Repository
{
    public interface IContatoRegiaoRepository : IRepository<ContatoRegiao>
    {
        Task<IList<ContatoRegiaoDto>> ObterContatoRegiaoTodos();
        Task<ContatoRegiaoDto> ObterContatoRegiaoTodosPorId(int id);
    }
}
