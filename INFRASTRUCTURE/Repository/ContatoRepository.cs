using CORE.Dto;
using CORE.Entity;
using CORE.Repository;

namespace INFRASTRUCTURE.Repository
{
    public class ContatoRepository : EFRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IList<RegiaoDto> ObterContatoRegiaoPorDdd(int Ddd)
        {
            var listaRegiao = new List<RegiaoDto>();

            var lista = (from p in this.context.ContatoRegiao.ToList()
                         join c in this.context.Contato.ToList() on p.ContatoId equals c.Id
                         join r in this.context.Regiao.ToList() on p.RegiaoId equals r.Id
                         where r.Ddd == Ddd
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
