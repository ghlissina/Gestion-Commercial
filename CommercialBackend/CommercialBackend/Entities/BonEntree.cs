using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class BonEntree
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Fournisseur")]
        public string FournisseurId { get; set; }
        public Fournisseur Fournisseur { get; set; }

        public ICollection<BonEntreeArticle> Articles { get; set; }
    }
}