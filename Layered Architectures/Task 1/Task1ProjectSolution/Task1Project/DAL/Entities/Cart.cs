namespace Task1Project.DAL.Entities
{
    public class Cart : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Image? Image { get; set; }
    }
}
