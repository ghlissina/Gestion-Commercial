using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class BonEntreeArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("BonEntree")]
        public int BonEntreeId { get; set; }
        public BonEntree BonEntree { get; set; }
        [ForeignKey("Article")]
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public int Quantity { get; set; }
    }
}