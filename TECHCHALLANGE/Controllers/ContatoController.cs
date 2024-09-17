using CORE.Entity;
using CORE.Input;
using CORE.Repository;
using CORE.Validator;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace TECHCHALLANGEAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepository contatoRepository;
        private readonly ContatoValidator contatoValidator;

        public ContatoController(IContatoRepository contatoRepository, ContatoValidator contatoValidator)
        {
            this.contatoRepository = contatoRepository;
            this.contatoValidator = contatoValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok((await this.contatoRepository.GetAllAsync()));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get([FromRoute] int Id)
        {
            try
            {
                return Ok((await this.contatoRepository.GetByIdAsync(Id)));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpGet("ddd/{Ddd}")]
        public async Task<IActionResult> ObterContatoRegiaoPorDddGet([FromRoute] string Ddd)
        {
            try
            {
                return Ok((await this.contatoRepository.ObterContatoRegiaoPorDdd(Ddd)));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContatoInput ContatoInput)
        {
            try
            {
                var contato = new Contato()
                {
                    Nome = ContatoInput.Nome,
                    Telefone = ContatoInput.Telefone,
                    Email = ContatoInput.Email
                };

                var contatoValidatorResult = await this.contatoValidator.ValidateAsync(contato);

                if (!contatoValidatorResult.IsValid)
                {
                    foreach (var failure in contatoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {failure.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar o contato.");
                }

                await this.contatoRepository.AddAsync(contato);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ContatoUpdateInput ContatoUpdateInput)
        {
            try
            {
                var contato = await this.contatoRepository.GetByIdAsync(ContatoUpdateInput.Id);

                if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato.");
                }

                contato.Id = ContatoUpdateInput.Id;
                contato.Nome = ContatoUpdateInput.Nome;
                contato.Telefone = ContatoUpdateInput.Telefone;
                contato.Email = ContatoUpdateInput.Email;

                var contatoValidatorResult = await this.contatoValidator.ValidateAsync(contato);

                if (!contatoValidatorResult.IsValid)
                {
                    foreach (var failure in contatoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {failure.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar o contato.");
                }

                await this.contatoRepository.UpdateAsync(contato);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            try
            {
                var contato = await this.contatoRepository.GetByIdAsync(Id);

                if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato.");
                }

                await this.contatoRepository.DeleteAsync(contato.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }
    }
}
