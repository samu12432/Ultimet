using System.ComponentModel.DataAnnotations;

namespace Api_Finish_Version.DTO.Supply
{
    public class AccessoryDto
    {
        [Required(ErrorMessage = "Es necesario ingresar el nombre del insumo.")]
        [StringLength(50)]
        public string nameSupply { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es necesario ingresar una descripción del insumo.")]
        [StringLength(100)]
        public string descriptionSupply { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es necesario ingresar el nombre del proveedor.")]
        [StringLength(20)]
        public string nameSupplier { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal priceSupply { get; set; }
        //___________________________________________________//

        [Required(ErrorMessage = "Es necesario ingresar una descripción del accesorio.")]
        [StringLength(100)]
        public string descriptionAccessory { get; set; } = string.Empty;
    }
}
