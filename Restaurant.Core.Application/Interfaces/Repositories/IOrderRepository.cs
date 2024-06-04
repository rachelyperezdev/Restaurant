using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetByIdWithIncludeAsync(int id);
    }
}
