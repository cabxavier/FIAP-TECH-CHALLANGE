using CORE.Dto;
using CORE.Entity;

namespace CORE.Repository
{
    public interface IContatoRepository : IRepository<Contato>
    {
        Task<ContatoDto> GetByTelefoneAsync(string Telefone);
        Task<ContatoDto> GetByEmailAsync(string Email);
        Task<ContatoDto> GetByNomeAndTelefoneAndEmailAsync(string Nome, string Telefone, string Email);
        Task <IList<RegiaoDto>> GetContatoRegiaoByDddAsync(string Ddd);
    }
}
