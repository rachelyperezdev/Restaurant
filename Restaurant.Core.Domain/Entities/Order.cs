using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class Order : AuditableBaseEntity
    {
        public int TableId { get; set; }
        public decimal Subtotal { get; set; }
        public string Status { get; set; }

        public Table AssignedTable { get; set; }
        public ICollection<Dish> Dishes { get; set; }
        public List<DishOrder> DishOrders { get; set; }
    }
}
