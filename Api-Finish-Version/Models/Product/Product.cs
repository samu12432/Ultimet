using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api_Finish_Version.Models.Enums;
using suply = Api_Finish_Version.Models.Supply.Supply;

namespace Api_Finish_Version.Models.Product
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int productId { get; set; }
        public string codeProduct { get; set; } = string.Empty;
        public string productName { get; set; } = string.Empty;
        public string productDescription { get; set; } = string.Empty;
        public ProductType productCategory { get; set; }

        public readonly List<SupplyNecessary> productDetail = new();
        public decimal productPrice { get; set; }
        public string imageUrl { get; set; } = string.Empty;


        public void AddSupply(suply supply, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Cantidad inválida.");

            productDetail.Add(new SupplyNecessary(supply, quantity));
        }
    }
}
