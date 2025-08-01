using System.ComponentModel.DataAnnotations;

namespace Api_Finish_Version.DTO.Supply
{
    public class ProfileDto
    {
        [Required(ErrorMessage = "Es necesario ingresar el nombre del insumo.")]
        [StringLength(50)]
        public string nameSupply { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es necesario ingresar una descripción del insumo.")]
        [StringLength(100)]
        public string descriptionSupply { get; set; } = string.Empty;

        public string imageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es necesario ingresar el nombre del proveedor.")]
        [StringLength(20)]
        public string nameSupplier { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal priceSupply { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser mayor que cero.")]
        public decimal profileWeight { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El largo debe ser mayor que cero.")]
        public decimal profileHeight { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El peso por metro debe ser mayor que cero.")]
        public decimal weightPerMeter { get; set; }

        [Required(ErrorMessage = "El color es obligatorio.")]
        [StringLength(20)]
        public string profileColor { get; set; } = string.Empty;
    }
}
