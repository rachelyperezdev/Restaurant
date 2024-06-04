using AutoMapper;
using Microsoft.AspNetCore.Http;
using Restaurant.Core.Application.DTOs.Account;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.ViewModels.Order;
using Restaurant.Core.Application.ViewModels.Table;
using Restaurant.Core.Domain.Entities;
using Restaurant.Core.Application.Helpers;
using Restaurant.Core.Application.Enums;


namespace Restaurant.Core.Application.Services
{
    public class TableService : GenericService<SaveTableViewModel, TableViewModel, Table>, ITableService
    {
        private readonly ITableRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;

        public TableService(ITableRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<bool> ChangeStatus(int tableId, int status)
        {
            var table = await _repository.GetByIdAsync(tableId);

            if (table == null)
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(TableStatus), status))
            {
                return false;
            }

            await _repository.ChangeStatus(tableId, status);
            return true;
        }

        public async Task<List<OrderViewModel>> GetTableOrder(int tableId)
        {
            List<Order> orders = await _repository.GetTableOrder(tableId);

            if(orders == null)
            {
                return null;
            }
            
            return orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                TableId = tableId,
                Status = order.Status,
                Subtotal = order.Subtotal,
                Dishes = order.Dishes.Select(dish => dish.Name).ToList()
            }).ToList();
        }

        public async Task UpdateByUpdateVm(UpdateTableViewModel vm, int id)
        {
            var table = await _repository.GetByIdAsync(id);
            Table tableUpdated = new()
            {
                Id = vm.Id,
                SeatingCapacity = vm.SeatingCapacity,
                Description = vm.Description,
                Status = table.Status
            };
            
            await _repository.UpdateAsync(tableUpdated, id);
        }

        public async Task<List<TableViewModel>> GetAll()
        {
            var tables = await _repository.GetAllAsync();

            return tables.Select(table => new TableViewModel()
            {
                Id= table.Id,
                SeatingCapacity= table.SeatingCapacity,
                Description = table.Description,
                Status = table.Status
            }).ToList();
        }

        public async Task<TableViewModel> GetById(int tableId)
        {
            var table = await _repository.GetByIdAsync(tableId);

            return table != null ? new TableViewModel
            {
                Id = table.Id,
                SeatingCapacity = table.SeatingCapacity,
                Description = table.Description,
                Status = table.Status
            } : null;
        }
    }
}
