namespace Task1.ViewModels
{
    public class ItemResponseModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public List<LinkModel> Links => new()
        {
          new LinkModel{ Rel = "Self", Uri = $"/items/{Id}" },
          new LinkModel{ Rel = "Category", Uri = $"/categories/{CategoryId}" }
        };
    }
}