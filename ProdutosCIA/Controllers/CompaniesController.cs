using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProdutosCIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompaniesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _unitOfWork.Companies.GetAllAsync();
            var companyDtos = companies.Select(c => new CompanyResponseDto(c.Id, c.Name, c.Cnpj));
            return Ok(companyDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company == null) return NotFound();

            return Ok(new CompanyResponseDto(company.Id, company.Name, company.Cnpj));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(CreateCompanyRequestDto requestDto)
        {
            var company = new Company { Name = requestDto.Name, Cnpj = requestDto.Cnpj };

            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.SaveChangesAsync();

            var responseDto = new CompanyResponseDto(company.Id, company.Name, company.Cnpj);
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCompanyRequestDto requestDto)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company == null) return NotFound();

            company.Name = requestDto.Name;
            company.Cnpj = requestDto.Cnpj;

            _unitOfWork.Companies.Update(company);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company == null) return NotFound();

            _unitOfWork.Companies.Delete(company);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

    }
}
