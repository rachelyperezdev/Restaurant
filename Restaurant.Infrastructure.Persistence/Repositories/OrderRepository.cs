using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _dbContext;
        public OrderRepository(ApplicationContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetByIdWithIncludeAsync(int id)
        {
            return await _dbContext.Set<Order>()
                                    .Include(o => o.Dishes)
                                    .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async override Task<Order> AddAsync(Order order)
        {
            switch (order.Status)
            {
                case "1":
                    order.Status = OrderStatus.InProcess.ToString();
                    break;
                case "2":
                    order.Status = OrderStatus.Completed.ToString();
                    break;
                default:
                    order.Status = OrderStatus.InProcess.ToString();
                    break;
            }

            return await base.AddAsync(order);
        }

        public async override Task UpdateAsync(Order order, int id)
        {
            switch (order.Status)
            {
                case "1":
                    order.Status = OrderStatus.InProcess.ToString();
                    break;
                case "2":
                    order.Status = OrderStatus.Completed.ToString();
                    break;
            }
            await base.UpdateAsync(order, id);
        }
    }
}
