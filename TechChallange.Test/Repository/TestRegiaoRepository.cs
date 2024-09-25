using FluentAssertions;
using INFRASTRUCTURE.Repository;
using Microsoft.EntityFrameworkCore;
using TechChallange.Test.MockData;

namespace TechChallange.Test.RepositoryTests
{
    public class TestRegiaoRepository : IDisposable
    {
        protected readonly ApplicationDbContext _context;

        public TestRegiaoRepository()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            this._context = new ApplicationDbContext(options);
            this._context.Database.EnsureCreated();
        }        

        [Fact]
        public async Task GetRegiaoAllAsync_ReturnRegiaoCollection()
        {
            _context.Regiao.AddRange(RegiaoMockData.GetRegiaoAll());
            _context.SaveChanges();

            var sut = new RegiaoRepository(_context);

            var result = await sut.GetAllAsync();
            result.Should().HaveCount(RegiaoMockData.GetRegiaoAll().Count);
        }

        public void Dispose()
        {
            this._context.Database.EnsureDeleted();
            this._context.Dispose();
        }
    }
}
