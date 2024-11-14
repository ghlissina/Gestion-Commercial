namespace CommercialBackend.Entities
{
    public class CategorieArticle
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        // Relation avec Article : Une catégorie peut avoir plusieurs articles
        public ICollection<Article> Articles { get; set; }
    }
}
