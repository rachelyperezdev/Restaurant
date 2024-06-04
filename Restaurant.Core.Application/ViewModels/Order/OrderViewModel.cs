using Restaurant.Core.Application.ViewModels.Dish;
using Restaurant.Core.Application.ViewModels.Table;

namespace Restaurant.Core.Application.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public decimal Subtotal { get; set; }
        public string Status { get; set; }
        public List<string> Dishes { get; set; }
    }
}
