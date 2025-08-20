using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(Guid id);
        Task<IEnumerable<Company>> GetAllAsync();
        Task AddAsync(Company company);
        void Update(Company company);
        void Delete(Company company);

    }
}
