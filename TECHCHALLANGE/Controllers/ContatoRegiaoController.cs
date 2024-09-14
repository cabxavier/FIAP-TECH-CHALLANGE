using CORE.Entity;
using CORE.Input;
using CORE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TECHCHALLANGEAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ContatoRegiaoController : ControllerBase
    {
        private readonly IContatoRegiaoRepository contatoRegiaoRepository;
        private readonly IContatoRepository contatoRepository;
        private readonly IRegiaoRepository regiaoRepository;

        public ContatoRegiaoController(IContatoRegiaoRepository contatoRegiaoRepository, IContatoRepository contatoRepository, IRegiaoRepository regiaoRepository)
        {
            this.contatoRegiaoRepository = contatoRegiaoRepository;
            this.contatoRepository = contatoRepository;
            this.regiaoRepository = regiaoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(this.contatoRegiaoRepository.ObterContatoRegiaoTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id:int}")]
        public IActionResult Get([FromRoute] int Id)
        {
            try
            {
                return Ok(this.contatoRegiaoRepository.ObterContatoRegiaoTodosPorId(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContatoRegiaoInput ContatoRegiaoInput)
        {
            try
            {
                var contato = this.contatoRepository.ObterPorId(ContatoRegiaoInput.ContatoId);
                var regiao = this.regiaoRepository.ObterPorId(ContatoRegiaoInput.RegiaoId);

                if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato");
                }

                if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região");
                }

                var contatoRegiao = new ContatoRegiao()
                {
                    ContatoId = contato.Id,
                    RegiaoId = regiao.Id
                };

                this.contatoRegiaoRepository.Cadastrar(contatoRegiao);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] ContatoRegiaoUpdateInput ContatoRegiaoUpdateInput)
        {
            try
            {
                var contatoRegiao = this.contatoRegiaoRepository.ObterPorId(ContatoRegiaoUpdateInput.Id);
                var contato = this.contatoRepository.ObterPorId(ContatoRegiaoUpdateInput.ContatoId);
                var regiao = this.regiaoRepository.ObterPorId(ContatoRegiaoUpdateInput.RegiaoId);

                if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato");
                }

                if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região");
                }

                contatoRegiao.Id = ContatoRegiaoUpdateInput.Id;
                contatoRegiao.ContatoId = contato.Id;
                contatoRegiao.RegiaoId = regiao.Id;

                this.contatoRegiaoRepository.Alterar(contatoRegiao);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id:int}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            try
            {
                var contatoRegiao = this.contatoRegiaoRepository.ObterPorId(Id);

                if (contatoRegiao is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato região");
                }

                this.contatoRegiaoRepository.Deletar(contatoRegiao.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
