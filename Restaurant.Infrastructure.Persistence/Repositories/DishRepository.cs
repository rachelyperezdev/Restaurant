using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        private readonly ApplicationContext _dbContext;

        public DishRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override Task<Dish> AddAsync(Dish entity)
        {
            switch (entity.Category)
            {
                case "1":
                    entity.Category = DishCategory.Appetizer.ToString();
                    break;
                case "2":
                    entity.Category = DishCategory.MainCourse.ToString();
                    break;
                case "3":
                    entity.Category = DishCategory.Dessert.ToString();
                    break;
                case "4":
                    entity.Category = DishCategory.Drink.ToString();
                    break;
            }
            
            return base.AddAsync(entity);
        }

        public async Task<Dish> GetByIdWithIncludeAsync(int id, List<string> includeProperties)
        {
            IQueryable<Dish> query = _dbContext.Set<Dish>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(dish => dish.Id == id);
        }

        public override async Task UpdateAsync(Dish entity, int id)
        {
            switch (entity.Category)
            {
                case "1":
                    entity.Category = DishCategory.Appetizer.ToString();
                    break;
                case "2":
                    entity.Category = DishCategory.MainCourse.ToString();
                    break;
                case "3":
                    entity.Category = DishCategory.Dessert.ToString();
                    break;
                case "4":
                    entity.Category = DishCategory.Drink.ToString();
                    break;
            }

            await base.UpdateAsync(entity, id);
        }
    }
}
