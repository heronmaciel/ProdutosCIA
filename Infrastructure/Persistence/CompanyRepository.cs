using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Company> GetByIdAsync(Guid id) => await _context.Companies.FindAsync(id);
        public async Task<IEnumerable<Company>> GetAllAsync() => await _context.Companies.ToListAsync();

        public async Task AddAsync(Company company) => await _context.Companies.AddAsync(company);

        public void Update(Company company) =>_context.Companies.Update(company);

        public void Delete(Company company) => _context.Companies.Remove(company);
    }
}
