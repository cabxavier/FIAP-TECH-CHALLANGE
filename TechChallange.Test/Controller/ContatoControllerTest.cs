using CORE.Repository;
using CORE.Validator;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallange.Test.MockData;
using TECHCHALLANGEAPI.Controllers;

namespace TechChallange.Test.Controller
{
    public class ContatoControllerTest
    {
        private Mock<IContatoRepository> contatoRepository;
        private ContatoValidator contatoValidator;
       
        public ContatoControllerTest()
        {
            this.contatoRepository = new Mock<IContatoRepository>();
            this.contatoValidator = new ContatoValidator();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOkObjectResult()
        {
            this.contatoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoMockData.GetAll());
            var sut = new ContatoController(this.contatoRepository.Object, this.contatoValidator);

            var result = await sut.Get();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllVazioAsync_ShouldReturnNotFoundResult()
        {
            this.contatoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoMockData.GetAllVazio());
            var sut = new ContatoController(this.contatoRepository.Object, this.contatoValidator);

            var result = await sut.Get();
            Assert.IsType<NotFoundResult>(result);
            this.contatoRepository.Verify(_ => _.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOkObjectResult()
        {
            var contatoId = 1;
            this.contatoRepository.Setup(_ => _.GetByIdAsync(contatoId)).ReturnsAsync(ContatoMockData.Contato);
            var sut = new ContatoController(this.contatoRepository.Object, this.contatoValidator);

            var result = await sut.GetById(contatoId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateContatoAsync_ShouldReturnCreatedAtActionResult()
        {
            var contatoInput = ContatoMockData.ContatoInput();
            var sut = new ContatoController(this.contatoRepository.Object, this.contatoValidator);

            var result = await sut.Create(contatoInput);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task UpdateContatoAsync_ShouldReturnBadRequestObjectResult()
        {
            var contatoInputUpdate = ContatoMockData.ContatoInputUpdate();
            var sut = new ContatoController(this.contatoRepository.Object, this.contatoValidator);

            var result = await sut.Update(contatoInputUpdate);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteContatoAsync_ShouldReturnBadRequestObjectResult()
        {
            var contatoId = 1;
            var sut = new ContatoController(this.contatoRepository.Object, this.contatoValidator);

            var result = await sut.Delete(contatoId);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
