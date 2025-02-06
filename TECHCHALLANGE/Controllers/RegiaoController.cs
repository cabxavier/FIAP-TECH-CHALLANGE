using CORE.Entity;
using CORE.Input;
using CORE.Repository;
using CORE.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TechChallange.Core.ServiceRabbitMQ;

namespace TECHCHALLANGEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoRepository regiaoRepository;
        private readonly RegiaoValidator regiaoValidator;
        private readonly RabbitMQProdutorService rabbitMQProdutorService;

        public RegiaoController(IRegiaoRepository regiaoRepository, RegiaoValidator regiaoValidator, RabbitMQProdutorService rabbitMQProdutorService)
        {
            this.regiaoRepository = regiaoRepository;
            this.regiaoValidator = regiaoValidator;
            this.rabbitMQProdutorService = rabbitMQProdutorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await this.regiaoRepository.GetAllAsync();

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

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            try
            {
                var result = await this.regiaoRepository.GetByIdAsync(Id);

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegiaoInput RegiaoInput)
        {
            try
            {
                var regiao = new Regiao()
                {
                    Ddd = RegiaoInput.Ddd
                };

                var regiaoValidatorResult = await this.regiaoValidator.ValidateAsync(regiao);

                if (!regiaoValidatorResult.IsValid)
                {
                    foreach (var erro in regiaoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {erro.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar a região.");
                }
                else if ((await this.regiaoRepository.GetByDddAsync(RegiaoInput.Ddd)) is not null)
                {
                    return BadRequest("Já existe região cadastrado com o ddd informado.");
                }

                if (this.rabbitMQProdutorService is not null)
                {
                    await this.rabbitMQProdutorService.SendMessage(regiao, this.rabbitMQProdutorService.configuration.GetSection("RabbitMQ")["NomeFilaRegiao"] ?? string.Empty);
                }

                return CreatedAtAction(nameof(this.GetById), new { id = regiao.Id }, regiao);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RegiaoInputUpdate RegiaoInputUpdate)
        {
            try
            {
                var regiao = await this.regiaoRepository.GetByIdAsync(RegiaoInputUpdate.Id);

                if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região.");
                }

                var ddd = regiao.Ddd;

                regiao.Ddd = RegiaoInputUpdate.Ddd;

                var regiaoValidatorResult = await this.regiaoValidator.ValidateAsync(regiao);

                if (!regiaoValidatorResult.IsValid)
                {
                    foreach (var erro in regiaoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {erro.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar a região.");
                }
                else if (!ddd.Equals(RegiaoInputUpdate.Ddd))
                {
                    
                    if ((await this.regiaoRepository.GetByDddAsync(regiao.Ddd)) is not null)
                    {
                        return BadRequest("Já existe região cadastrado com o ddd informado.");
                    }

                    if (this.rabbitMQProdutorService is not null)
                    {
                        await this.rabbitMQProdutorService.SendMessage(regiao, this.rabbitMQProdutorService.configuration.GetSection("RabbitMQ")["NomeFilaRegiao"] ?? string.Empty);
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}");
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            try
            {
                var regiao = await this.regiaoRepository.GetByIdAsync(Id);

                if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região.");
                }

                if (this.rabbitMQProdutorService is not null)
                {
                    await this.rabbitMQProdutorService.SendMessage(regiao, this.rabbitMQProdutorService.configuration.GetSection("RabbitMQ")["NomeFilaRegiaoDelete"] ?? string.Empty);
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