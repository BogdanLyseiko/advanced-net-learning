using GraphQLTask.Entities;

namespace GraphQLTask.Models
{
    public class ItemResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
