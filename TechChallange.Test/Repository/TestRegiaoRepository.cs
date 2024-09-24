using FluentAssertions;
using INFRASTRUCTURE.Repository;
using Microsoft.EntityFrameworkCore;
using TechChallange.Test.MockData;


namespace TechChallange.Test.Repository
{
    public class TestRegiaoRepository
    {
        protected readonly ApplicationDbContext _context;

        public TestRegiaoRepository()
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            this._context= new ApplicationDbContext(option);
            this._context.Database.EnsureCreated();
        }
        [Fact]
        public async Task GetRegioesAsync_returnTarefacallection()
        {
            _context.Regiao.AddRange(RegiaoMockData.GetRegiaos());
            _context.SaveChanges();

            var sut = new RegiaoRepository(_context);

            var result =await sut.GetAllAsync();
            result.Should().HaveCount(RegiaoMockData.GetRegiaos().Count);
         }

        public void Dispose()
        {
            this._context.Database.EnsureDeleted();
            this._context.Dispose   ( );
        }

    }
}
