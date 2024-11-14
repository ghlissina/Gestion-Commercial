using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class CommandeArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Commande")]
        public string CommandeId { get; set; }
        public Commande Commande { get; set; }
        [ForeignKey("Article")]
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public int Quantity { get; set; }
    }
}