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

        public void Alterar(T entidade)
        {
            this.dbSet.Update(entidade);
            this.context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            entidade.DataCriacao = DateTime.Now;
            this.dbSet.Add(entidade);
            this.context.SaveChanges();
        }

        public void Deletar(int id)
        {
            this.dbSet.Remove(this.ObterPorId(id));
            this.context.SaveChanges();
        }

        public T ObterPorId(int id)
            => this.dbSet.FirstOrDefault(entity => entity.Id == id);

        public IList<T> ObterTodos()
            => this.dbSet.ToList();
    }
}
