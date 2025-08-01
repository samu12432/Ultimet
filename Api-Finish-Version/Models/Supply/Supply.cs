using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api_Finish_Version.Models.Supply
{
    [Table ("Supply")]
    [Index(nameof(codeSupply), IsUnique = true)]
    public class Supply
    {
        [Key]
        public int idSupply { get; set; }
        public string codeSupply { get; set; } = string.Empty;
        public string nameSupply { get; set; } = string.Empty;
        public string descriptionSupply { get; set; } = string.Empty;
        public string imageUrl { get; set; } = string.Empty;
        public string nameSupplier { get; set; } = string.Empty;
        public decimal priceSupply { get; set; }

        public Supply(int idSupply, string codeSupply, string nameSupply, string descriptionSupply, string imageUrl, string nameSupplier, decimal priceSupply)
        {
            this.idSupply = idSupply;
            this.codeSupply = codeSupply;
            this.nameSupply = nameSupply;
            this.descriptionSupply = descriptionSupply;
            this.imageUrl = imageUrl;
            this.nameSupplier = nameSupplier;
            this.priceSupply = priceSupply;
        }
    }
}
