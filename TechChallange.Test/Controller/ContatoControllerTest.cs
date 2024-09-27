using CORE.Repository;
using CORE.Validator;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallange.Test.MockData;
using TECHCHALLANGEAPI.Controllers;
using FluentAssertions;
using CORE.Input;

namespace TechChallange.Test.Controller
{
    public class ContatoControllerTest
    {
        private ContatoValidator contatoValidator;
       
        public ContatoControllerTest()
        {
            this.contatoValidator = new ContatoValidator();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            var contatoRepository = new Mock<IContatoRepository>();
            contatoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoMockData.GetAll());
            var sut = new ContatoController(contatoRepository.Object, this.contatoValidator);

            var result = await sut.Get();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllVazioAsync_ShouldReturn404Status()
        {
            var contatoRepository = new Mock<IContatoRepository>();
            contatoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoMockData.GetAllVazio());
            var sut = new ContatoController(contatoRepository.Object, this.contatoValidator);

            var result = await sut.Get();
            Assert.IsType<NotFoundResult>(result);
            contatoRepository.Verify(_ => _.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturn200Status()
        {
            var contatoRepository = new Mock<IContatoRepository>();
            contatoRepository.Setup(_ => _.GetByIdAsync(1)).ReturnsAsync(ContatoMockData.Contato);
            var sut = new ContatoController(contatoRepository.Object, this.contatoValidator);

            var result = await sut.GetById(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateContatoAsync_ShouldReturn201Status()
        {
            var contatoRepository = new Mock<IContatoRepository>();
            var contatoInput = ContatoMockData.ContatoInput();
            var sut = new ContatoController(contatoRepository.Object, this.contatoValidator);

            var result = await sut.Create(contatoInput);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task UpdateContatoAsync_ShouldReturnBadRequestStatus()
        {
            var contatoRepository = new Mock<IContatoRepository>();
            var contatoInputUpdate = ContatoMockData.ContatoInputUpdate();
            var sut = new ContatoController(contatoRepository.Object, this.contatoValidator);

            var result = await sut.Update(contatoInputUpdate);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteContatoAsync_ShouldReturnBadRequestStatus()
        {
            var contatoRepository = new Mock<IContatoRepository>();
            var contatoId = 1;
            var sut = new ContatoController(contatoRepository.Object, this.contatoValidator);

            var result = await sut.Delete(contatoId);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
