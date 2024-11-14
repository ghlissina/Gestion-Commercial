using CommercialBackend.Entities;

namespace CommercialBackend.Dtos
{
    public class ArticleDto
    {
        public string ArticleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public string LogoFilePath { get; set; }

        public string FileName { get; set; }
      
        public int CategorieArticleId { get; set; }
      
    }
}
