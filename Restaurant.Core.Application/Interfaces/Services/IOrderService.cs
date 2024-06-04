using Restaurant.Core.Application.ViewModels.DishOrder;
using Restaurant.Core.Application.ViewModels.Order;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IOrderService : IGenericService<SaveOrderViewModel, OrderViewModel, Order>
    {
        Task AddDish(int? orderId, int dishId);
        Task<List<DishOrderViewModel>> GetDishes(int orderId);
        Task<OrderViewModel> GetOrderWithDishes(int orderId);
        Task<List<OrderViewModel>> GetOrdersWithDishes();

        // Task<List<OrderViewModel>> GetOrders(int tableId);
        Task RemoveDish(int? orderId, int dishId);
        Task UpdateByUpdateVm(UpdateOrderViewModel vm, int id);
        Task<OrderViewModel> GetById(int id);
    }
}
