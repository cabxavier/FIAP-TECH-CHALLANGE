using CORE.Repository;
using CORE.Validator;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallange.Core.ServiceRabbitMQ;
using TechChallange.Test.MockData;
using TECHCHALLANGEAPI.Controllers;

namespace TechChallange.Test.Controller
{
    public class RegiaoControllerTest
    {
        private Mock<IRegiaoRepository> regiaoRepository;
        private RabbitMQProdutorService rabbitMQProdutorService;
        private RegiaoValidator regiaoValidator;

        public RegiaoControllerTest()
        {
            this.regiaoRepository = new Mock<IRegiaoRepository>();
            this.regiaoValidator = new RegiaoValidator();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOkObjectResult()
        {
            this.regiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(RegiaoMockData.GetAll());
            var sut = new RegiaoController(this.regiaoRepository.Object, this.regiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Get();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllVazioAsync_ShouldReturnNotFoundResult()
        {
            this.regiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(RegiaoMockData.GetAllVazio());
            var sut = new RegiaoController(this.regiaoRepository.Object, this.regiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Get();
            Assert.IsType<NotFoundResult>(result);
            this.regiaoRepository.Verify(_ => _.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOkObjectResult()
        {
            var regiaoId = 1;
            this.regiaoRepository.Setup(_ => _.GetByIdAsync(regiaoId)).ReturnsAsync(RegiaoMockData.Regiao);
            var sut = new RegiaoController(this.regiaoRepository.Object, this.regiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.GetById(regiaoId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateRegiaoAsync_ShouldReturnCreatedAtActionResult()
        {
            var regiaoInput = RegiaoMockData.RegiaoInput();
            var sut = new RegiaoController(this.regiaoRepository.Object, this.regiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Create(regiaoInput);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task UpdateRegiaoAsync_ShouldReturnBadRequestObjectResult()
        {
            var regiaoInputUpdate = RegiaoMockData.RegiaoInputUpdate();
            var sut = new RegiaoController(this.regiaoRepository.Object, this.regiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Update(regiaoInputUpdate);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteContatoAsync_ShouldReturnBadRequestObjectResult()
        {
            var regiaoId = 1;
            var sut = new RegiaoController(this.regiaoRepository.Object, this.regiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Delete(regiaoId);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
