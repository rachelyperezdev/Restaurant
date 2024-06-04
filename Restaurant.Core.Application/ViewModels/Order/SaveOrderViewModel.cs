using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.ViewModels.Order
{
    public class SaveOrderViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "La mesa es requerida para establecer la orden.")]
        public int TableId { get; set; }
        [Required(ErrorMessage = "Debe ingresar el subtotal de la orden.")]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }
        public int Status { get; set; }

        public List<int> DishesIds { get; set; }
    }
}
