using AutoMapper;
using Microsoft.AspNetCore.Http;
using Restaurant.Core.Application.DTOs.Account;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.ViewModels.Dish;
using Restaurant.Core.Domain.Entities;
using Restaurant.Core.Application.Helpers;

namespace Restaurant.Core.Application.Services
{
    public class DishService : GenericService<SaveDishViewModel, DishViewModel, Dish>, IDishService
    {
        private readonly IDishRepository _repository;
        private readonly IDishIngredientRepository _dishIngredientRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;

        public DishService(IDishRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IDishIngredientRepository dishIngredientRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _dishIngredientRepository = dishIngredientRepository;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }


        public async Task AddIngredientToDish(int? dishId, int ingredientId)
        {
            DishIngredient dishIngredient = new()
            {
                DishId = (int)dishId,
                IngredientId = ingredientId
            };

            await _dishIngredientRepository.AddAsync(dishIngredient);
        }
        public async Task DeleteIngredientFromDish(int? dishId, int ingredientId)
        {
            var dishIngredients = await _dishIngredientRepository.GetAllAsync();
            var dishIngredient = dishIngredients.FirstOrDefault(di => di.DishId == dishId && di.IngredientId == ingredientId);

            if (dishIngredient != null)
            {
                await _dishIngredientRepository.DeleteAsync(dishIngredient);
            }
        }

        public async Task<List<int>> GetDishIngredients()
        {
            var dishIngredients = await _dishIngredientRepository.GetAllAsync();
            var ingredientsIds = dishIngredients.Select(di => di.IngredientId).ToList();

            return ingredientsIds;
        }

        public async Task<List<DishViewModel>> GetAllViewModelWithInclude()
        {
            var dishes = await _repository.GetAllAsyncWithIncludeAsync(new List<string> { "Ingredients" });

            return dishes.Select(dish => new DishViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                ServingSize = dish.ServingSize,
                Ingredients = dish.Ingredients.Select(ingredient => ingredient.Name).ToList(),
                Category = dish.Category
            }).ToList();
        }

        public async Task<DishViewModel> GetByIdWithInclude(int id)
        {
            var existingDish = await _repository.GetByIdAsync(id);
            if(existingDish == null)
            {
                return null;
            }
            // var dish = await _repository.GetByIdWithIncludeAsync(id, new List<string> { "Ingredients" });
            var dish = await _repository.GetByIdWithIncludeAsync(id, new List<string>(), new List<string> { "Ingredients" });
            if (dish == null)
            {
                return null;
            }

            var vm = new DishViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                ServingSize = dish.ServingSize,
                Category = dish.Category,
                Ingredients = dish.Ingredients.Select(ingredient => ingredient.Name).ToList() 
            };

            return vm;
        }

        public async Task<decimal> GetPriceById(int dishId)
        {
            var dish = await _repository.GetByIdAsync(dishId);
            return dish.Price;
        }
    }
}
