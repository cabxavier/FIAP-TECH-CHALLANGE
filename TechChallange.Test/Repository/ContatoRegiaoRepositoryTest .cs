using FluentAssertions;
using INFRASTRUCTURE.Repository;
using Microsoft.EntityFrameworkCore;
using TechChallange.Test.MockData;

namespace TechChallange.Test.RepositoryTests
{
    public class ContatoRegiaoRepositoryTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;

        public ContatoRegiaoRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            this._context = new ApplicationDbContext(options);
            this._context.Database.EnsureCreated();
        }        

        [Fact]
        public async Task GetAllAsync_ReturnContatoRegiaoCollection()
        {
            _context.ContatoRegiao.AddRange(ContatoRegiaoMockData.GetAll());
            _context.SaveChanges();

            var sut = new ContatoRegiaoRepository(_context);

            var result = await sut.GetAllAsync();
            result.Should().HaveCount(ContatoRegiaoMockData.GetAll().Count);
        }

        public void Dispose()
        {
            this._context.Database.EnsureDeleted();
            this._context.Dispose();
        }
    }
}
