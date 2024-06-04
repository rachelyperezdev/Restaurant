using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class Ingredient : AuditableBaseEntity
    {
        public string Name { get; set; }

        public ICollection<Dish> Dishes { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }

    }
}
