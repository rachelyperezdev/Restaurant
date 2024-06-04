using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.ViewModels.Table
{
    public class SaveTableViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Debe especificar la capacidad de asientos de la mesa.")]
        public int SeatingCapacity { get; set; }
        [Required(ErrorMessage = "Debe describir la mesa.")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}
