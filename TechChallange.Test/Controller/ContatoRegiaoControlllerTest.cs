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
    public class ContatoRegiaoControlllerTest
    {
        private ContatoRegiaoValidator contatoRegiaoValidator;

        public ContatoRegiaoControlllerTest() 
        {

            this.contatoRegiaoValidator = new ContatoRegiaoValidator();
        }
        [Fact]

        public async Task GetAllAsync_ShouldReturn200Status()
        {
            var contatoRegiaoRepository =new Mock<IContatoRegiaoRepository>();
            contatoRegiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoRegiaoMockData.GetAll());
            var sut = new ContatoRegiaoController(contatoRegiaoRepository.Object, this.contatoRegiaoValidator);

            var reult = await sut.Get();
            Assert.IsType<OkObjectResult>(reult);              
        }
        //Segunda Test
        [Fact]
        public async Task GetAllVazioAsync_ShouldReturn404Status()
        {
            var contatoRegiaoRepository = new Mock<IContatoRegiaoRepository>();
            contatoRegiaoRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(ContatoRegiaoMockData.GetAllVazio());
            var sut = new ContatoRegiaoController(contatoRegiaoRepository.Object, this.contatoRegiaoValidator);

            var reult = await sut.Get();
            Assert.IsType<NotFoundResult>(reult);
            contatoRegiaoRepository.Verify(_=>_.GetAllAsync(),Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturn200Status()
        {
            var contatoRegiaoRepository = new Mock<IContatoRegiaoRepository>();
            contatoRegiaoRepository.Setup(_ => _.GetByIdAsync(1)).ReturnsAsync(ContatoRegiaoMockData.ContatoRegiao);
            var sut = new ContatoRegiaoController(contatoRegiaoRepository.Object, this.contatoRegiaoValidator);

            var reult = await sut.GetById(1);
            Assert.IsType<OkObjectResult>(reult);
        }
        //ctrl c /v
        [Fact]
            public async Task CreateContatoRegiaoAsync_ShouldReturn201Status()
        {
            var contatoRegiaoRepository = new Mock<IContatoRegiaoRepository>();
            var contatoRegiaoInput = ContatoRegiaoMockData.ContatoRegiaoInput();
            var sut = new ContatoRegiaoController(contatoRegiaoRepository.Object, this.contatoRegiaoValidator);

            var result = await sut.Create(contatoRegiaoInput);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task UpdateContatoRegiaoAsync_ShouldReturnBadRequestStatus()
        {
            var contatoRegiaoRepository = new Mock<IContatoRegiaoRepository>();
            var contatoRegiaoInputUpdate = ContatoRegiaoMockData.ContatoRegiaoInputUpdate();
            var sut = new ContatoRegiaoController(contatoRegiaoRepository.Object, this.contatoRegiaoValidator);

            var result = await sut.Update(contatoRegiaoInputUpdate);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteContatoAsync_ShouldReturnBadRequestStatus()
        {
            var contatoRegiaoRepository = new Mock<IContatoRegiaoRepository>();
            var contatoId = 1;
            var sut = new ContatoRegiaoController(contatoRegiaoRepository.Object, this.contatoRegiaoValidator);

            var result = await sut.Delete(contatoId);
            Assert.IsType<BadRequestObjectResult>(result);


        }

    }
}
