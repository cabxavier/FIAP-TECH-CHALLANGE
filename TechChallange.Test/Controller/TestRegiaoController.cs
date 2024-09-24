using CORE.Repository;
using CORE.Validator;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallange.Test.MockData;
using TECHCHALLANGEAPI.Controllers;
using FluentAssertions;

namespace TechChallange.Test.Controller
{
    public class TestRegiaoController
    {
        private RegiaoValidator regiaoValidador;

        public TestRegiaoController()
        {
            this.regiaoValidador = new RegiaoValidator();
        }

        [Fact]
        public async Task GetRegioesAsync_shouldReturn200Status()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            regiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(RegiaoMockData.GetRegiaos());

            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidador);

            var result = (OkObjectResult)await sut.Get();
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task GetRegioesAsync_shoulReturn204Status()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            regiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(RegiaoMockData.GetRegiaos());

            var sut = new RegiaoController(regiaoRepository.Object,this.regiaoValidador);

            var result = (NoContentResult)await sut.Get();
            result.StatusCode.Should().Be(200);
            regiaoRepository.Verify(_=>_.GetAllAsync(), Times.Exactly(1));

        }

        [Fact]

        public async Task CreateRegiaoAsync_ShouldReturn201Status()
        {
            var regiaoRepository =new Mock<IRegiaoRepository>();
            var regiaoValidador = new RegiaoValidator();
            var regiaoInput = RegiaoMockData.GetRegiaos();
            var sut = new RegiaoController (regiaoRepository.Object,this.regiaoValidador);

            var result = (CreatedAtActionResult)await sut.Create(regiaoInput);
            result.StatusCode.Should().Be(201);
        }


    }
}
