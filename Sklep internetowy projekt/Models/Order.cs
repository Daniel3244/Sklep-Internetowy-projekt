using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sklep_internetowy_projekt.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Postal Code must be a 5-digit number.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Home Number is required.")]
        public string HomeNumber { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }

}
