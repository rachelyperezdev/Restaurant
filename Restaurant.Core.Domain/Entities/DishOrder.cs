using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class DishOrder : AuditableBaseEntity
    {
        public int DishId { get; set; }
        public int OrderId { get; set; }
        public Dish Dish { get; set; }
        public Order Order { get; set; }
    }
}
