using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialBackend.Entities
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ClientId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Addresse { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string SiteUrl { get; set; }
        public string Password { get; set; }
        public string Fixe { get; set; }
        public string RegistreCommerce { get; set; }
        public int CIN { get; set; }
        public ICollection<BonSortie> BonSortie { get; set; }
    }
}
