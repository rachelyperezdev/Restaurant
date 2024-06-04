using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface ITableRepository : IGenericRepository<Table>
    {
        Task ChangeStatus(int tableId, int status);
        Task<List<Order>> GetTableOrder(int tableId);
    }
}
