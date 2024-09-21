using CORE.Repository;
using CORE.Validator;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallange.Test.MockData;
using TECHCHALLANGEAPI.Controllers;
using FluentAssertions;

namespace TechChallange.Test.Controller
{
    public class TestContatoController
    {
        private ContatoValidator contatoValidator;
       
        public TestContatoController()
        {
            this.contatoValidator = new ContatoValidator();
        }

        [Fact]
        public async Task GetContatoAllAsync_ShouldReturn200Status()
        {
            var contatoRepository = new Mock<IContatoRepository>();
            contatoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoMockData.GetContatoAll());

            var sut = new ContatoController(contatoRepository.Object, this.contatoValidator);

            var result = (OkObjectResult)await sut.Get();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetContatoAllAsync_ShouldReturn204Status()
        {
            var contatoRepository = new Mock<IContatoRepository>();
            contatoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoMockData.GetContatoVazia());

            var sut = new ContatoController(contatoRepository.Object, this.contatoValidator);

            var result = (NoContentResult)await sut.Get();
            result.StatusCode.Should().Be(204);
            contatoRepository.Verify(_ => _.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task CreateContatoAsync_ShouldReturn201Status()
        {
            var contatoRepository = new Mock<IContatoRepository>();
            var contatoValidator = new ContatoValidator();
            var contatoInput = ContatoMockData.ContatoInputNovo();
            var sut = new ContatoController(contatoRepository.Object, this.contatoValidator);

            var result = (CreatedAtActionResult)await sut.Create(contatoInput);

            result.StatusCode.Should().Be(201);
        }
    }
}
