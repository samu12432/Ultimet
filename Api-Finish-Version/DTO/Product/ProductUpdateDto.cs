using System.ComponentModel.DataAnnotations;

namespace API_REST_PROYECT.DTOs.Product
{
    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "Es necesario ingresar el codigo del producto.")]
        public string codeProduct { get; set; } = string.Empty;
        public string nameProduct { get; set; } = string.Empty;
        public string descriptionProduct { get; set; } = string.Empty;
        public string imgUrl { get; set; } = string.Empty;
    }
}
