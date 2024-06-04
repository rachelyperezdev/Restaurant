using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.ViewModels.Order;
using Restaurant.WebApi.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurant.WebApi.Controllers.v1
{
    [Authorize(Roles = "Waiter,SuperAdmin")]
    [ApiVersion("1.0")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IDishService _dishService;

        public OrderController(IOrderService orderService, IDishService dishService)
        {
            _orderService = orderService;
            _dishService = dishService;
        }

        [HttpPost("Create")]
        [SwaggerOperation(
            Summary = "Create a new order",
             Description = "Create a new order with details provided in the request body. The status should be one of the following values:\n\n" +
                  "- 1: InProcess\n" +
                  "- 2: Completed" 
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveOrderViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveOrderViewModel vm)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if(vm.DishesIds.Count == 0)
                {
                    ModelState.AddModelError("noDishes", "Must add at least one dish.");
                    return BadRequest(ModelState);
                }

                foreach(var id in vm.DishesIds)
                {
                    var dish = await _dishService.GetByIdSaveViewModel(id);
                    if (dish == null)
                    {
                        ModelState.AddModelError("dishDoesntExist", $"There are not any dish with this id '{id}'" );
                        return BadRequest(ModelState);
                    }
                }

                decimal subtotal = 0;
                foreach(var id in vm.DishesIds)
                {
                    var dish = await _dishService.GetByIdSaveViewModel(id);
                    subtotal += dish.Price;
                }

                vm.Subtotal = subtotal;
                vm.Status = (int)OrderStatus.InProcess;

                var order = await _orderService.Add(vm);

                foreach(var id in vm.DishesIds)
                {
                    await _orderService.AddDish(order.Id, id);
                }

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("Update/{id}")]
        [SwaggerOperation(
            Summary = "Update order's plates",
             Description = "Update order's plates with details provided in the request body."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateOrderViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateOrderViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(vm);
                }

                var order = await _orderService.GetByIdSaveViewModel(id);
                if (order == null)
                {
                    ModelState.AddModelError("orderDoesNotExist", $"There is not any order with this id '{id}'");
                    return BadRequest(ModelState);
                }

                if(vm.DishesIds.Count == 0)
                {
                    ModelState.AddModelError("NoDishes", "Must add a dish.");
                    return BadRequest(ModelState);  
                }

                foreach(var dishId in vm.DishesIds)
                {
                    var dish = await _dishService.GetByIdSaveViewModel(dishId);
                    if(dish == null)
                    {
                        ModelState.AddModelError("DishDoesntExists", $"There are not dishes with this id '{dishId}'");
                        return BadRequest(ModelState);
                    }
                }

                List<int> toAdd = new();
                List<int> toRemove = new();

                decimal amountToAdd = 0;
                decimal amountToSubstract = 0;

                var orderDishes = await _orderService.GetDishes(id);

                foreach(int dishId in vm.DishesIds)
                {
                    if (!orderDishes.Any(orderDish => orderDish.DishId == dishId))
                    {
                        toAdd.Add(dishId);
                        amountToAdd += await _dishService.GetPriceById(dishId);
                    }
                }

                foreach(var dish in orderDishes)
                {
                    if(!vm.DishesIds.Contains(dish.DishId))
                    {
                        toRemove.Add(dish.DishId);
                        amountToSubstract += await _dishService.GetPriceById(dish.DishId);
                    }
                }

                foreach(var dishId  in toRemove)
                {
                    await _orderService.RemoveDish(id, dishId);
                }

                var amountToAddVm = amountToAdd;

                vm.Subtotal = amountToAddVm;

                vm.Id = id;

                await _orderService.UpdateByUpdateVm(vm, id);

                foreach(var dishId in toAdd)
                {
                    await _orderService.AddDish(id, dishId);
                }

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("List")]
        [SwaggerOperation(
            Summary = "List all orders",
             Description = "Get all orders created."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var orders = await _orderService.GetOrdersWithDishes();

                if(orders == null || orders.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetById/{id}")]
        [SwaggerOperation(
            Summary = "Get an order",
             Description = "Get an order by its id."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var order = await _orderService.GetById(id);

                if(order == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return Ok(order);   
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(
            Summary = "Delete an order",
             Description = "Delete an order by its id."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
