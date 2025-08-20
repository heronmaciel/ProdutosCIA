using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IUserRepository? _userRepository;
        private ICompanyRepository? _companyRepository;
        private IProductRepository? _productRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public ICompanyRepository Companies => _companyRepository ??= new CompanyRepository(_context);
        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
