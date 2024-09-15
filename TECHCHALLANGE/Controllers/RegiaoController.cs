using CORE.Entity;
using CORE.Input;
using CORE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TECHCHALLANGEAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoRepository regiaoRepository;

        public RegiaoController(IRegiaoRepository regiaoRepository)
        {
            this.regiaoRepository = regiaoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(this.regiaoRepository.ObterTodos());
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
                return Ok(this.regiaoRepository.ObterPorId(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }       

        [HttpPost]
        public IActionResult Post([FromBody] RegiaoInput RegiaoInput)
        {
            try
            {
                var regiao = new Regiao()
                {
                    Ddd = RegiaoInput.Ddd
                };

                this.regiaoRepository.Cadastrar(regiao);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] RegiaoUpdateInput RegiaoUpdateInput)
        {
            try
            {
                var regiao = this.regiaoRepository.ObterPorId(RegiaoUpdateInput.Id);

                if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região");
                }

                regiao.Id = RegiaoUpdateInput.Id;
                regiao.Ddd = RegiaoUpdateInput.Ddd;

                this.regiaoRepository.Alterar(regiao);

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
                var regiao = this.regiaoRepository.ObterPorId(Id);

                if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região");
                }

                this.regiaoRepository.Deletar(regiao.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}