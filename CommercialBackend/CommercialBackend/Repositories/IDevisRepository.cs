using CommercialBackend.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CommercialBackend.Repositories
{
    public interface IDevisRepository
    {
        Task<IEnumerable<Devis>> GetAllAsync();
        Task<Devis> GetByIdAsync(string id);
        Task AddAsync(Devis devis);
        Task UpdateAsync(Devis devis);
        Task DeleteAsync(Devis devis);
        Task SaveAsync(Devis devis);
        DbSet<Devis> Query();
    }
}
