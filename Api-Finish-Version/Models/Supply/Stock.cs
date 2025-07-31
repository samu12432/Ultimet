using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Finish_Version.Models.Supply
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int idStock { get; set; }
        public string codeSupply { get; set; } = string.Empty;
        public Supply Supply { get; set; } = null!; 
        public int stockQuantity { get; set; }
        public DateTime stockCreate { get; set; }
        public DateTime stockUpdate { get; set; }


        public Stock() { }

        public Stock(string codeSupply, int stockQuantity)
        {
            this.codeSupply = codeSupply;
            this.stockQuantity = stockQuantity;
            stockCreate = DateTime.Now;
            stockUpdate = DateTime.Now;
        }
    }
}
