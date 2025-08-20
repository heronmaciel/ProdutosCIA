using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record ProductResponseDto(Guid Id, string Name, string Sku, decimal CostPrice);
    public record CreateProductRequestDto(string Name, string Sku, decimal CostPrice);
    public record UpdateProductRequestDto(string Name, string Sku, decimal CostPrice);
}
