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
    public class ContatoRegiaoController : ControllerBase
    {
        private readonly IContatoRegiaoRepository contatoRegiaoRepository;
        private readonly IContatoRepository contatoRepository;
        private readonly IRegiaoRepository regiaoRepository;
        private readonly ContatoRegiaoValidator contatoRegiaoValidator;

        private readonly RabbitMQProdutorService rabbitMQProdutorService;

        public ContatoRegiaoController(IContatoRegiaoRepository contatoRegiaoRepository, IContatoRepository contatoRepository, IRegiaoRepository regiaoRepository,
            ContatoRegiaoValidator contatoRegiaoValidator, RabbitMQProdutorService rabbitMQProdutorService)
        {
            this.contatoRegiaoRepository = contatoRegiaoRepository;
            this.contatoRepository = contatoRepository;
            this.regiaoRepository = regiaoRepository;
            this.contatoRegiaoValidator = contatoRegiaoValidator;
            this.rabbitMQProdutorService = rabbitMQProdutorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await this.contatoRegiaoRepository.GetAllAsync();

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
                var result = await this.contatoRegiaoRepository.GetContatoRegiaoByIdAsync(Id);

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
        public async Task<IActionResult> Create([FromBody] ContatoRegiaoInput ContatoRegiaoInput)
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
                    foreach (var erro in contatoRegiaoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {erro.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar a contato região.");
                }
                else if ((await this.contatoRepository.GetByIdAsync(contatoRegiao.ContatoId)) is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato.");
                }
                else if ((await this.regiaoRepository.GetByIdAsync(contatoRegiao.RegiaoId)) is null)
                {
                    return BadRequest("Não foi possível obter as informações da região.");
                }
                else if ((await this.contatoRegiaoRepository.GetByContatoIdAndRegiaoIdAsync(ContatoRegiaoInput.ContatoId, ContatoRegiaoInput.RegiaoId)) is not null)
                {
                    return BadRequest("Já existe contato região cadastrado com o contatoId e regiaoId informados.");
                }

                if (this.rabbitMQProdutorService is not null)
                {
                    await this.rabbitMQProdutorService.SendMessage(contatoRegiao, this.rabbitMQProdutorService.configuration.GetSection("RabbitMQ")["NomeFilaContatoRegiao"] ?? string.Empty);
                }

                return CreatedAtAction(nameof(this.GetById), new { id = contatoRegiao.Id }, ContatoRegiaoInput);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContatoRegiaoInputUpdate ContatoRegiaoInputUpdate)
        {
            try
            {
                var contatoRegiao = await this.contatoRegiaoRepository.GetByIdAsync(ContatoRegiaoInputUpdate.Id);
                var contato = await this.contatoRepository.GetByIdAsync(ContatoRegiaoInputUpdate.ContatoId);
                var regiao = await this.regiaoRepository.GetByIdAsync(ContatoRegiaoInputUpdate.RegiaoId);

                if (contatoRegiao is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato região.");
                }
                else if (contato is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato.");
                }
                else if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região.");
                }

                var contatoId = contatoRegiao.ContatoId;
                var regiaoId = contatoRegiao.RegiaoId;

                contatoRegiao.ContatoId = ContatoRegiaoInputUpdate.ContatoId;
                contatoRegiao.RegiaoId = ContatoRegiaoInputUpdate.RegiaoId;

                var contatoRegiaoValidatorResult = await this.contatoRegiaoValidator.ValidateAsync(contatoRegiao);

                if (!contatoRegiaoValidatorResult.IsValid)
                {
                    foreach (var erro in contatoRegiaoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {erro.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar a contato região.");
                }
                else if ((!contatoId.Equals(ContatoRegiaoInputUpdate.ContatoId)) || (!regiaoId.Equals(ContatoRegiaoInputUpdate.RegiaoId)))
                {
                    
                    if ((await this.contatoRegiaoRepository.GetByContatoIdAndRegiaoIdAsync(contatoRegiao.ContatoId, contatoRegiao.RegiaoId)) is not null)
                    {
                        return BadRequest("Já existe contato região cadastrado com o contatoId e regiaoId informados.");
                    }

                    if (this.rabbitMQProdutorService is not null)
                    {
                        var contatoRegiaoAux = new ContatoRegiao()
                        {
                            Id = contatoRegiao.Id,
                            ContatoId = contatoRegiao.ContatoId,
                            RegiaoId = contatoRegiao.RegiaoId
                        };

                        await this.rabbitMQProdutorService.SendMessage(contatoRegiaoAux, this.rabbitMQProdutorService.configuration.GetSection("RabbitMQ")["NomeFilaContatoRegiao"] ?? string.Empty);
                    }
                }

                return NoContent();
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
                var contatoRegiao = await this.contatoRegiaoRepository.GetByIdAsync(Id);

                if (contatoRegiao is null)
                {
                    return BadRequest("Não foi possível obter as informações do contato região.");
                }

                if (this.rabbitMQProdutorService is not null)
                {
                    await this.rabbitMQProdutorService.SendMessage(contatoRegiao, this.rabbitMQProdutorService.configuration.GetSection("RabbitMQ")["NomeFilaContatoRegiaoDelete"] ?? string.Empty);
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
