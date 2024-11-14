using Microsoft.AspNetCore.Mvc;
using CommercialBackend.Dtos;
using CommercialBackend.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommercialBackend.sqlDb;

namespace CommercialBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly DataContext _context;

        public ArticlesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAllArticles()
        {
            var articles = _context.Articles.Select(article => new ArticleDto
            {
                ArticleId = article.ArticleId,
                Name = article.Name,
                Description = article.Description,
                Price = article.Price,
                StockQuantity = article.StockQuantity,
                LogoFilePath = article.LogoFilePath,
                FileName = article.FileName,
                CategorieArticleId = article.CategorieArticleId
            }).ToList();

            return Ok(articles);
        }

        // GET: api/Articles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticleById(string id)
        {
            var article = _context.Articles
                .Where(a => a.ArticleId == id)
                .Select(a => new ArticleDto
                {
                    ArticleId = a.ArticleId,
                    Name = a.Name,
                    Description = a.Description,
                    Price = a.Price,
                    StockQuantity = a.StockQuantity,
                    LogoFilePath = a.LogoFilePath,
                    FileName = a.FileName,
                    CategorieArticleId = a.CategorieArticleId
                })
                .FirstOrDefault();

            if (article == null)
                return NotFound();

            return Ok(article);
        }

        // POST: api/Articles
        [HttpPost]
        public async Task<ActionResult> CreateArticle(ArticleDto articleDto)
        {
            var article = new Article
            {
                ArticleId = articleDto.ArticleId,
                Name = articleDto.Name,
                Description = articleDto.Description,
                Price = articleDto.Price,
                StockQuantity = articleDto.StockQuantity,
                LogoFilePath = articleDto.LogoFilePath,
                FileName = articleDto.FileName,
                CategorieArticleId = articleDto.CategorieArticleId
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArticleById), new { id = article.ArticleId }, articleDto);
        }

        // PUT: api/Articles/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArticle(string id, ArticleDto articleDto)
        {
            if (id != articleDto.ArticleId)
                return BadRequest("ID mismatch.");

            var article = _context.Articles.FirstOrDefault(a => a.ArticleId == id);
            if (article == null)
                return NotFound();

            article.Name = articleDto.Name;
            article.Description = articleDto.Description;
            article.Price = articleDto.Price;
            article.StockQuantity = articleDto.StockQuantity;
            article.LogoFilePath = articleDto.LogoFilePath;
            article.FileName = articleDto.FileName;
            article.CategorieArticleId = articleDto.CategorieArticleId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Articles/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(string id)
        {
            var article = _context.Articles.FirstOrDefault(a => a.ArticleId == id);
            if (article == null)
                return NotFound();

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> SearchArticlesByName(string name)
        {
            var articles = _context.Articles
                .Where(a => a.Name.Contains(name))
                .Select(a => new ArticleDto
                {
                    ArticleId = a.ArticleId,
                    Name = a.Name,
                    Description = a.Description,
                    Price = a.Price,
                    StockQuantity = a.StockQuantity,
                    LogoFilePath = a.LogoFilePath,
                    FileName = a.FileName,
                    CategorieArticleId = a.CategorieArticleId
                })
                .ToList();

            return Ok(articles);
        }

    }
}
