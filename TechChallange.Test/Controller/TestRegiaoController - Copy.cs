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
        private RegiaoValidator regiaoValidator;
       
        public TestRegiaoController()
        {
            this.regiaoValidator = new RegiaoValidator();
        }

        [Fact]
        public async Task GetRegiaoAllAsync_ShouldReturn200Status()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            regiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(RegiaoMockData.GetRegiaoAll());

            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidator);

            var result = (OkObjectResult)await sut.Get();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetRegiaoAllAsync_ShouldReturn204Status()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            regiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(RegiaoMockData.GetRegiaoVazia());

            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidator);

            var result = (NoContentResult)await sut.Get();
            result.StatusCode.Should().Be(204);
            regiaoRepository.Verify(_ => _.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task CreateRegiaoAsync_ShouldReturn201Status()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            var regiaoInput = RegiaoMockData.RegiaoInputNovo();
            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidator);

            var result = (CreatedAtActionResult)await sut.Create(regiaoInput);

            result.StatusCode.Should().Be(201);
        }
    }
}
