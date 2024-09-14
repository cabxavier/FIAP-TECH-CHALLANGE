using CORE.Dto;
using CORE.Entity;

namespace CORE.Repository
{
    public interface IContatoRegiaoRepository : IRepository<ContatoRegiao>
    {
        IList<ContatoRegiaoDto> ObterContatoRegiaoTodos();
        ContatoRegiaoDto ObterContatoRegiaoTodosPorId(int id);
    }
}
