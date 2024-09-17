using CORE.Entity;
using CORE.Repository;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Repository
{
    public class EFRepository<T> : IRepository<T> where T : EntityBase
    {
        protected ApplicationDbContext context;
        protected DbSet<T> dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync()
            => await this.dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await this.dbSet.FirstOrDefaultAsync(entity => entity.Id == id);

        public async Task AddAsync(T entidade)
        {
            entidade.DataCriacao = DateTime.Now;
            this.dbSet.Add(entidade);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entidade)
        {
            this.dbSet.Update(entidade);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var t = await this.GetByIdAsync(id);

            if (t is not null)
            {
                this.dbSet.Remove(t);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
