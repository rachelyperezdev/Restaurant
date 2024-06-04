using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IDishOrderRepository : IGenericRepository<DishOrder>
    {
        Task<DishOrder> GetDishOrderByIdAsync(int orderId, int dishId);
    }
}
