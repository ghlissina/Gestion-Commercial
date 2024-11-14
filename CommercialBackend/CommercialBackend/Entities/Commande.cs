using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class Commande
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        //EnCours,
        //Livrée,
        //Annulée
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public Client Client { get; set; }

        public ICollection<CommandeArticle> Articles { get; set; }
    }

   

}