using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.ViewModels.Table
{
    public class UpdateTableViewModel
    {
        public int Id { get; set; }
        public int SeatingCapacity { get; set; }
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
