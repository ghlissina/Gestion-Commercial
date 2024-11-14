using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class BonSortieArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("BonSortie")]
        public int BonSortieId { get; set; }
        public BonSortie BonSortie { get; set; }
        [ForeignKey("Article")]
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public int Quantity { get; set; }
    }
}