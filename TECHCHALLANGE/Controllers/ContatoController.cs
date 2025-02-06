using CORE.Entity;
using CORE.Input;
using CORE.Repository;
using CORE.Validator;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using TechChallange.Core.ServiceRabbitMQ;

namespace TECHCHALLANGEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepository contatoRepository;
        private readonly ContatoValidator contatoValidator;
        private readonly RabbitMQProdutorService rabbitMQProdutorService;

        public ContatoController(IContatoRepository contatoRepository, ContatoValidator contatoValidator, RabbitMQProdutorService rabbitMQProdutorService)
        {
            this.contatoRepository = contatoRepository;
            this.contatoValidator = contatoValidator;
            this.rabbitMQProdutorService = rabbitMQProdutorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await this.contatoRepository.GetAllAsync();

                if (result.Count == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var result = await this.contatoRepository.GetByIdAsync(Id);

                if (result is null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpGet("ddd/{Ddd}")]
        public async Task<IActionResult> GetContatoRegiaoByDdd([FromRoute] string Ddd)
        {
            try
            {
                var result = await this.contatoRepository.GetContatoRegiaoByDddAsync(Ddd);

                if (result.Count == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContatoInput ContatoInput)
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
                    foreach (var erro in contatoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {erro.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar o contato.");
                }
                else if ((await this.contatoRepository.GetByTelefoneAsync(ContatoInput.Telefone)) is not null)
                {
                    return BadRequest("Já existe contato cadastrado com o telefone informado.");
                }
                else if ((await this.contatoRepository.GetByEmailAsync(ContatoInput.Email)) is not null)
                {
                    return BadRequest("Já existe contato cadastrado com o e-mail informado.");
                }
                else if ((await this.contatoRepository.GetByNomeAndTelefoneAndEmailAsync(ContatoInput.Nome, ContatoInput.Telefone, ContatoInput.Email)) is not null)
                {
                    return BadRequest("Já existe contato cadastrado com o nome, telefone e e-mail informado.");
                }

                if (this.rabbitMQProdutorService is not null)
                {
                    await this.rabbitMQProdutorService.SendMessage(contato, this.rabbitMQProdutorService.configuration.GetSection("RabbitMQ")["NomeFilaContato"] ?? string.Empty);
                }

                return CreatedAtAction(nameof(this.GetById), new { id = contato.Id }, contato);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContatoInputUpdate ContatoInputUpdate)
        {
            try
            {
                var contato = await this.contatoRepository.GetByIdAsync(ContatoInputUpdate.Id);

                if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato.");
                }

                var nome = contato.Nome;
                var telefone = contato.Telefone;
                var email = contato.Email;

                contato.Nome = ContatoInputUpdate.Nome;
                contato.Telefone = ContatoInputUpdate.Telefone;
                contato.Email = ContatoInputUpdate.Email;

                var contatoValidatorResult = await this.contatoValidator.ValidateAsync(contato);

                if (!contatoValidatorResult.IsValid)
                {
                    foreach (var erro in contatoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {erro.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar o contato.");
                }
                else if (!telefone.Equals(ContatoInputUpdate.Telefone))
                {
                    if ((await this.contatoRepository.GetByTelefoneAsync(contato.Telefone)) is not null)
                    {
                        return BadRequest("Já existe contato cadastrado com o telefone informado.");
                    }
                }
                else if (!email.Equals(ContatoInputUpdate.Email))
                {
                    if ((await this.contatoRepository.GetByEmailAsync(contato.Email)) is not null)
                    {
                        return BadRequest("Já existe contato cadastrado com o e-mail informado.");
                    }
                }

                if (this.rabbitMQProdutorService is not null)
                {
                    await this.rabbitMQProdutorService.SendMessage(contato, this.rabbitMQProdutorService.configuration.GetSection("RabbitMQ")["NomeFilaContato"] ?? string.Empty);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            try
            {
                var contato = await this.contatoRepository.GetByIdAsync(Id);

                if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato.");
                }

                if (this.rabbitMQProdutorService is not null)
                {
                    await this.rabbitMQProdutorService.SendMessage(contato, this.rabbitMQProdutorService.configuration.GetSection("RabbitMQ")["NomeFilaContatoDelete"] ?? string.Empty);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }
    }
}
