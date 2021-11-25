using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Model
{
    public class CustomerDetail
    {
        [Key]
        [Required]
        public int customerId { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Name cannot exceed 10")]
        public String customerName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public String customerEmail { get; set; }

        
        public String customerAddress1 { get; set; }

        
        public String customerAddress2 { get; set; }

        public int customerContactNumber { get; set; }
        [Required]
        public int customerPincode { get; set; }



    }
}
