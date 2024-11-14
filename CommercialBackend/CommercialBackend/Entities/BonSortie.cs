using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class BonSortie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public Client Client { get; set; }

        public ICollection<BonSortieArticle> Articles { get; set; }
    }
}