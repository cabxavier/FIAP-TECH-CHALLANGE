using FluentAssertions;
using INFRASTRUCTURE.Repository;
using Microsoft.EntityFrameworkCore;
using TechChallange.Test.MockData;

namespace TechChallange.Test.RepositoryTests
{
    public class TestContatoRepository : IDisposable
    {
        protected readonly ApplicationDbContext _context;

        public TestContatoRepository()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            this._context = new ApplicationDbContext(options);
            this._context.Database.EnsureCreated();
        }        

        [Fact]
        public async Task GetContatosAsync_ReturnTarefaCollection()
        {
            _context.Contato.AddRange(ContatoMockData.GetContatoAll());
            _context.SaveChanges();

            var sut = new ContatoRepository(_context);

            var result = await sut.GetAllAsync();
            result.Should().HaveCount(ContatoMockData.GetContatoAll().Count);
        }

        public void Dispose()
        {
            this._context.Database.EnsureDeleted();
            this._context.Dispose();
        }
    }
}
