using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CompanyResponseDto(Guid id, string Name, string Cnpj);
    public record CreateCompanyRequestDto(string Name, string Cnpj);
    public record UpdateCompanyRequestDto(string Name, string Cnpj);
}
