using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        var productDtos = products.Select(p => new ProductResponseDto(p.Id, p.Name, p.Sku, p.CostPrice));
        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(new ProductResponseDto(product.Id, product.Name, product.Sku, product.CostPrice));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateProductRequestDto requestDto)
    {
        var product = new Product { Name = requestDto.Name, Sku = requestDto.Sku, CostPrice = requestDto.CostPrice };

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = new ProductResponseDto(product.Id, product.Name, product.Sku, product.CostPrice);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, responseDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequestDto requestDto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null) return NotFound();

        product.Name = requestDto.Name;
        product.Sku = requestDto.Sku;
        product.CostPrice = requestDto.CostPrice;

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null) return NotFound();

        _unitOfWork.Products.Delete(product);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}