using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class DishIngredient : AuditableBaseEntity
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }
        public Dish Dish { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
