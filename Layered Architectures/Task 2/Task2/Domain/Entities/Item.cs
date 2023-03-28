using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Item : IBaseEntity
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
