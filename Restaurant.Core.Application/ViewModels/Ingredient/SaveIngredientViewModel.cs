using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.ViewModels.Ingredient
{
    public class SaveIngredientViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nombre del ingrediente.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
