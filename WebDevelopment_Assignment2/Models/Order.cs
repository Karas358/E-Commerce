using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDevelopment_Assignment2.App_Data;

namespace WebDevelopment_Assignment2.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public string OrderTimeStamp { get; set; }
        public string UserEmail { get; set; }
        [Required]
        [ForeignKey("CartID")]
        public string CartID { get; set; }
        public virtual Cart Cart { get; set; }
        public IdentityUser User { get; set; }
    }
}
