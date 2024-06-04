using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class DishIngredientRepository : GenericRepository<DishIngredient>, IDishIngredientRepository
    {
        private readonly ApplicationContext _dbContext;

        public DishIngredientRepository(ApplicationContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
    }
}
