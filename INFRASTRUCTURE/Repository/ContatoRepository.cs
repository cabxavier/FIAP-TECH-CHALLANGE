using CORE.Dto;
using CORE.Entity;
using CORE.Repository;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Repository
{
    public class ContatoRepository : EFRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<RegiaoDto>> ObterContatoRegiaoPorDdd(string Ddd)
        {
            var listaRegiao = new List<RegiaoDto>();

            var lista = (from p in await this.context.ContatoRegiao.ToListAsync()
                         join c in await this.context.Contato.ToListAsync() on p.ContatoId equals c.Id
                         join r in await this.context.Regiao.ToListAsync() on p.RegiaoId equals r.Id
                         where r.Ddd.Equals(Ddd)
                         select new
                         {
                             r.Id,
                             r.Ddd,
                             r.DataCriacao,
                             ContatoId = c.Id,
                             ContatoNome = c.Nome,
                             ContatoTelefone = c.Telefone,
                             ContatoEmail = c.Email,
                             ContatoDataCriacao = c.DataCriacao
                         }).ToList();

            if ((lista is null ? 0 : lista.Count) > 0)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    var regiaoDto = new RegiaoDto()
                    {
                        Id = lista[i].Id,
                        Ddd = lista[i].Ddd,
                        DataCriacao = lista[i].DataCriacao,
                        Contatos = new List<ContatoDto>()
                    {
                        new ContatoDto()
                        {
                            Id = lista[i].ContatoId,
                            Nome = lista[i].ContatoNome,
                            Telefone = lista[i].ContatoTelefone,
                            Email = lista[i].ContatoEmail
                        }
                    }
                    };

                    listaRegiao.Add(regiaoDto);
                }
            }

            return listaRegiao;
        }
    }
}
