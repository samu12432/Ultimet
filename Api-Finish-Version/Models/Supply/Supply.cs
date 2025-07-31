using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Finish_Version.Models.Supply
{
    [Table ("Supply")]
    public class Supply
    {
        [Key]
        public int idSupply { get; set; }
        public string codeSupply { get; set; } = string.Empty;
        public string nameSupply { get; set; } = string.Empty;
        public string descriptionSupply { get; set; } = string.Empty;
        public string urlImgSupply { get; set; } = string.Empty;
        public string nameSupplier { get; set; } = string.Empty;
        public decimal priceSupply { get; set; }

        public Supply(int idSupply, string codeSupply, string nameSupply, string descriptionSupply, string nameSupplier, decimal priceSupply)
        {
            this.idSupply = idSupply;
            this.codeSupply = codeSupply;
            this.nameSupply = nameSupply;
            this.descriptionSupply = descriptionSupply;
            this.nameSupplier = nameSupplier;
            this.priceSupply = priceSupply;
        }
    }
}
