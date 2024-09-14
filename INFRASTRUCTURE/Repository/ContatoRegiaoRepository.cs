using CORE.Dto;
using CORE.Entity;
using CORE.Repository;

namespace INFRASTRUCTURE.Repository
{
    public class ContatoRegiaoRepository : EFRepository<ContatoRegiao>, IContatoRegiaoRepository
    {
        public ContatoRegiaoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IList<ContatoRegiaoDto> ObterContatoRegiaoTodos()
        {
            var listContatoRegiaoDto = new List<ContatoRegiaoDto>();
            var listContatoRegiao = this.context.ContatoRegiao.ToList();

            if ((listContatoRegiao is null ? 0 : listContatoRegiao.Count) > 0)
            {
                for (int i = 0; i < listContatoRegiao.Count; i++)
                {
                    var contatoRegiao = listContatoRegiao[i];

                    var contato = this.context.Contato.FirstOrDefault(p => p.Id == contatoRegiao.ContatoId);
                    var regiao = this.context.Regiao.FirstOrDefault(p => p.Id == contatoRegiao.RegiaoId);

                    var contatoRegiaoDto = new ContatoRegiaoDto()
                    {
                        Id = contatoRegiao.Id,
                        ContatoId = contatoRegiao.ContatoId,
                        RegiaoId = contatoRegiao.RegiaoId,
                        DataCriacao = contatoRegiao.DataCriacao,
                        Contato = new ContatoDto()
                        {
                            Id = contato.Id,
                            Nome = contato.Nome,
                            Telefone = contato.Telefone,
                            Email = contato.Email,
                            DataCriacao = contato.DataCriacao
                        },
                        Regiao = new RegiaoDto()
                        {
                            Id = regiao.Id,
                            Ddd = regiao.Ddd,
                            DataCriacao = regiao.DataCriacao
                        }
                    };

                    listContatoRegiaoDto.Add(contatoRegiaoDto);
                }
            }

            return listContatoRegiaoDto;
        }

        public ContatoRegiaoDto ObterContatoRegiaoTodosPorId(int id)
        {
            var contatoRegiao = this.context.ContatoRegiao
                .FirstOrDefault(p => p.Id == id)
                ?? throw new Exception("Esse contato região não existe");

            var contato = this.context.Contato.FirstOrDefault(p => p.Id == contatoRegiao.ContatoId);
            var regiao = this.context.Regiao.FirstOrDefault(p => p.Id == contatoRegiao.RegiaoId);

            return new ContatoRegiaoDto()
            {
                Id = contatoRegiao.Id,
                ContatoId = contatoRegiao.ContatoId,
                RegiaoId = contatoRegiao.RegiaoId,
                DataCriacao = contatoRegiao.DataCriacao,
                Contato = new ContatoDto()
                {
                    Id = contato.Id,
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    Email = contato.Email,
                    DataCriacao = contato.DataCriacao
                },
                Regiao = new RegiaoDto()
                {
                    Id = regiao.Id,
                    Ddd = regiao.Ddd,
                    DataCriacao = regiao.DataCriacao
                }
            };
        }
    }
}
