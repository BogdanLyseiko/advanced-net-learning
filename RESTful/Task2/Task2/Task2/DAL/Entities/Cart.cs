namespace Task2.DAL.Entities
{
    public class Cart
    {
        public string Id { get; set; }

        public ICollection<Item>? Items { get; set; }
    }
}
