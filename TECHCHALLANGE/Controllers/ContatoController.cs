using CORE.Entity;
using CORE.Input;
using CORE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TECHCHALLANGEAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepository contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            this.contatoRepository = contatoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(this.contatoRepository.ObterTodos());
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
                return Ok(this.contatoRepository.ObterPorId(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ddd/{Ddd:int}")]
        public IActionResult ObterContatoRegiaoPorDddGet([FromRoute] int Ddd)
        {
            try
            {
                return Ok(this.contatoRepository.ObterContatoRegiaoPorDdd(Ddd));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContatoInput ContatoInput)
        {
            try
            {
                var contato = new Contato()
                {
                    Nome = ContatoInput.Nome,
                    Telefone = ContatoInput.Telefone,
                    Email = ContatoInput.Email
                };

                this.contatoRepository.Cadastrar(contato);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] ContatoUpdateInput ContatoUpdateInput)
        {
            try
            {
                var contato = this.contatoRepository.ObterPorId(ContatoUpdateInput.Id);

                if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato");
                }

                contato.Id = ContatoUpdateInput.Id;
                contato.Nome = ContatoUpdateInput.Nome;
                contato.Telefone = ContatoUpdateInput.Telefone;
                contato.Email = ContatoUpdateInput.Email;

                this.contatoRepository.Alterar(contato);

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
                var contato = this.contatoRepository.ObterPorId(Id);

                if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato");
                }

                this.contatoRepository.Deletar(contato.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
