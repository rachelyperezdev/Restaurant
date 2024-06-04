using Restaurant.Core.Application.ViewModels.Dish;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IDishService : IGenericService<SaveDishViewModel, DishViewModel, Dish>
    {
        Task AddIngredientToDish(int? dishId, int ingredientId);
        Task DeleteIngredientFromDish(int? dishId, int ingredientId);
        Task<List<DishViewModel>> GetAllViewModelWithInclude();
        Task<DishViewModel> GetByIdWithInclude(int id);
        Task<List<int>> GetDishIngredients();
        Task<decimal> GetPriceById(int dishId);
    }
}
