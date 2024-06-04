using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        private readonly ApplicationContext _dbContext;

        public TableRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // default state creation: available

        public async override Task<Table> AddAsync(Table table)
        {
            switch (table.Status)
            {
                case "1":
                    table.Status = TableStatus.Available.ToString();
                    break;
                case "2":
                    table.Status = TableStatus.InProcessOfService.ToString();
                    break;
                case "3":
                    table.Status = TableStatus.Served.ToString();
                    break;
                default:
                    table.Status = TableStatus.Available.ToString();
                    break;
            }

            return await base.AddAsync(table);
        }

        public async Task ChangeStatus(int tableId, int status) 
        { 
            var table = await _dbContext.Set<Table>().FindAsync(tableId);

            switch (status)
            {
                case 1:
                    table.Status = TableStatus.Available.ToString();
                    break;
                case 2:
                    table.Status = TableStatus.InProcessOfService.ToString();
                    break;
                case 3:
                    table.Status = TableStatus.Served.ToString();
                    break;
            }

            _dbContext.Entry(table).CurrentValues.SetValues(table);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetTableOrder(int tableId)
        {
            var ordersInProcess = await _dbContext.Set<Order>()
                             .Include(o => o.AssignedTable)
                             .Include(o => o.Dishes)
                             .Where(o => o.TableId == tableId && o.Status == OrderStatus.InProcess.ToString())
                             .ToListAsync();

            if(ordersInProcess.Count == 0)
            {
                return null;
            }

            return ordersInProcess;
        }
    }
}
