using CommercialBackend.sqlDb;
using CommercialBackend.Dtos;
using CommercialBackend.Entities;
using Microsoft.AspNetCore.Mvc;

using CommercialBackend.Repositories;

using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
namespace CommercialBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevisController : ControllerBase
    {
        private IDevisRepository _devisRepository;

        public DevisController(IDevisRepository devisRepository)
        {
            _devisRepository = devisRepository;
        }

        // GET: api/Devis
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<DevisDto>>> GetAllDevis()
        {
            var devisList = await _devisRepository.GetAllAsync();
            var devisDtoList = devisList.Select(d => new DevisDto
            {
                Id = d.Id,
                Date = d.Date,
                Status = d.Status,
                Details = d.Details,
                TotalHT = d.TotalHT,
                TauxTVA = d.TauxTVA,
                MontantTVA = d.MontantTVA,
                TotalTTC = d.TotalTTC,
                ClientId = d.ClientId
            }).ToList();

            return Ok(devisDtoList);
        }

        // POST: api/Devis
        [HttpPost("AjouterDevis")]
        public async Task<ActionResult> CreateDevis(DevisDto devisDto)
        {
            var devis = new Devis
            {
                Id = devisDto.Id,
                Date = devisDto.Date,
                Status = devisDto.Status,
                Details = devisDto.Details,
                TotalHT = devisDto.TotalHT,
                TauxTVA = devisDto.TauxTVA,
                MontantTVA = devisDto.MontantTVA,
                TotalTTC = devisDto.TotalTTC,
                ClientId = devisDto.ClientId
            };

            await _devisRepository.AddAsync(devis);
            await _devisRepository.SaveAsync(devis);
            return Ok("Devis ajouté avec succès !");
        }




        [HttpGet("{id}/print")]
        public async Task<IActionResult> PrintDevis(string id)
        {
            var devis = await _devisRepository.Query()
      .Include(d => d.Client)
      .Include(d => d.DevisArticles)
      .ThenInclude(da => da.Article)
      .FirstOrDefaultAsync(d => d.Id == id);


            if (devis == null)
                return NotFound("Le devis est introuvable.");

            if (devis.Client == null)
                return BadRequest("Les informations du client ne sont pas disponibles pour ce devis.");

            var memoryStream = new MemoryStream();
            var pdfDocument = new PdfDocument();
            var pdfPage = pdfDocument.AddPage();
            var graphics = XGraphics.FromPdfPage(pdfPage);

            var fontHeader = new XFont("Verdana", 14);
            var fontBody = new XFont("Verdana", 12);
            var logo = XImage.FromFile("files/n.png");
            graphics.DrawImage(logo, 40, 20, 100, 50);  

                    graphics.DrawString("Devis", fontHeader, XBrushes.Black, new XRect(0, 80, pdfPage.Width, pdfPage.Height), XStringFormats.TopCenter);
            graphics.DrawString($"ID : {devis.Id}", fontBody, XBrushes.Black, new XPoint(40, 140));
            graphics.DrawString($"Date : {devis.Date:dd/MM/yyyy}", fontBody, XBrushes.Black, new XPoint(40, 160));
            graphics.DrawString($"Statut : {devis.Status}", fontBody, XBrushes.Black, new XPoint(40, 180));
            graphics.DrawString($"Client : {devis.Client.Nom}", fontBody, XBrushes.Black, new XPoint(40, 200));
            graphics.DrawString($"Total HT : {devis.TotalHT.ToString("C", CultureInfo.CurrentCulture)}", fontBody, XBrushes.Black, new XPoint(40, 220));
            graphics.DrawString($"TVA ({devis.TauxTVA}%): {devis.MontantTVA.ToString("C", CultureInfo.CurrentCulture)}", fontBody, XBrushes.Black, new XPoint(40, 240));
            graphics.DrawString($"Total TTC : {devis.TotalTTC.ToString("C", CultureInfo.CurrentCulture)}", fontBody, XBrushes.Black, new XPoint(40, 260));

            graphics.DrawString("Produits", fontHeader, XBrushes.Black, new XPoint(40, 280));

            var tableYPosition = 300;
            graphics.DrawString("Produit", fontBody, XBrushes.Black, new XPoint(40, tableYPosition));
            graphics.DrawString("Prix Unitaire", fontBody, XBrushes.Black, new XPoint(150, tableYPosition));
            graphics.DrawString("Quantité", fontBody, XBrushes.Black, new XPoint(250, tableYPosition));
            graphics.DrawString("Prix Total HT", fontBody, XBrushes.Black, new XPoint(350, tableYPosition));
            graphics.DrawString("TVA", fontBody, XBrushes.Black, new XPoint(450, tableYPosition));
            graphics.DrawString("Total TTC", fontBody, XBrushes.Black, new XPoint(550, tableYPosition));

            foreach (var devisArticle in devis.DevisArticles)
            {
                var article = devisArticle.Article;
                if (article == null) continue;  // Skip if article data is missing

                tableYPosition += 20;

                var prixTotalHT = article.Price * devisArticle.Quantite;
                var montantTVA = prixTotalHT * (devis.TauxTVA / 100);
                var prixTotalTTC = prixTotalHT + montantTVA;

                graphics.DrawString(article.Name, fontBody, XBrushes.Black, new XPoint(40, tableYPosition));
                graphics.DrawString($"{article.Price:C}", fontBody, XBrushes.Black, new XPoint(150, tableYPosition));
                graphics.DrawString(devisArticle.Quantite.ToString(), fontBody, XBrushes.Black, new XPoint(250, tableYPosition));
                graphics.DrawString($"{prixTotalHT:C}", fontBody, XBrushes.Black, new XPoint(350, tableYPosition));
                graphics.DrawString($"{montantTVA:C}", fontBody, XBrushes.Black, new XPoint(450, tableYPosition));
                graphics.DrawString($"{prixTotalTTC:C}", fontBody, XBrushes.Black, new XPoint(550, tableYPosition));
            }

            pdfDocument.Save(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream, "application/pdf", $"Devis_{devis.Id}.pdf");
        }


       

      


    // calculer la TVA à partir du total HT (avant taxes).
    [HttpPost("{devisId}/addarticletodevis")]
        public async Task<IActionResult> AddArticleToDevis(string devisId, [FromBody] Article article, int quantite)
        {
            // Récupérer le devis existant
            var devis = await _devisRepository.GetByIdAsync(devisId);
            if (devis == null)
                return NotFound("Le devis est introuvable.");

            // Valider l'article et la quantité
            if (article == null || string.IsNullOrEmpty(article.ArticleId) || quantite <= 0)
                return BadRequest("Données d'article ou quantité invalides.");

            // Initialiser la liste DevisArticles s'il est null
            if (devis.DevisArticles == null)
                devis.DevisArticles = new List<DevisArticle>();

            // Créer un nouvel objet DevisArticle et calculer le prix total pour l'article
            var devisArticle = new DevisArticle
            {
                DevisArticleId = Guid.NewGuid().ToString(), // Génération d'un Guid pour DevisArticleId
                DevisId = devisId,
                ArticleId = article.ArticleId,
                Quantite = quantite,
                PrixTotal = article.Price * quantite
            };

            // Ajouter l'article au devis
            devis.DevisArticles.Add(devisArticle);

            // Calculer le TotalHT et le TotalTTC du devis
            CalculerTotalDevis(devis);

            // Sauvegarder les changements
            await _devisRepository.SaveAsync(devis);

            return Ok(devis);
        }

        private void CalculerTotalDevis(Devis devis)
        {
            // Recalculer le TotalHT
            devis.TotalHT = devis.DevisArticles.Sum(da => da.PrixTotal);

            // Calculer Montant TVA et Total TTC
            devis.MontantTVA = devis.TotalHT * (devis.TauxTVA / 100);
            devis.TotalTTC = devis.TotalHT + devis.MontantTVA;
        }




    }
}
