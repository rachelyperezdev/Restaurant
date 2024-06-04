using Restaurant.Core.Application.ViewModels.Ingredient;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.ViewModels.Dish
{
    public class SaveDishViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nombre del plato.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debe ingresar el precio del plato.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Debe ingresar para cuantas personas es el plato.")]
        public int ServingSize { get; set; }
        [Required(ErrorMessage = "Debe ingresar los ingredientes del plato.")]
        [DataType(DataType.Text)]
        public List<int> IngredientsIds { get; set; }
        [Required(ErrorMessage = "Debe ingresar la categoría del plato.")]
        [DataType(DataType.Text)]
        public int Category { get; set; }
    }
}
