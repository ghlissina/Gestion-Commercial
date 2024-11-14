using CommercialBackend.sqlDb;
using CommercialBackend.Entities;
using CommercialBackend.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommercialBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet("GetAllCat")]
        public async Task<ActionResult<IEnumerable<CategorieDto>>> GetAllCategories()
        {
            var categories = _context.CategorieArticles
                .Select(c => new CategorieDto
                {
                    Id = c.Id,
                    Nom = c.Nom,
                    Description = c.Description
                })
                .ToList();

            return Ok(categories);
        }

        [HttpGet("searchnomcat")]
        public async Task<ActionResult<IEnumerable<CategorieDto>>> SearchCategoriesByName(string nom)
        {
            var categories = _context.CategorieArticles
                .Where(c => c.Nom.Contains(nom))
                .Select(c => new CategorieDto
                {
                    Id = c.Id,
                    Nom = c.Nom,
                    Description = c.Description
                })
                .ToList();

            return Ok(categories);
        }


        // GET: api/Categories/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CategorieDto>> GetCategoryById(int id)
        {
            var category = _context.CategorieArticles
                .Where(c => c.Id == id)
                .Select(c => new CategorieDto
                {
                    Id = c.Id,
                    Nom = c.Nom,
                    Description = c.Description
                })
                .FirstOrDefault();

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/Categories
        [HttpPost("AjouterArticle")]
        public async Task<ActionResult> CreateCategory(CategorieDto categorieDto)
        {
            var category = new CategorieArticle
            {
                Nom = categorieDto.Nom,
                Description = categorieDto.Description
            };

            _context.CategorieArticles.Add(category);
            await _context.SaveChangesAsync();

            return Ok("La catégorie a été ajoutée avec succès.");

        }

        // PUT: api/Categories/{id}
        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategorieDto categorieDto)
        {
            if (id != categorieDto.Id)
                return BadRequest("ID mismatch.");

            var category = _context.CategorieArticles.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            category.Nom = categorieDto.Nom;
            category.Description = categorieDto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("Supprimer/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = _context.CategorieArticles.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            _context.CategorieArticles.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
