using Restaurant.Core.Application.ViewModels.Ingredient;
using Restaurant.Core.Application.ViewModels.Order;

namespace Restaurant.Core.Application.ViewModels.Dish
{
    public class DishViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ServingSize { get; set; }
        public string Category { get; set; }

        public List<string> Ingredients { get; set; }
    }
}
