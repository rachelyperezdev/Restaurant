using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.ViewModels.Dish;
using Restaurant.WebApi.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurant.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DishController : BaseApiController
    {
        private readonly IDishService _dishService;
        private readonly IIngredientService _ingredientService;

        public DishController(IDishService dishService, IIngredientService ingredientService)
        {
            _dishService = dishService;
            _ingredientService = ingredientService;
        }

        [HttpPost("Create")]
        [SwaggerOperation(
            Summary = "Create a new dish",
             Description = "Create a new dish with details provided in the request body. The category should be one of the following values:\n\n" +
                  "- 1: Appetizer\n" +
                  "- 2: Main Course\n" +
                  "- 3: Dessert\n" +
                  "- 4: Drink"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveDishViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveDishViewModel vm)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if(vm.IngredientsIds.Count == 0)
                {
                    ModelState.AddModelError("noIngredients", "Must add ingredients to the dish.");
                    return BadRequest(ModelState);
                }

                foreach(var id in vm.IngredientsIds)
                {
                    var ingredient = await _ingredientService.GetByIdSaveViewModel(id);
                    if (ingredient == null) 
                    {
                        ModelState.AddModelError("ingredientNoExists", $"There's not ingredient with this id '{id}'");
                        return BadRequest(ModelState);
                    }
                }

                if(!Enum.IsDefined(typeof(DishCategory), vm.Category))
                {
                    ModelState.AddModelError("categoryDoNotExists", $"There's not category '{vm.Category}'");
                    return BadRequest(ModelState);
                }

                var dish = await _dishService.Add(vm);

                foreach(var id in vm.IngredientsIds)
                {
                    await _dishService.AddIngredientToDish(dish.Id, id);
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
            Summary = "Update a dish",
             Description = "Update a dish with details provided in the request body. The category should be one of the following values:\n\n" +
                  "- 1: Appetizer\n" +
                  "- 2: Main Course\n" +
                  "- 3: Dessert\n" +
                  "- 4: Drink"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveDishViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, SaveDishViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _dishService.Update(vm, id);

                var currentIngredientIds = await _dishService.GetDishIngredients();

                var newIngredientIds = vm.IngredientsIds;

                foreach (var ingredientId in newIngredientIds)
                {
                    if (!currentIngredientIds.Contains(ingredientId))
                    {
                        await _dishService.AddIngredientToDish(vm.Id, ingredientId);
                    }
                }

                foreach (var currentIngredientId in currentIngredientIds)
                {
                    if (!newIngredientIds.Contains(currentIngredientId))
                    {
                        await _dishService.DeleteIngredientFromDish(vm.Id, currentIngredientId);
                    }
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
            Summary = "List all dishes",
             Description = "Get all dishes created."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var dishes = await _dishService.GetAllViewModelWithInclude();

                if(dishes == null || dishes.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        [SwaggerOperation(
            Summary = "Get a dish",
             Description = "Get a dish by its id."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof (DishViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var dish = await _dishService.GetByIdWithInclude(id);

                if (dish == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return Ok(dish);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
