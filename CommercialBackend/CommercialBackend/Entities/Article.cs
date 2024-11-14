using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] // Option None pour les chaînes
        //public string ArticleId { get; set; } = Guid.NewGuid().ToString(); // Génère un Guid pour chaque article

        public string ArticleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public string LogoFilePath { get; set; }
        [ForeignKey("CategorieArticle")]
        public int CategorieArticleId { get; set; }
        public CategorieArticle CategorieArticle { get; set; }
        public string FileName { get; set; }
        public ICollection<BonEntreeArticle> BonsEntree { get; set; }
        public ICollection<BonSortieArticle> BonsSortie { get; set; }
        public ICollection<CommandeArticle> Commandes { get; set; }
        
        public ICollection<DevisArticle> DevisArticles { get; set; }
    }
}