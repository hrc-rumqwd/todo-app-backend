using TodoApp.Shared.Commons;

namespace TodoApp.Domain.Entities
{
    public class Product : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
