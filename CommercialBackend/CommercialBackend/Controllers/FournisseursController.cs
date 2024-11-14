using CommercialBackend.sqlDb;
using CommercialBackend.Dtos;
using CommercialBackend.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommercialBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FournisseursController : ControllerBase
    {
        private readonly DataContext dbcontext;
        

        public FournisseursController(DataContext context)
        {
            dbcontext = context;
        }

        // GET: api/Fournisseurs
        [HttpGet("GetAllF")]
        public async Task<ActionResult<IEnumerable<FournisseurDto>>> GetAllFournisseurs()
        {
            var fournisseurs = dbcontext.Fournisseurs
                .Select(f => new FournisseurDto
                {
                    FournisseurId = f.FournisseurId,
                    Nom = f.Nom,
                    Prenom = f.Prenom,
                    Addresse = f.Addresse,
                    Email = f.Email,
                    Mobile = f.Mobile,
                    SiteUrl = f.SiteUrl,
                    Password = f.Password,
                    Fixe = f.Fixe,
                    RegistreCommerce = f.RegistreCommerce,
                    CIN = f.CIN
                })
                .ToList();

            return Ok(fournisseurs);
        }

        // GET: api/Fournisseurs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FournisseurDto>> GetFournisseurById(string id)
        {
            var fournisseur = dbcontext.Fournisseurs
                .Where(f => f.FournisseurId == id)
                .Select(f => new FournisseurDto
                {
                    FournisseurId = f.FournisseurId,
                    Nom = f.Nom,
                    Prenom = f.Prenom,
                    Addresse = f.Addresse,
                    Email = f.Email,
                    Mobile = f.Mobile,
                    SiteUrl = f.SiteUrl,
                    Password = f.Password,
                    Fixe = f.Fixe,
                    RegistreCommerce = f.RegistreCommerce,
                    CIN = f.CIN
                })
                .FirstOrDefault();

            if (fournisseur == null)
                return NotFound();

            return Ok(fournisseur);
        }

        // POST: api/Fournisseurs
        [HttpPost("AjouterFr")]
        public async Task<ActionResult> CreateFournisseur(FournisseurDto fournisseurDto)
        {
            var fournisseur = new Fournisseur
            {
                FournisseurId = fournisseurDto.FournisseurId,
                Nom = fournisseurDto.Nom,
                Prenom = fournisseurDto.Prenom,
                Addresse = fournisseurDto.Addresse,
                Email = fournisseurDto.Email,
                Mobile = fournisseurDto.Mobile,
                SiteUrl = fournisseurDto.SiteUrl,
                Password = fournisseurDto.Password,
                Fixe = fournisseurDto.Fixe,
                RegistreCommerce = fournisseurDto.RegistreCommerce,
                CIN = fournisseurDto.CIN
            };

            dbcontext.Fournisseurs.Add(fournisseur);
            await dbcontext.SaveChangesAsync();

            return Ok("Fournisseur ajouté avec succès !");
        }

        // PUT: api/Fournisseurs/{id}
        [HttpPut("Modifier/{id}")]
        public async Task<ActionResult> UpdateFournisseur(string id, FournisseurDto fournisseurDto)
        {
            var fournisseur = dbcontext.Fournisseurs.FirstOrDefault(f => f.FournisseurId == id);
            if (fournisseur == null)
                return NotFound();

            fournisseur.Nom = fournisseurDto.Nom;
            fournisseur.Prenom = fournisseurDto.Prenom;
            fournisseur.Addresse = fournisseurDto.Addresse;
            fournisseur.Email = fournisseurDto.Email;
            fournisseur.Mobile = fournisseurDto.Mobile;
            fournisseur.SiteUrl = fournisseurDto.SiteUrl;
            fournisseur.Password = fournisseurDto.Password;
            fournisseur.Fixe = fournisseurDto.Fixe;
            fournisseur.RegistreCommerce = fournisseurDto.RegistreCommerce;
            fournisseur.CIN = fournisseurDto.CIN;

            await dbcontext.SaveChangesAsync();

            return Ok("Fournisseur mis à jour avec succès !");
        }

        // DELETE: api/Fournisseurs/{id}
        [HttpDelete("Supprimer/{id}")]
        public async Task<ActionResult> DeleteFournisseur(string id)
        {
            var fournisseur = dbcontext.Fournisseurs.FirstOrDefault(f => f.FournisseurId == id);
            if (fournisseur == null)
                return NotFound();

            dbcontext.Fournisseurs.Remove(fournisseur);
            await dbcontext.SaveChangesAsync();

            return Ok("Fournisseur supprimé avec succès !");
        }

        // Search by CIN, RegistreCommerce, Email, or Mobile
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FournisseurDto>>> SearchFournisseur(
            string cin = null, string registreCommerce = null, string email = null, string mobile = null)
        {
            var query = dbcontext.Fournisseurs.AsQueryable();

            if (!string.IsNullOrEmpty(cin))
                query = query.Where(f => f.CIN.ToString() == cin);
            if (!string.IsNullOrEmpty(registreCommerce))
                query = query.Where(f => f.RegistreCommerce.Contains(registreCommerce));
            if (!string.IsNullOrEmpty(email))
                query = query.Where(f => f.Email.Contains(email));
            if (!string.IsNullOrEmpty(mobile))
                query = query.Where(f => f.Mobile.Contains(mobile));

            var fournisseurs = query.Select(f => new FournisseurDto
            {
                FournisseurId = f.FournisseurId,
                Nom = f.Nom,
                Prenom = f.Prenom,
                Addresse = f.Addresse,
                Email = f.Email,
                Mobile = f.Mobile,
                SiteUrl = f.SiteUrl,
                Password = f.Password,
                Fixe = f.Fixe,
                RegistreCommerce = f.RegistreCommerce,
                CIN = f.CIN
            }).ToList();

            return Ok(fournisseurs);
        }
    }
}
