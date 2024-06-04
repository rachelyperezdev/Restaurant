using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class DishOrderRepository : GenericRepository<DishOrder>, IDishOrderRepository
    {
        private readonly ApplicationContext _dbContext;

        public DishOrderRepository(ApplicationContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<DishOrder> GetDishOrderByIdAsync(int orderId, int dishId)
        {
            var dishOrder = await _dbContext.Set<DishOrder>().FindAsync(dishId, orderId);
            return dishOrder;
        }
    }
}
