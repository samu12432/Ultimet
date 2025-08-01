using System.ComponentModel.DataAnnotations;
using suply = Api_Finish_Version.Models.Supply.Supply;

namespace Api_Finish_Version.Models.Product
{
    public class SupplyNecessary
    {
        [Key]
        public int idSupplyNecessary { get; set; }

        [Required]
        public int supplyId { get; set; }

        public suply? supply { get; set; } = null;

        [Required]
        public int productId { get; set; }

        public Product? Product { get; set; } = null;

        [Required]
        public int Quantity { get; set; } 

        public SupplyNecessary() { }

        public SupplyNecessary(suply supply, int quantity)
        {
            this.supply = supply;
            Quantity = quantity;
        }
    }
}
