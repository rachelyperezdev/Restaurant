using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class Dish : AuditableBaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ServingSize { get; set; }
        public string Category { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public ICollection<Order> Orders { get; set; }
        public List<DishOrder> DishOrders { get; set; }
    }
}
