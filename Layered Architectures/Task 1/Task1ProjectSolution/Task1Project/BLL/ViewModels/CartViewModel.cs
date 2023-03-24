namespace Task1Project.BLL.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ImageViewModel? Image { get; set; }
    }
}
