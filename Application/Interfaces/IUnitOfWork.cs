using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }
        ICompanyRepository Companies { get; }
        IProductRepository Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
