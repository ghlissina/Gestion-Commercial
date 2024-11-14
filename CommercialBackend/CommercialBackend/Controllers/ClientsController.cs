using Microsoft.AspNetCore.Mvc;
using CommercialBackend.Dtos;
using CommercialBackend.Services;
using CommercialBackend.Entities;

namespace CommercialBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly List<Client> _clients = new List<Client>();
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Clients
        [HttpGet("GetAllClients")]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        // GET: api/Clients/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ClientDto>> GetClientById(string id)
        {
            var client = await _clientService.GetClientByIdAsync(id);

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        // POST: api/Clients
        [HttpPost("AjouterClient")]
        public async Task<ActionResult> CreateClient(ClientDto clientDto)
        {
            await _clientService.CreateClientAsync(clientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = clientDto.ClientId }, clientDto);
        }

        // PUT: api/Clients/{id}
        [HttpPut("EditbyId/{id}")]
        public async Task<ActionResult> UpdateClient(string id, ClientDto clientDto)
        {
            if (id != clientDto.ClientId)
                return BadRequest("Client ID mismatch.");

            var result = await _clientService.UpdateClientAsync(clientDto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Clients/{id}
        [HttpDelete("Supprimer/{id}")]
        public async Task<ActionResult> DeleteClient(string id)
        {
            var result = await _clientService.DeleteClientAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ClientDto> GetClientByRegistreCommerceAsync(string registreCommerce)
        {
            var client = _clients.FirstOrDefault(c => c.RegistreCommerce == registreCommerce);
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

    }
}
