using CommercialBackend.Dtos;

namespace CommercialBackend.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(string clientId);
        Task CreateClientAsync(ClientDto clientDto);
        Task<bool> UpdateClientAsync(ClientDto clientDto);
        Task<bool> DeleteClientAsync(string clientId);
    }
}
