using FluentAssertions;
using INFRASTRUCTURE.Repository;
using Microsoft.EntityFrameworkCore;
using TechChallange.Test.MockData;

namespace TechChallange.Test.RepositoryTests
{
    public class ContatoRepositoryTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;

        public ContatoRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            this._context = new ApplicationDbContext(options);
            this._context.Database.EnsureCreated();
        }        

        [Fact]
        public async Task GetAllAsync_ReturnContatoCollection()
        {
            _context.Contato.AddRange(ContatoMockData.GetAll());
            _context.SaveChanges();

            var sut = new ContatoRepository(_context);

            var result = await sut.GetAllAsync();
            result.Should().HaveCount(ContatoMockData.GetAll().Count);
        }

        public void Dispose()
        {
            this._context.Database.EnsureDeleted();
            this._context.Dispose();
        }
    }
}
