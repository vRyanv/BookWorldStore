using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWorldStore.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Invalid email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(8, ErrorMessage = "Password must be greater than or equal 8 character")]
        public string password { get; set; }
        public string role { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(10, ErrorMessage = "Phone must be equal 10 character")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }
        public string status { get; set; }

        public virtual ICollection<Order> orders { get; set; }
    }
}
