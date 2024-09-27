using CORE.Repository;
using CORE.Validator;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallange.Test.MockData;
using TECHCHALLANGEAPI.Controllers;
using FluentAssertions;
using INFRASTRUCTURE.Repository;
using CORE.Input;

namespace TechChallange.Test.Controller
{
    public class RegiaoControllerTest
    {
        private RegiaoValidator regiaoValidator;
       
        public RegiaoControllerTest()
        {
            this.regiaoValidator = new RegiaoValidator();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            regiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(RegiaoMockData.GetAll());
            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidator);

            var result = await sut.Get();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllVazioAsync_ShouldReturn404Status()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            regiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(RegiaoMockData.GetAllVazio());

            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidator);

            var result = await sut.Get();
            Assert.IsType<NotFoundResult>(result);
            regiaoRepository.Verify(_ => _.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturn200Status()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            regiaoRepository.Setup(_ => _.GetByIdAsync(1)).ReturnsAsync(RegiaoMockData.Regiao);
            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidator);

            var result = await sut.GetById(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateRegiaoAsync_ShouldReturn201Status()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            var regiaoInput = RegiaoMockData.RegiaoInput();
            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidator);

            var result = await sut.Create(regiaoInput);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task UpdateRegiaoAsync_ShouldReturnBadRequestStatus()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            var regiaoInputUpdate = RegiaoMockData.RegiaoInputUpdate();
            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidator);

            var result = await sut.Update(regiaoInputUpdate);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteContatoAsync_ShouldReturnBadRequestStatus()
        {
            var regiaoRepository = new Mock<IRegiaoRepository>();
            var regiaoId = 1;
            var sut = new RegiaoController(regiaoRepository.Object, this.regiaoValidator);

            var result = await sut.Delete(regiaoId);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
