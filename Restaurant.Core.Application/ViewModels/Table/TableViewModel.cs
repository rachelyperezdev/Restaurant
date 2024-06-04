using Restaurant.Core.Application.ViewModels.Order;

namespace Restaurant.Core.Application.ViewModels.Table
{
    public class TableViewModel
    {
        public int Id { get; set; }
        public int SeatingCapacity { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
