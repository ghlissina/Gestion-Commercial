using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommercialBackend.Entities
{
    public class DevisArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DevisArticleId { get; set; }
        [ForeignKey("Devis")]
        public string DevisId { get; set; }
        [JsonIgnore]
        public Devis Devis { get; set; }
        [ForeignKey("Article")]
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public int Quantite { get; set; }
      
        public double PrixTotal { get; set; }
    }
}
