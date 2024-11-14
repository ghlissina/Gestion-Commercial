using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialBackend.Dtos
{
    public class DevisDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }

        
        public double TotalHT { get; set; }

        
        public double TauxTVA { get; set; }


        public double MontantTVA { get; set; }

       
        public double TotalTTC { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
    }
}
