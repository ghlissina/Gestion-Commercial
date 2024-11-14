using CommercialBackend.Dtos;
using CommercialBackend.Entities;
using CommercialBackend.sqlDb;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommercialBackend.Services
{
    public class ClientService : IClientService
    {
        private readonly DataContext _context;

        public ClientService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            return await _context.Clients
                .Select(client => new ClientDto
                {
                    ClientId = client.ClientId,
                    Nom = client.Nom,
                    Prenom = client.Prenom,
                    Addresse = client.Addresse,
                    Email = client.Email,
                    Mobile = client.Mobile,
                    SiteUrl = client.SiteUrl,
                    Password = client.Password,
                    Fixe = client.Fixe,
                    RegistreCommerce = client.RegistreCommerce,
                    CIN = client.CIN
                })
                .ToListAsync();
        }

        public async Task<ClientDto> GetClientByIdAsync(string clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null) return null;

            return new ClientDto
            {
                ClientId = client.ClientId,
                Nom = client.Nom,
                Prenom = client.Prenom,
                Addresse = client.Addresse,
                Email = client.Email,
                Mobile = client.Mobile,
                SiteUrl = client.SiteUrl,
                Password = client.Password,
                Fixe = client.Fixe,
                RegistreCommerce = client.RegistreCommerce,
                CIN = client.CIN
            };
        }

        public async Task CreateClientAsync(ClientDto clientDto)
        {
            var client = new Client
            {
                ClientId = clientDto.ClientId,
                Nom = clientDto.Nom,
                Prenom = clientDto.Prenom,
                Addresse = clientDto.Addresse,
                Email = clientDto.Email,
                Mobile = clientDto.Mobile,
                SiteUrl = clientDto.SiteUrl,
                Password = clientDto.Password,
                Fixe = clientDto.Fixe,
                RegistreCommerce = clientDto.RegistreCommerce,
                CIN = clientDto.CIN
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateClientAsync(ClientDto clientDto)
        {
            var client = await _context.Clients.FindAsync(clientDto.ClientId);
            if (client == null) return false;

            client.Nom = clientDto.Nom;
            client.Prenom = clientDto.Prenom;
            client.Addresse = clientDto.Addresse;
            client.Email = clientDto.Email;
            client.Mobile = clientDto.Mobile;
            client.SiteUrl = clientDto.SiteUrl;
            client.Password = clientDto.Password;
            client.Fixe = clientDto.Fixe;
            client.RegistreCommerce = clientDto.RegistreCommerce;
            client.CIN = clientDto.CIN;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClientAsync(string clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null) return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
