using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_internetowy_projekt.Models
{
    public class OrderProduct
    {
        [Key]
        [Column(Order = 1)]
        public int OrderId { get; set; }

        [Column(Order = 2)]
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
