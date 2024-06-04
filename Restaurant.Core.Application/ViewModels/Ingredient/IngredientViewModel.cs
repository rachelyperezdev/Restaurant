using Restaurant.Core.Application.ViewModels.Dish;

namespace Restaurant.Core.Application.ViewModels.Ingredient
{
    public class IngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<DishViewModel>? Dishes { get; set; }
    }
}
