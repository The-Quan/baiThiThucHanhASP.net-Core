using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class Rental
    {
        public int RentalID { get; set; }

        [Required(ErrorMessage = "Customer is required.")]
        public int CustomerID { get; set; }  

        [Required(ErrorMessage = "Rental Date is required.")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Return Date is required.")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Active";

        public Customer Customer { get; set; }  
        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
