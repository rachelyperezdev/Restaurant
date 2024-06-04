using AutoMapper;
using Microsoft.AspNetCore.Http;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.ViewModels.Order;
using Restaurant.Core.Domain.Entities;
using Restaurant.Core.Application.Helpers;
using Restaurant.Core.Application.DTOs.Account;
using Restaurant.Core.Application.ViewModels.DishOrder;

namespace Restaurant.Core.Application.Services
{
    public class OrderService : GenericService<SaveOrderViewModel, OrderViewModel, Order>, IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;
        private readonly IDishOrderRepository _dishOrderRepository;

        public OrderService(IOrderRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IDishOrderRepository dishOrderRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _dishOrderRepository = dishOrderRepository;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task AddDish(int? orderId, int dishId)
        {
            DishOrder dishOrder = new()
            {
                OrderId = (int)orderId,
                DishId = dishId
            };

            await _dishOrderRepository.AddAsync(dishOrder);
        }

        public async Task<OrderViewModel> GetById(int id)
        {
            var order = await _repository.GetByIdWithIncludeAsync(id);

            if(order == null)
            {
                return null;
            }

            var orderVm = new OrderViewModel()
            {
                Id = order.Id,
                TableId = order.TableId,
                Subtotal = order.Subtotal,
                Status = order.Status,
                Dishes = order.Dishes.Select(dish => dish.Name).ToList()
            };
            return orderVm;
        }

        public async Task<List<DishOrderViewModel>> GetDishes(int orderId)
        {
            var dishes = await _dishOrderRepository.GetAllAsync();
            var dishesByOrder = dishes.FindAll(dishorder =>  dishorder.OrderId == orderId);
            return _mapper.Map<List<DishOrderViewModel>>(dishesByOrder);
        }

        public async Task<List<OrderViewModel>> GetOrdersWithDishes()
        {
            var orders = await _repository.GetAllAsyncWithIncludeAsync(new List<string> { "Dishes" });
            return orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                TableId = order.TableId,
                Subtotal = order.Subtotal,
                Status = order.Status,
                Dishes = order.Dishes.Select(dish => dish.Name).ToList() 
            }).ToList();
        }

        public async Task<OrderViewModel> GetOrderWithDishes(int orderId)
        {
            var order = await _repository.GetByIdWithIncludeAsync(orderId, new List<string>(), new List<string> { "Dishes" });
            return _mapper.Map<OrderViewModel>(order);  
        }

        public async Task RemoveDish(int? orderId, int dishId)
        {
            int order = (int)orderId;
            var existingOrder = await _dishOrderRepository.GetDishOrderByIdAsync(order, dishId);

            if (existingOrder != null)
            {
                await _dishOrderRepository.DeleteAsync(existingOrder);
            }
        }

        public async Task UpdateByUpdateVm(UpdateOrderViewModel vm, int id)
        {
            var order = await _repository.GetByIdWithIncludeAsync(id);
            Order orderUpdated = new()
            {
                Id = vm.Id,
                Status = order.Status,
                TableId = order.TableId,
                Subtotal = (decimal)vm.Subtotal
            };
            await _repository.UpdateAsync(orderUpdated, id);
        }
    }
}
