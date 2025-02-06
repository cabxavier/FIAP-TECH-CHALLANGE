using CORE.Repository;
using CORE.Validator;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallange.Core.ServiceRabbitMQ;
using TechChallange.Test.MockData;
using TECHCHALLANGEAPI.Controllers;

namespace TechChallange.Test.Controller
{
    public class ContatoRegiaoControllerTest
    {
        private ContatoRegiaoValidator contatoRegiaoValidator;
        private RabbitMQProdutorService rabbitMQProdutorService;
        private Mock<IContatoRegiaoRepository> contatoRegiaoRepository;
        private Mock<IRegiaoRepository> regiaoRepository;
        private Mock<IContatoRepository> contatoRepository;

        public ContatoRegiaoControllerTest()
        {
            this.contatoRegiaoRepository = new Mock<IContatoRegiaoRepository>();
            this.contatoRepository = new Mock<IContatoRepository>();
            this.regiaoRepository = new Mock<IRegiaoRepository>();
            this.contatoRegiaoValidator = new ContatoRegiaoValidator();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOkObjectResult()
        {
            this.contatoRegiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoRegiaoMockData.GetAll());
            var sut = new ContatoRegiaoController(this.contatoRegiaoRepository.Object, this.contatoRepository.Object, this.regiaoRepository.Object, this.contatoRegiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Get();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllVazioAsync_ShouldReturnNotFoundResult()
        {
            this.contatoRegiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoRegiaoMockData.GetAllVazio());
            var sut = new ContatoRegiaoController(this.contatoRegiaoRepository.Object, this.contatoRepository.Object, this.regiaoRepository.Object, this.contatoRegiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Get();
            Assert.IsType<NotFoundResult>(result);
            this.contatoRegiaoRepository.Verify(_ => _.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOkObjectResult()
        {
            var contatoRegiaoId = 1;
            this.contatoRegiaoRepository.Setup(_ => _.GetContatoRegiaoByIdAsync(contatoRegiaoId)).ReturnsAsync(ContatoRegiaoMockData.GetContatoRegiaoById());
            var sut = new ContatoRegiaoController(this.contatoRegiaoRepository.Object, this.contatoRepository.Object, this.regiaoRepository.Object, this.contatoRegiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.GetById(contatoRegiaoId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateContatoRegiaoAsync_ShouldReturnBadRequestObjectResult()
        {
            var contatoRegiaoInput = ContatoRegiaoMockData.ContatoRegiaoInput();
            var sut = new ContatoRegiaoController(this.contatoRegiaoRepository.Object, this.contatoRepository.Object, this.regiaoRepository.Object, this.contatoRegiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Create(contatoRegiaoInput);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateContatoRegiaoAsync_ShouldReturnBadRequestObjectResult()
        {
            var contatoRegiaoInputUpdate = ContatoRegiaoMockData.ContatoRegiaoInputUpdate();
            var sut = new ContatoRegiaoController(this.contatoRegiaoRepository.Object, this.contatoRepository.Object, this.regiaoRepository.Object, this.contatoRegiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Update(contatoRegiaoInputUpdate);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteContatoRegiaoAsync_ShouldReturnBadRequestObjectResult()
        {
            var contatoRegiaoId = 1;
            var sut = new ContatoRegiaoController(this.contatoRegiaoRepository.Object, this.contatoRepository.Object, this.regiaoRepository.Object, this.contatoRegiaoValidator, this.rabbitMQProdutorService);

            var result = await sut.Delete(contatoRegiaoId);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
