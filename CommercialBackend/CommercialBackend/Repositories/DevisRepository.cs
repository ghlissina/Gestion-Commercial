using CommercialBackend.Entities;
using CommercialBackend.sqlDb;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommercialBackend.Repositories
{
    public class DevisRepository : IDevisRepository
    {
        private readonly DataContext _context;

        public DevisRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Devis>> GetAllAsync()
        {
            return await _context.Devis.ToListAsync();
        }

        public async Task<Devis> GetByIdAsync(string id)
        {
            return await _context.Devis
                .Include(d => d.Client)
                .Include(d => d.DevisArticles)
                .ThenInclude(da => da.Article)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public DbSet<Devis> Query()
        {
            return _context.Devis;
        }



        public async Task AddAsync(Devis devis)
        {
            await _context.Devis.AddAsync(devis);
            await _context.SaveChangesAsync();
            await SaveAsync();
        }

        public async Task UpdateAsync(Devis devis)
        {
            _context.Devis.Update(devis);
            await _context.SaveChangesAsync();
            await SaveAsync();
        }

        public async Task DeleteAsync(Devis devis)
        {
            _context.Devis.Remove(devis);
            await _context.SaveChangesAsync();
            await SaveAsync();
        }
        public async Task SaveAsync(Devis devis = null)
        {
            // Si un devis spécifique est fourni, on effectue la mise à jour sur celui-ci
            if (devis != null)
            {
                _context.Devis.Update(devis);  // Assurez-vous que le devis est marqué pour mise à jour si besoin
            }

            // Sauvegarde de tous les changements effectués dans le contexte
            await _context.SaveChangesAsync();
        }
    }
}
