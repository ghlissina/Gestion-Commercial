using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class Devis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }

        // Total HT (hors taxes)
        public double TotalHT { get; set; }

        // Taux de TVA en pourcentage
        public double TauxTVA { get; set; }

        // Montant de la TVA
        public double MontantTVA { get; set; }

        // Total TTC (toutes taxes comprises)
        public double TotalTTC { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public Client Client { get; set; }

        // Relation avec les articles via la table de jointure DevisArticle
        public ICollection<DevisArticle> DevisArticles { get; set; }
    }

}
