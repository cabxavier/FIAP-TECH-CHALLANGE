using CORE.Entity;
using CORE.Input;
using CORE.Repository;
using CORE.Validator;
using FluentValidation;
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
        private readonly ContatoRegiaoValidator contatoRegiaoValidator;

        public ContatoRegiaoController(IContatoRegiaoRepository contatoRegiaoRepository, IContatoRepository contatoRepository, IRegiaoRepository regiaoRepository, ContatoRegiaoValidator contatoRegiaoValidator)
        {
            this.contatoRegiaoRepository = contatoRegiaoRepository;
            this.contatoRepository = contatoRepository;
            this.regiaoRepository = regiaoRepository;
            this.contatoRegiaoValidator = contatoRegiaoValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok((await this.contatoRegiaoRepository.ObterContatoRegiaoTodos()));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task< IActionResult> Get([FromRoute] int Id)
        {
            try
            {
                return Ok((await this.contatoRegiaoRepository.ObterContatoRegiaoTodosPorId(Id)));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContatoRegiaoInput ContatoRegiaoInput)
        {
            try
            {
                var contatoRegiao = new ContatoRegiao()
                {
                    ContatoId = ContatoRegiaoInput.ContatoId,
                    RegiaoId = ContatoRegiaoInput.RegiaoId
                };

                var contatoRegiaoValidatorResult = await this.contatoRegiaoValidator.ValidateAsync(contatoRegiao);

                if (!contatoRegiaoValidatorResult.IsValid)
                {
                    foreach (var failure in contatoRegiaoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {failure.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar a contato região.");
                }

                if ((await this.contatoRepository.GetByIdAsync(contatoRegiao.ContatoId)) is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato.");
                }

                if ((await this.regiaoRepository.GetByIdAsync(contatoRegiao.RegiaoId)) is null)
                {
                    return BadRequest("Não foi possível obter as informações da região.");
                }

                await this.contatoRegiaoRepository.AddAsync(contatoRegiao);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ContatoRegiaoUpdateInput ContatoRegiaoUpdateInput)
        {
            try
            {
                var contatoRegiao = await this.contatoRegiaoRepository.GetByIdAsync(ContatoRegiaoUpdateInput.Id);
                var contato = await this.contatoRepository.GetByIdAsync(ContatoRegiaoUpdateInput.ContatoId);
                var regiao = await this.regiaoRepository.GetByIdAsync(ContatoRegiaoUpdateInput.RegiaoId);

                if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato.");
                }

                if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região.");
                }

                contatoRegiao.Id = ContatoRegiaoUpdateInput.Id;
                contatoRegiao.ContatoId = contato.Id;
                contatoRegiao.RegiaoId = regiao.Id;

                var contatoRegiaoValidatorResult = await this.contatoRegiaoValidator.ValidateAsync(contatoRegiao);

                if (!contatoRegiaoValidatorResult.IsValid)
                {
                    foreach (var failure in contatoRegiaoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {failure.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar a contato região.");
                }

                await this.contatoRegiaoRepository.UpdateAsync(contatoRegiao);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task< IActionResult> Delete([FromRoute] int Id)
        {
            try
            {
                var contatoRegiao = await this.contatoRegiaoRepository.GetByIdAsync(Id);

                if (contatoRegiao is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato região.");
                }

                await this.contatoRegiaoRepository.DeleteAsync(contatoRegiao.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }
    }
}
