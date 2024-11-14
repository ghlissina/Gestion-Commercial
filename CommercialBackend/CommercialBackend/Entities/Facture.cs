using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class Facture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        [ForeignKey("Commande")]
        public string CommandeId { get; set; }
        public Commande Commande { get; set; }
    }
}
