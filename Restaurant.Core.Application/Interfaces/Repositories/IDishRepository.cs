using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IDishRepository : IGenericRepository<Dish>
    {
        //Task<Dish> GetByIdWithIncludeAsync(int id, List<string> includeProperties);
    }
}
