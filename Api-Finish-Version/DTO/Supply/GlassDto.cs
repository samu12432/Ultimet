using Api_Finish_Version.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api_Finish_Version.DTO.Supply
{
    public class GlassDto
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
        //___________________________________________________//

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal glassThickness { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal glassLength { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal glassWidth { get; set; }

        [EnumDataType(typeof(GlassType), ErrorMessage = "Tipo de vidrio no valido.")]
        public GlassType glassType { get; set; }



    }
}
