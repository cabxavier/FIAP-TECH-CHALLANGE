using CORE.Dto;
using CORE.Entity;
using CORE.Repository;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Repository
{
    public class RegiaoRepository : EFRepository<Regiao>, IRegiaoRepository
    {
        public RegiaoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<RegiaoDto> GetByDddAsync(string Ddd)
        {
            RegiaoDto regiaoDto = null;

            var regiao = await this.context.Regiao.FirstOrDefaultAsync(p => p.Ddd.Equals(Ddd));

            if(regiao is not null)
            {
                regiaoDto = new RegiaoDto()
                {
                    Id = regiao.Id,
                    Ddd = regiao.Ddd,
                    DataCriacao = regiao.DataCriacao,
                };
            }

            return regiaoDto;
        }
    }
}
