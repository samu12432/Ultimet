using System.ComponentModel.DataAnnotations;
using Api_Finish_Version.Models.Enums;

namespace API_REST_PROYECT.DTOs.Supply
{
    public class EditSupplyDto
    {
        [Required]
        public string codeSupply { get; set; } = string.Empty;
        [Required]
        public string description { get; set; } = string.Empty;
        [Required]
        public TypeSupply type { get; set; }
    }
}
