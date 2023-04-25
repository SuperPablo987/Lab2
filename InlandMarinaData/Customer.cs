using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    [Table("Customer")]
    public class Customer
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a First Name")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a Last Name")]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a valid phone number.")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
        [StringLength(15)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter a City")]
        [StringLength(30)]
        public string City { get; set; }
        
        [StringLength(30)]
        [Required(ErrorMessage = "Please enter a Username.")]
        public string Username { get; set; }
        
        
        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(30)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [NotMapped]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        

        // navigation property
        public virtual ICollection<Lease> Leases { get; set; }

    }
}
