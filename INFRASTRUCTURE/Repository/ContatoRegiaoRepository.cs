using CORE.Dto;
using CORE.Entity;
using CORE.Repository;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Repository
{
    public class ContatoRegiaoRepository : EFRepository<ContatoRegiao>, IContatoRegiaoRepository
    {
        public ContatoRegiaoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<ContatoRegiaoDto>> ObterContatoRegiaoTodos()
        {
            var listContatoRegiaoDto = new List<ContatoRegiaoDto>();
            var listContatoRegiao =  await this.context.ContatoRegiao.ToListAsync();

            if ((listContatoRegiao is null ? 0 : listContatoRegiao.Count) > 0)
            {
                for (int i = 0; i < listContatoRegiao.Count; i++)
                {
                    var contatoRegiao = listContatoRegiao[i];

                    var contato = await this.context.Contato.FirstOrDefaultAsync(p => p.Id == contatoRegiao.ContatoId);
                    var regiao = await this.context.Regiao.FirstOrDefaultAsync(p => p.Id == contatoRegiao.RegiaoId);

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

        public async Task< ContatoRegiaoDto> ObterContatoRegiaoTodosPorId(int id)
        {
            var contatoRegiao = await this.context.ContatoRegiao
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new Exception("Esse contato região não existe");

            var contato = await this.context.Contato.FirstOrDefaultAsync(p => p.Id == contatoRegiao.ContatoId);
            var regiao = await this.context.Regiao.FirstOrDefaultAsync(p => p.Id == contatoRegiao.RegiaoId);

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
