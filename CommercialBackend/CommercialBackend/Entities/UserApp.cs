using Microsoft.AspNetCore.Identity;

namespace CommercialBackend.Entities
{
    public class UserApp : IdentityUser
    {
       
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Addresse { get; set; }
        public string Matricule { get; set; }
        public ICollection<Commande> Commandes { get; set; }
       
        
    }
}