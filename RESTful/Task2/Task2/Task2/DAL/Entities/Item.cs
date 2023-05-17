namespace Task2.DAL.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? CartId { get; set; }

        public Cart? Cart { get; set; }
    }
}
