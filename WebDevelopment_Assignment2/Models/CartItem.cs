using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevelopment_Assignment2.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        [Required]
        public int Qty { get; set; }
        [ForeignKey("CartID")]
        [Required]
        public string CartID { get; set; }
        [ForeignKey("ProductID")]
        [Required]
        public int ProductID { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
