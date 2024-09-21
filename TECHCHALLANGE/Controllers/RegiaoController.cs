﻿using CORE.Entity;
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
                var result = await this.regiaoRepository.GetAllAsync();

                if (result.Count == 0)
                {
                    return NoContent();
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
                var regiao = await this.regiaoRepository.GetByIdAsync(Id);

                return regiao is not null ? Ok(regiao) : NotFound("Não existe região com o filtro informado.");
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

                await this.regiaoRepository.AddAsync(regiao);

                return CreatedAtAction(nameof(this.GetById), new { id = regiao.Id }, regiao);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RegiaoUpdateInput RegiaoUpdateInput)
        {
            try
            {
                var regiao = await this.regiaoRepository.GetByIdAsync(RegiaoUpdateInput.Id);

                if (regiao is null)
                {
                    return BadRequest("Não foi possível obter as informações da região.");
                }

                var ddd = regiao.Ddd;

                regiao.Ddd = RegiaoUpdateInput.Ddd;

                var regiaoValidatorResult = await this.regiaoValidator.ValidateAsync(regiao);

                if (!regiaoValidatorResult.IsValid)
                {
                    foreach (var erro in regiaoValidatorResult.Errors)
                    {
                        return BadRequest($"Error: {erro.ErrorMessage}");
                    }

                    throw new ValidationException("Não foi possível validar a região.");
                }
                else if (!ddd.Equals(RegiaoUpdateInput.Ddd))
                {
                    if ((await this.regiaoRepository.GetByDddAsync(regiao.Ddd)) is not null)
                    {
                        return BadRequest("Já existe região cadastrado com o ddd informado.");
                    }

                    await this.regiaoRepository.UpdateAsync(regiao);
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

                await this.regiaoRepository.DeleteAsync(regiao.Id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:  {ex.Message}.");
            }
        }
    }
}