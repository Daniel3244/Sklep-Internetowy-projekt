﻿using System.ComponentModel.DataAnnotations;

namespace Sklep_internetowy_projekt.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Desc { get; set; }

        [Required]
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between 0.01 and 1000.00")]
        public decimal Price { get; set; }
        [Required]
        public string ImagePath { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }


}
