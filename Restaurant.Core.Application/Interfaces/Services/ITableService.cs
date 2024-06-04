using Restaurant.Core.Application.ViewModels.Dish;
using Restaurant.Core.Application.ViewModels.Order;
using Restaurant.Core.Application.ViewModels.Table;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface ITableService : IGenericService<SaveTableViewModel, TableViewModel, TableViewModel>
    {
        Task<bool> ChangeStatus(int tableId, int status);
        Task<List<TableViewModel>> GetAll();
        Task<TableViewModel> GetById(int tableId);
        Task<List<OrderViewModel>> GetTableOrder(int tableId);
        Task UpdateByUpdateVm(UpdateTableViewModel vm, int id);
    }
}
