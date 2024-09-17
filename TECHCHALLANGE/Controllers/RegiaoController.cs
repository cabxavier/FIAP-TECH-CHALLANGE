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
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoRepository regiaoRepository;
        private readonly RegiaoValidator regiaoValidator;

        public RegiaoController(IRegiaoRepository regiaoRepository, RegiaoValidator regiaoValidator)
        {
            this.regiaoRepository = regiaoRepository;
            this.regiaoValidator = regiaoValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok((await this.regiaoRepository.GetAllAsync()));
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
                return Ok((await this.regiaoRepository.GetByIdAsync(Id)));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegiaoInput RegiaoInput)
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
                    foreach (var failure in regiaoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {failure.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar a região.");
                }

                await this.regiaoRepository.AddAsync(regiao);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RegiaoUpdateInput RegiaoUpdateInput)
        {
            try
            {
                var regiao = await this.regiaoRepository.GetByIdAsync(RegiaoUpdateInput.Id);

                if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região.");
                }

                regiao.Id = RegiaoUpdateInput.Id;
                regiao.Ddd = RegiaoUpdateInput.Ddd;

                var regiaoValidatorResult = await this.regiaoValidator.ValidateAsync(regiao);

                if (!regiaoValidatorResult.IsValid)
                {
                    foreach (var failure in regiaoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {failure.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar a região.");
                }

                await this.regiaoRepository.UpdateAsync(regiao);

                return Ok();
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

                await this.regiaoRepository.DeleteAsync(regiao.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }
    }
}