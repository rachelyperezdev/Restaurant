using System.Text.Json.Serialization;

namespace Restaurant.Core.Application.ViewModels.Order
{
    public class UpdateOrderViewModel
    {
        public int Id { get; set; }
        public List<int> DishesIds { get; set; }
        [JsonIgnore]
        public decimal? Subtotal { get; set; }
    }
}
